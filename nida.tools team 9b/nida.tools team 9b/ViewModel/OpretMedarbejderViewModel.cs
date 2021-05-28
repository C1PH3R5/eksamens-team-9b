using System;
using System.Collections.Generic;
using System.Text;
using nida.tools_team_9b.View.page;
using nida.tools_team_9b.Database;
using MySql.Data.MySqlClient;
using nida.tools_team_9b.Model;
using System.Windows.Controls;


namespace nida.tools_team_9b.ViewModel
{
    
    public class OpretMedarbejderViewModel : DBCon
    {
        /// <summary>
        /// denne metode henter alle team ned og pakker det ned i list af typen Team
        /// </summary>
        /// <param name="window"> window er en instans af opretMedarbejder pagen</param>
        /// <returns> returner listen af team til bomboboxen i opretMedarbejder pagen</returns>
        public static List<Team> GetTeamList(opretMedarbejder window)
        {
            List<Team> TeamList = new List<Team>();
            MySqlConnection con = GetConnection();
            con.Open();
            string sqlQuery = "SELECT teamNavn, id FROM team;";
            MySqlCommand cmd = new MySqlCommand(sqlQuery, con);

            using (MySqlDataReader Reader = cmd.ExecuteReader())
            {
                if (Reader.HasRows)
                {
                    while (Reader.Read())
                    {
                        //I would also check for DB.Null here before reading the value.
                        TeamList.Add(new Team()
                        {
                            name = Reader.GetString(Reader.GetOrdinal("teamNavn")),

                        });
                    }
                    Reader.Close();
                }
            }
            con.Close();
            window.vælgTeam.ItemsSource = TeamList;
            window.vælgTeam.DisplayMemberPath = "name";
            return null;

        }
        /// <summary>
        /// metoden her opretter nye medarbejder i databasen
        /// </summary>
        /// <param name="window"> window er en instans af opretMedarbejder page</param>
        public static void OpretMedarbejderData(opretMedarbejder window)
        {

            MySqlConnection con = GetConnection();
            string sqlQuery = "INSERT INTO employee SET " +
                "roleid = 3," +
                " userid = '" + window.Navn.Text.Substring(0, 2) + window.Efternavn.Text.Substring(0, 2) + "'," +
                " firstname = '"+ window.Navn.Text +"'," +
                " lastname = '"+ window.Efternavn.Text +"'," +
                " email = '"+ window.Email.Text +"'," +
                " arbejdesemail = '"+ window.arbejdesEmail.Text + "'," +
                " adresse= '"+ window.adresse.Text+"'," +
                " city = '"+ window.By.Text+"'," +
                " postnr = "+ window.Postnr.Text+"," +
                " workinghours = 7;";
            MySqlCommand cmd = new MySqlCommand(sqlQuery, con);
            con.Open();
            cmd.ExecuteNonQuery();
            int medarbejderId = GetMedarbejderId(window, con);
            SetTeam(window, con, medarbejderId);
            OpretKontaktData(window, con, medarbejderId);
            SetPasswordStatus(window, con, medarbejderId);
            generePassword(window, con, medarbejderId);
            con.Close();
        }

        /// <summary>
        /// denne metode sætter linket mellem teamet og medarbejderen 
        /// </summary>
        /// <param name="window"> window er en instans af opretMedarbejder pagen</param>
        /// <param name="con">con er obejct af Database Connection</param>
        /// <param name="medarbejderId"> er den nye medarbejders id </param>
        private static void SetTeam(opretMedarbejder window, MySqlConnection con, int medarbejderId)
        {

            
            string sqlQuery = "INSERT INTO employee_team SET team_id = "+ GetTeamId(window, con) +", employee_id = "+ medarbejderId + ";";
            MySqlCommand cmd = new MySqlCommand(sqlQuery, con);
            cmd.ExecuteNonQuery();

        }

