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
        /// <summary>
        /// denne tjeker vilken type af  tjek ind der er sidst er regsiteret, og bestemmer vilken knap der skal vises 
        /// </summary>
        /// <param name="window">window er en instans af Tjek_ind_ud pagen</param>
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
        /// <summary>
        /// denne metode  tjeker vilken type af  tjek ind der er sidst er regsiteret og vægler vilken parameter der skal sendes med videre i showStempleButtons
        /// </summary>
        /// <param name="Reader"> reader inholder alle data der er trukket ned fra databasen </param>
        /// <param name="window">window er en instans af Tjek_ind_ud pagen</param>
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
        /// <summary>
        /// metoden skifter texten på knapper på pagen efter og man er tjeket ind eller ej
        /// </summary>
        /// <param name="window">window er en instans af Tjek_ind_ud pagen</param>
        /// <param name="btnText"> er texten der bliver vist på pagen</param>
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

        /// <summary>
        /// metoden ind sætter i data basen om man tjekker ind eller ud 
        /// </summary>
        /// <param name="window">window er en instans af Tjek_ind_ud pagen</param>
        /// <param name="mainWindow"> mainwindow er en instans af Mainwindow</param>
        public static void TjekIndUd(Tjek_ind_ud window, MainWindow mainWindow)
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
            mainWindow.contentHolder.NavigationService.Refresh();
        }
        /// <summary>
        /// indsætter hvilken type af tejk ind eller ud i databsen 
        /// </summary>
        /// <param name="action">actio er type afom man er tejkket ind eller ud </param>
        /// <param name="brugerId"> brugen id der der logget id</param>
        /// <param name="con">con er obejct af Database Connection</param>
        private static void stempleSystemAction(string action, int brugerId, MySqlConnection con)
        {
            
            string sqlQuery = "INSERT INTO timeBank SET employee_id = " + brugerId + ", type = '" + action + "', time = '" + DateTime.Now.ToString("yyyyMMddHHmmss") + "';";
            con.Open();
            MySqlCommand cmd = new MySqlCommand(sqlQuery, con);
            cmd.ExecuteNonQuery();
            con.Close();

        }
        /// <summary>
        /// henter den medarbjeder der er logget inds id
        /// </summary>
        /// <param name="con">con er obejct af Database Connection</param>
        /// <returns>returner iden på brugeren der er logget ind</returns>
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
