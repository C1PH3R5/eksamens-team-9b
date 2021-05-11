using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using nida.tools_team_9b.Model;
using nida.tools_team_9b.Database;
using MySql.Data.MySqlClient;
using nida.tools_team_9b.View.window;


namespace nida.tools_team_9b.ViewModel
{
    public class LogindViewModel : DBCon
    {
        public static void LogIndServices(string brugerNavn, string passWordStr, LogIndWindow logIndWindow)
        {
            MySqlConnection con = GetConnection();
            if (validatePaasWord(brugerNavn, passWordStr, logIndWindow, con))
            {
                SetGrobalProperties(brugerNavn, con);
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                logIndWindow.Close();
            }
        }

        private static void SetGrobalProperties(string brugerNavn, MySqlConnection con)
        {
            
            string sqlQuery = "SELECT roleid FROM NidaTools.employee WHERE userid = '" + brugerNavn + "';";
            con.Open();
            MySqlCommand cmd = new MySqlCommand(sqlQuery, con);

            using (MySqlDataReader Reader = cmd.ExecuteReader())
            {
                if (Reader.HasRows)
                {
                    Reader.Read();
                    Application.Current.Properties["Global_userId"] = brugerNavn;
                    Application.Current.Properties["Global_userRole"] = Reader.GetInt32(Reader.GetOrdinal("roleid"));
                }
            }
            con.Close();
            
        }

        private static bool validatePaasWord(string brugerNavn, string passWordStr, LogIndWindow logIndWindow, MySqlConnection con)
        {
            
            con.Open();
            LogInd passWord = GetPassWord(brugerNavn, con);

            if (!(passWord == null))
            {
                return TjekStatus(passWordStr, logIndWindow, con, passWord);
            }
            else
            {
                SetErrorMessage(logIndWindow, "Forkert Brugernavn!");
                return false;
            }
            

        }

        private static bool TjekStatus(string passWordStr, LogIndWindow logIndWindow, MySqlConnection con, LogInd passWord)
        {
            if (passWord.passwordStatus.Equals("aktiv"))
            {
                string CurrentPassWord = GetCurrentPassWord(passWord.id, con);
                con.Close();
                if (passWordStr.Equals(CurrentPassWord))
                {
                    ResetInValidAttemps(con, passWord);
                    return true;
                }
                else
                {
                    SetErrorMessage(logIndWindow, "Forkert Password!");
                    invalidAttempsHandler(passWord, con);
                    return false;
                }
            }
            else
            {
                SetErrorMessage(logIndWindow, "Din konto er " + passWord.passwordStatus + ". Kontakt Admin!");
                return false;
            }
        }

        private static void ResetInValidAttemps(MySqlConnection con, LogInd passWord)
        {
            string sqlQuery = "UPDATE password SET invalidAttemps =  0 WHERE id = " + passWord.id;
            MySqlCommand cmd = new MySqlCommand(sqlQuery, con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        private static void SetErrorMessage(LogIndWindow logIndWindow, string message)
        {
            logIndWindow.message.Text = message;
            logIndWindow.messageImage.Visibility = Visibility.Visible;
        }


        private static void invalidAttempsHandler(LogInd passWord, MySqlConnection con)
        {
            string sqlQuery = "UPDATE password SET invalidAttemps = " + (passWord.invalidAttemps + 1);
            if ((passWord.invalidAttemps + 1) == 3)
            {
                sqlQuery = sqlQuery + ", passwordStatus = 'blocked'";

            }
            sqlQuery = sqlQuery + " WHERE id = " + passWord.id;
            MySqlCommand cmd = new MySqlCommand(sqlQuery, con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        private static LogInd GetPassWord(string brugerNavn, MySqlConnection con)
        {
            LogInd passWord = null;
            string sqlQuery = "SELECT pw.id, pw.passwordstatus, pw.employee_id, pw.invalidattemps FROM employee em JOIN password pw ON em.id = pw.employee_id WHERE userid = '" + brugerNavn + "';";
            MySqlCommand cmd = new MySqlCommand(sqlQuery, con);
            using (MySqlDataReader Reader = cmd.ExecuteReader())
            {
                if (Reader.HasRows)
                {
                    passWord = new LogInd();
                    while (Reader.Read())
                    {
                        passWord.id = Reader.GetInt32(Reader.GetOrdinal("id"));
                        passWord.passwordStatus = Reader.GetString(Reader.GetOrdinal("passwordstatus"));
                        passWord.employeeId = Reader.GetInt32(Reader.GetOrdinal("Employee_id"));
                        passWord.invalidAttemps = Reader.GetInt32(Reader.GetOrdinal("invalidAttemps"));
                    }
                    Reader.Close();
                }
            }
            
            return passWord;
        }
        private static string GetCurrentPassWord(int passWordId, MySqlConnection con)
        {
            string sqlQuery = "select password_id, password, time from passwordHistory where (password_id, time) in ( select password_id, max(time) from passwordHistory where password_id = " + passWordId + " group by password_id );";
            MySqlCommand cmd = new MySqlCommand(sqlQuery, con);
            string currenPassWord = null;
            using (MySqlDataReader Reader = cmd.ExecuteReader())
            {
                if (Reader.HasRows)
                {
                    while (Reader.Read())
                    {
                        currenPassWord = Reader.GetString(Reader.GetOrdinal("password"));
                    }
                    Reader.Close();
                }
            }
            return currenPassWord;

        }
        public static void Anuller(LogIndWindow window) {
            window.Close();
        }
    }
}