        private static int GetTeamId(opretMedarbejder window, MySqlConnection con)
        {
            string sqlQuery = "SELECT id FROM team WHERE teamNavn = '"+ window.vælgTeam.Text +"';";
            MySqlCommand cmd = new MySqlCommand(sqlQuery, con);
            int teamId = 0;
            using (MySqlDataReader Reader = cmd.ExecuteReader())
            {
                if (Reader.HasRows)
                {
                    while (Reader.Read())
                    {
                        teamId = Reader.GetInt32(Reader.GetOrdinal("id"));
                    }
                    Reader.Close();
                }
            }
            return teamId;
        }
        /// <summary>
        /// metoden her opretter alle kontakt dater i databasen til den nye medarbejder
        /// </summary>
        /// <param name="window"> window er en instans af opretMedarbejder pagen</param>
        /// <param name="con">con er obejct af Database Connection</param>
        /// <param name="medarbejderId"> er den nye medarbejders id </param>
        private static void OpretKontaktData(opretMedarbejder window, MySqlConnection con, int medarbejderId)
        {
            string sqlQuery = null;
            if (window.hjemmeTlf.Text != "Hjemme")
            {
                sqlQuery = "INSERT INTO phoneNumber SET employee_id = " + medarbejderId + ", TYPE = 'Home', number = "+ window.hjemmeTlf.Text + ";";
                MySqlCommand cmd = new MySqlCommand(sqlQuery, con);
                cmd.ExecuteNonQuery();
            }
            if (window.mobil.Text != "Mobil")
            {
                sqlQuery = "INSERT INTO phoneNumber SET employee_id = " + medarbejderId + ", TYPE = 'MobilePhone', number = "+ window.mobil.Text + ";";
                MySqlCommand cmd = new MySqlCommand(sqlQuery, con);
                cmd.ExecuteNonQuery();
            }
            if (window.arbejds.Text != "Arbejde")
            {
                sqlQuery = "INSERT INTO phoneNumber SET employee_id = " + medarbejderId + ", TYPE = 'Work', number = "+ window.arbejds.Text + ";";
                MySqlCommand cmd = new MySqlCommand(sqlQuery, con);
                cmd.ExecuteNonQuery();
            }
        }
        /// <summary>
        /// metoden her opretter og sætter password status til aktiv
        /// </summary>
        /// <param name="window"> window er en instans af opretMedarbejder pagen</param>
        /// <param name="con">con er obejct af Database Connection</param>
        /// <param name="medarbejderId"> er den nye medarbejders id </param>
        private static void SetPasswordStatus(opretMedarbejder window, MySqlConnection con, int medarbejderId)
        {
            
            string sqlQuery = "INSERT INTO password SET employee_id = "+ medarbejderId + ", passwordStatus = 'aktiv', invalidAttemps = 0;";
            MySqlCommand cmd = new MySqlCommand(sqlQuery, con);
            cmd.ExecuteNonQuery();
            
        }
        /// <summary>
        /// metoden her genere password ud fra navn og efternavn
        /// </summary>
        /// <param name="window"> window er en instans af opretMedarbejder pagen</param>
        /// <param name="con">con er obejct af Database Connection</param>
        /// <param name="medarbejderId"> er den nye medarbejders id </param>
        private static void generePassword(opretMedarbejder window, MySqlConnection con, int medarbejderId)
        {
            
            string sqlQuery = "INSERT INTO passwordHistory SET " +
                "password_id = " + FindPasswordStatusId(con, medarbejderId) + "," +
                " password = 'NidaPW"+ window.Navn.Text.Substring(0, 2) + window.Efternavn.Text.Substring(0, 2) + "'," +
                " time = '"+ DateTime.Now.ToString("yyyyMMddHHmmss") + "';";

            MySqlCommand cmd = new MySqlCommand(sqlQuery, con);
            cmd.ExecuteNonQuery();

        }
        /// <summary>
        /// metoden finder password status iden
        /// </summary>
        /// <param name="con">con er obejct af Database Connection</param>
        /// <param name="medarbejderId"> er den nye medarbejders id </param>
        /// <returns>returner password status id </returns>
        private static int FindPasswordStatusId(MySqlConnection con, int medarbejderId)
        {

            string sqlQuery = "SELECT id FROM password WHERE employee_id = '"+ medarbejderId +"'";
            MySqlCommand cmd = new MySqlCommand(sqlQuery, con);
            int passwordStatusId = 0;
            using (MySqlDataReader Reader = cmd.ExecuteReader())
            {
                if (Reader.HasRows)
                {
                    while (Reader.Read())
                    {
                        passwordStatusId = Reader.GetInt32(Reader.GetOrdinal("id"));
                    }
                    Reader.Close();
                }
            }
            return passwordStatusId;
        }
        /// <summary>
        /// henter id'en på den nye medarbejder
        /// </summary>
        /// <param name="window"> window er en instans af opretMedarbejder pagen</param>
        /// <param name="con">con er obejct af Database Connection</param>
        /// <returns> returner id'en på den nye medarbejder</returns>
        private static int GetMedarbejderId(opretMedarbejder window, MySqlConnection con)
        {
            string sqlQuery = "SELECT id FROM employee WHERE firstname = '"+ window.Navn.Text + "' AND lastname = '"+ window.Efternavn.Text + "';";
            MySqlCommand cmd = new MySqlCommand(sqlQuery, con);
            int medarbejderId = 0;
            using (MySqlDataReader Reader = cmd.ExecuteReader())
            {
                if (Reader.HasRows)
                {
                    while (Reader.Read())
                    {
                        medarbejderId = Reader.GetInt32(Reader.GetOrdinal("id"));
                    }
                    Reader.Close();
                }
            }
            return medarbejderId;
        }
        /// <summary>
        /// metoden anuller opretlsen af medarbejderen og sender en tilbage til medarbejderList
        /// </summary>
        /// <param name="window">window er en instans af MainWindow</param>
        public static void anuller(MainWindow window)
        {
            window.contentHolder.Source = new Uri("/View/page/medarbejderList.xaml", UriKind.Relative);
        }
    }
}
//MySqlConnection con = GetConnection();
//string sqlQuery = "INSERT INTO employee SET roleid = 1, userid = "", firstname = "", lastname = "", email = "", arbejdesemail = "", adresse= "", city = "", postnr = 6000, workinghours = "";";
//MySqlCommand cmd = new MySqlCommand(sqlQuery, con);
//con.Open();
//cmd.ExecuteNonQuery();
//con.Close();