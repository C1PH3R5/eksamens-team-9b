using System;
using System.Collections.Generic;
using System.Text;
using nida.tools_team_9b.Model;
using nida.tools_team_9b.Database;
using MySql.Data.MySqlClient;
using nida.tools_team_9b;
using System.Windows;
using nida.tools_team_9b.View.page;


namespace nida.tools_team_9b.ViewModel
{
    public class MedarbejderListViewModel : DBCon
    {
        /// <summary>
        /// denne metoden vis vilken knapper der skal vise efter vilken bruger der logget ind og vilke rolle de har 
        /// </summary>
        /// <param name="window">window er en instans af medarbejderList page</param>
        public static void ShowMedarbejderListButtons(MedarbejderList window)
        {
            string roleId = Application.Current.Properties["Global_userRole"].ToString();
            int convertedRoleId = Convert.ToInt32(roleId);
            if (convertedRoleId < 3)
            {
                window.opretMedarbejder.Visibility = Visibility.Visible;
                window.fjernMedarbejder.Visibility = Visibility.Visible;
                window.redigereMedarbejder.Visibility = Visibility.Visible;
            }
        }
        /// <summary>
        /// denne metoden henter alle med arbejder der har et team og viser dem i medarbejderlist pagen
        /// </summary>
        /// <param name="window">window er en instans af medarbejderList page</param>
        /// <returns>returner en list til datagrid i medarbjderlist pagen</returns>
        public static List<Medarbejder> GetMedarbejder(MedarbejderList window)
            {
                window.medarbejderList.Columns[0].Visibility = Visibility.Hidden;
                List<Medarbejder> medarbejderList = new List<Medarbejder>();
                MySqlConnection con = GetConnection();
                con.Open();
                string sqlQuery = "SELECT " +
                        "employee.id," +
                        " employee.roleid," +
                        " employee.userid," +
                        " employee.firstname," +
                        " employee.lastname," +
                        " employee.email," +
                        " employee.workinghours," +
                        " team.teamNavn" +
                    " FROM" +
                        " ((employee " +
                        "INNER JOIN employee_team ON employee.id = employee_team.employee_id) " +
                        "INNER JOIN team ON employee_team.team_id = team.id) " +
                    "WHERE " +
                        "employee.roleid = 3;";

                MySqlCommand cmd = new MySqlCommand(sqlQuery, con);
                using (MySqlDataReader Reader = cmd.ExecuteReader())
                {
                    if (Reader.HasRows)
                    {
                        while (Reader.Read())
                        {
                            //I would also check for DB.Null here before reading the value.
                            medarbejderList.Add(new Medarbejder()
                            {
                                id = Reader.GetInt32(Reader.GetOrdinal("id")),
                                roleId = Reader.GetInt32(Reader.GetOrdinal("roleid")),
                                userId = Reader.GetString(Reader.GetOrdinal("userid")),
                                navn = Reader.GetString(Reader.GetOrdinal("firstname")),
                                efternavn = Reader.GetString(Reader.GetOrdinal("lastname")),
                                email = Reader.GetString(Reader.GetOrdinal("email")),
                                workinghours = Reader.GetFloat(Reader.GetOrdinal("workinghours")),
                                team = Reader.GetString(Reader.GetOrdinal("teamNavn")),
                                fullnavn = (Reader.GetString(Reader.GetOrdinal("firstname")) + " " + Reader.GetString(Reader.GetOrdinal("lastname")))
                            });
                        }
                        Reader.Close();
                    }
                }
            con.Close();
            return medarbejderList;
            }
        /// <summary>
        /// metoden sætter source på contentholder framen   
        /// </summary>
        /// <param name="window"> window er en instans af mainwindow</param>
        public static void ShowOpretMedarbejderPage(MainWindow window)
        {

            //Window mainWindow = this.MainWindow;

            window.contentHolder.Source = new Uri("/View/page/opretMedarbejder.xaml", UriKind.Relative);
        }
    }
}
