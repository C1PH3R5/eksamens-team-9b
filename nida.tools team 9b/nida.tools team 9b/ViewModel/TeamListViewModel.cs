using MySql.Data.MySqlClient;
using nida.tools_team_9b.Database;
using nida.tools_team_9b.Model;
using nida.tools_team_9b.View.page;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;


namespace nida.tools_team_9b.ViewModel
{
    /// <summary>
    /// TeamListViewModel inholder metoder som er tildelt til teamList page.
    /// </summary>
    public class TeamListViewModel : DBCon
    {
        /// <summary>
        /// ShowTeamListButtons metoden viser funktions knapperne efter hvilken rolle id der er gemt i global values, ved hjelp af Visibility funktionen i WPF.
        /// Metoden tager 1 parameter.
        /// </summary>
        /// <param name="window">window er en instans af TeamList pagen</param>
        public static void ShowTeamListButtons(TeamList window)
        {
            string roleId = Application.Current.Properties["Global_userRole"].ToString();
            int convertedRoleId = Convert.ToInt32(roleId);
            if (convertedRoleId < 3)
            {
                window.opretTeam.Visibility = Visibility.Visible;
                window.fjernTeam.Visibility = Visibility.Visible;
                window.redigereTeam.Visibility = Visibility.Visible;
            }
        }


        /// <summary>
        /// GetTeam metoden henter data ned fra tabellen Team og pakker det sammen i en liste af objected team modellen.
        /// Metoden tager 1 parameter.
        /// </summary>
        /// <param name="window">window er en instans af TeamList pagen</param>
        /// <returns> returner en liste af objected team.</returns>
        public static List<Team> GetTeam(TeamList window)
        {
            window.TeamListGrid.Columns[0].Visibility = Visibility.Hidden;
            MySqlConnection con = GetConnection();
            con.Open();
            string sqlQuery = "SELECT employee.firstname, employee.lastname, team.teamNavn, team.id FROM ((employee INNER JOIN employee_team ON employee.id = employee_team.employee_id) INNER JOIN team ON employee_team.team_id = team.id) WHERE employee.roleid = 2;";
            MySqlCommand cmd = new MySqlCommand(sqlQuery, con);

            List<Team> TeamList = new List<Team>();

            using (MySqlDataReader Reader = cmd.ExecuteReader())
            {
                if (Reader.HasRows)
                {
                    while (Reader.Read())
                    {
                        //I would also check for DB.Null here before reading the value.
                        TeamList.Add(new Team() 
                        {
                            id = Reader.GetInt32(Reader.GetOrdinal("id")),
                            name = Reader.GetString(Reader.GetOrdinal("teamNavn")),
                            leder = (Reader.GetString(Reader.GetOrdinal("firstname")) + " " + Reader.GetString(Reader.GetOrdinal("lastname"))),
                        });
                    }
                    Reader.Close();
                }
            }
            con.Close();
            return TeamList;
        }


        /// <summary>
        /// ShowOpretTeamPage metoden sætter en nye source i MainWindow contentHolder frame. 
        /// Metoden tager 1 parameter.
        /// </summary>
        /// <param name="window">window er en instans af MainWindow</param>
        public static void ShowOpretTeamPage(MainWindow window)
        {
            window.contentHolder.Source = new Uri("/View/page/opretTeam.xaml", UriKind.Relative);
        }


        /// <summary>
        /// DeleteTeam metoden sletter det valgte team i Databasen, teamet der boliver sletted bliver valgt i TeamListGrid Datagrid i teamList pagen. 
        /// Metoden tager 2 parameter
        /// </summary>
        /// <param name="window">window er en instans af teamList page</param>
        /// <param name="mainWindow">window er en instans af MainWindow </param>
        public static void DeleteTeam(TeamList window, MainWindow mainWindow)
        {
            MySqlConnection con = GetConnection();
            Team dataRow = window.TeamListGrid.SelectedItem as Team;
            con.Open();
            

            //messagebox
            
            if (MessageBox.Show("Do you want to close this window?", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                DeleteTeamBindings( dataRow, con);
                string sqlQuery = "DELETE FROM team WHERE id = '" + dataRow.id + "'";
                MySqlCommand cmd = new MySqlCommand(sqlQuery, con);
                cmd.ExecuteNonQuery();
                con.Close();
                //mainWindow.contentHolder.Source = new Uri("/View/page/teamList.xaml", UriKind.Relative);
                mainWindow.contentHolder.NavigationService.Refresh();
            }
            else
            {
                mainWindow.contentHolder.NavigationService.Refresh();
            }

            
        }


        /// <summary>
        /// DeleteTeamBindings sletter bendingen mellem teams og medarbejder i Databasen
        /// Metoden tager 2 parameter 
        /// </summary>
        /// <param name="dataRow"> dataRow obeject af modellen team </param>
        /// <param name="con">con er obejct af Database Connection</param>
        private static void DeleteTeamBindings(dynamic dataRow, MySqlConnection con)
        {
            string sqlQuery = "DELETE FROM employee_team WHERE team_id = '" + dataRow.id + "'";
            MySqlCommand cmd = new MySqlCommand(sqlQuery, con);
            cmd.ExecuteNonQuery();
        }

    }
}
