using System;
using System.Collections.Generic;
using System.Text;
using nida.tools_team_9b.View.page;
using nida.tools_team_9b.Database;
using MySql.Data.MySqlClient;
using nida.tools_team_9b.Model;


namespace nida.tools_team_9b.ViewModel
{
    public class OpretTeamViewModel : DBCon
    {
        /// <summary>
        /// metoden opretter et nyt team i databasen
        /// </summary>
        /// <param name="window">window er en instans af OpretTeam pagen</param>
        /// <param name="mainwindow"> mmainwindow er en instans af MainWindow</param>
        public static void OpretTeamData(opretTeam window, MainWindow mainwindow)
        {
            MySqlConnection con = GetConnection();
            String sqlQuery = "INSERT INTO team SET teamNavn ='" + window.teamNavn.Text + "';";

            MySqlCommand cmd = new MySqlCommand(sqlQuery, con);
            con.Open();
            cmd.ExecuteNonQuery();
            SetTeamLeder(window, con);
            con.Close();
            mainwindow.contentHolder.Source = new Uri("/View/page/teamList.xaml", UriKind.Relative);

        }
        /// <summary>
        /// metoden sætter en leder til det nye team
        /// </summary>
        /// <param name="window"> window er en instan af OretTeam pagen</param>
        /// <param name="con">con er obejct af Database Connection</param>
        public static void SetTeamLeder(opretTeam window, MySqlConnection con)
        {
            string SqlQuery = "INSERT INTO employee_team SET team_id = "+ GetTeamId(window, con) +", employee_id = "+ GetEmployeeId(window, con) +";";

            MySqlCommand cmd = new MySqlCommand(SqlQuery, con);
            cmd.ExecuteNonQuery();
        }
        /// <summary>
        /// metoden henter det teams id fra databasen
        /// </summary>
        /// <param name="window"> window er en instan af OretTeam pagen</param>
        /// <param name="con">con er obejct af Database Connection</param>
        /// <returns>returner id'en fra det nye team </returns>
        public static int GetTeamId(opretTeam window, MySqlConnection con)
        {
            string sqlQuery = "SELECT id FROM team WHERE teamNavn = '"+ window.teamNavn.Text +"';";
            MySqlCommand cmd = new MySqlCommand(sqlQuery, con);

            int teamId = 0;

            using (MySqlDataReader Reader = cmd.ExecuteReader())
            {
                if (Reader.HasRows)
                {
                    while (Reader.Read())
                    {
                        //I would also check for DB.Null here before reading the value.
                        teamId = Reader.GetInt32(Reader.GetOrdinal("id"));
                    }
                    Reader.Close();
                }
            }

            return teamId;
        }
        /// <summary>
        /// henter id'en fra leder der skal være på det nye team
        /// </summary>
        /// <param name="window"> window er en instan af OretTeam pagen</param>
        /// <param name="con">con er obejct af Database Connection</param>
        /// <returns>returner leder id </returns>
        public static int GetEmployeeId(opretTeam window, MySqlConnection con)
        {
            string sqlQuery = "SELECT id FROM employee WHERE firstname ='"+ window.vælgTeamLeder.Text.Split(' ')[0] +"' AND lastname ='"+ window.vælgTeamLeder.Text.Split(' ')[1]+"';";
            MySqlCommand cmd = new MySqlCommand(sqlQuery, con);

            int lederId = 0;

            using (MySqlDataReader Reader = cmd.ExecuteReader())
            {
                if (Reader.HasRows)
                {
                    while (Reader.Read())
                    {
                        //I would also check for DB.Null here before reading the value.
                        lederId = Reader.GetInt32(Reader.GetOrdinal("id"));
                    }
                    Reader.Close();
                }
            }

            return lederId;
        }
        /// <summary>
        /// denne metode henter alle leder ned fra databasen
        /// </summary>
        /// <param name="window"> window er en instans af opretteam pagen</param>
        /// <returns>returner en list af leder til comboboxen </returns>
        public static List<Team> GetTeamlederList(opretTeam window)
        {
            MySqlConnection con = GetConnection();
            con.Open();
            string sqlQuery = "SELECT firstname, lastname, id FROM employee WHERE roleid = 2;";
            MySqlCommand cmd = new MySqlCommand(sqlQuery, con);

            List<Team> lederList = new List<Team>();

            using(MySqlDataReader Reader = cmd.ExecuteReader())
            {
                if (Reader.HasRows)
                {
                    while (Reader.Read())
                    {
                        lederList.Add(new Team()
                        {
                            lederId = Reader.GetInt32(Reader.GetOrdinal("id")),
                            name = (Reader.GetString(Reader.GetOrdinal("firstname")) + " " + Reader.GetString(Reader.GetOrdinal("lastname")))

                        });
                    } 
                    Reader.Close();
                }
            }
            window.vælgTeamLeder.ItemsSource = lederList;
            window.vælgTeamLeder.SelectedValue = "id";
            window.vælgTeamLeder.DisplayMemberPath = "name";
            return lederList;
        }
        /// <summary>
        /// metoden anuller opretelsen af teamet og sender en tilbage til TeamList pagen
        /// </summary>
        /// <param name="window">window er en instans af MainWindow</param>
        public static void anuller(MainWindow window)
        {
            window.contentHolder.Source = new Uri("/View/page/teamList.xaml", UriKind.Relative);
        }
    }
        
}