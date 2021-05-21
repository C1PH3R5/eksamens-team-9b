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

    public class TeamListViewModel : DBCon
    {
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
        public static void ShowOpretTeamPage(MainWindow window)
        {
            window.contentHolder.Source = new Uri("/View/page/opretTeam.xaml", UriKind.Relative);
        }

        public static void DeleteTeam(TeamList window, MainWindow mainWindow)
        {
            MySqlConnection con = GetConnection();
            Team dataRow = window.TeamListGrid.SelectedItem as Team;
            con.Open();
            

            //messagebox

            if (MessageBox.Show("Do you want to close this window?", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                DeleteTeamBindings(window, dataRow, con);
                string sqlQuery = "DELETE FROM team WHERE id = '" + dataRow.id + "'";
                MySqlCommand cmd = new MySqlCommand(sqlQuery, con);
                cmd.ExecuteNonQuery();
                con.Close();
                //mainWindow.contentHolder.Source = new Uri("/View/page/teamList.xaml", UriKind.Relative);
                mainWindow.contentHolder.NavigationService.Refresh();
            }
            else
            {
                // Do not close the window  
            }

            
        }

        private  static void DeleteTeamBindings(TeamList window, dynamic dataRow, MySqlConnection con)
        {
            string sqlQuery = "DELETE FROM employee_team WHERE team_id = '" + dataRow.id + "'";
            MySqlCommand cmd = new MySqlCommand(sqlQuery, con);
            cmd.ExecuteNonQuery();
        }

    }
}
