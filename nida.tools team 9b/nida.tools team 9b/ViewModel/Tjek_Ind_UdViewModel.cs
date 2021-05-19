using System;
using System.Collections.Generic;
using System.Text;
using nida.tools_team_9b.Database;
using MySql.Data.MySqlClient;
using System.Windows;
using nida.tools_team_9b.View.page;

namespace nida.tools_team_9b.ViewModel
{
    public class Tjek_Ind_UdViewModel : DBCon
    {
        public static void ShowStempleSystem(Tjek_ind_ud window)
        {
            MySqlConnection con = GetConnection();
            string sqlQuery = "SELECT userid, type from timeBank INNER JOIN employee ON timeBank.employee_id = employee.id WHERE time BETWEEN '" + DateTime.Today.ToString("yyyy/MM/dd") + " 00:00:00' AND '" + DateTime.Today.ToString("yyyy/MM/dd") + " 23:59:59' And userid = '" + Application.Current.Properties["Global_userId"] + "' ORDER BY timeBank.time DESC LIMIT 1";
            con.Open();
                MySqlCommand cmd = new MySqlCommand(sqlQuery, con);
                using (MySqlDataReader Reader = cmd.ExecuteReader())
                {
                    if (Reader.HasRows)
                    {
                        Reader.Read();
                        tjekType(Reader, window);
                    }
                    else
                    {
                    showStempleButtons(window, "Tjek Ind");
                    }
                }
            con.Close();
        }
        private static void tjekType(MySqlDataReader Reader, Tjek_ind_ud window)
        {
            switch (Reader.GetString(Reader.GetOrdinal("type")))
            {
                case "ind":
                    showStempleButtons(window, "Tjek Ud");
                    break;
                case "ud":
                    showStempleButtons(window, "Tjek Ind");
                    break;

            }
        }
        private static void showStempleButtons(Tjek_ind_ud window, string btnText)
        {
            if (btnText == "Tjek Ind")
            {
                window.StempleButton.Content = btnText;
                window.KommentarBox.Visibility = Visibility.Hidden;
            }
            else
            {
                window.StempleButton.Content = btnText;
                window.KommentarBox.Visibility = Visibility.Visible;
            }

        }
        public static void TjekIndUd(Tjek_ind_ud window)
        {
            MySqlConnection con = GetConnection();
            switch (window.StempleButton.Content)
            {
                case "Tjek Ind":
                    stempleSystemAction("ind", GetEmployeeId(con), con);
                    break;

                case "Tjek Ud":
                    stempleSystemAction("ud", GetEmployeeId(con), con);
                    break;

            }
            con.Close();
        }
        private static void stempleSystemAction(string action, int brugerId, MySqlConnection con)
        {
            
            string sqlQuery = "INSERT INTO timeBank SET employee_id = " + brugerId + ", type = '" + action + "', time = '" + DateTime.Now.ToString("yyyyMMddHHmmss") + "';";
            con.Open();
            MySqlCommand cmd = new MySqlCommand(sqlQuery, con);
            cmd.ExecuteNonQuery();
            con.Close();
        }
        private static int GetEmployeeId(MySqlConnection con)
        {   
            string sqlQuery = "SELECT id FROM NidaTools.employee WHERE userid = '" + Application.Current.Properties["Global_userId"] + "';";
            con.Open();
            MySqlCommand cmd = new MySqlCommand(sqlQuery, con);
            int brugerId = 0;
            using (MySqlDataReader Reader = cmd.ExecuteReader())
            {
                Reader.Read();
                
                brugerId =  Reader.GetInt32(Reader.GetOrdinal("id"));
            }
            con.Close();
            return brugerId;

        }
    }  
}
