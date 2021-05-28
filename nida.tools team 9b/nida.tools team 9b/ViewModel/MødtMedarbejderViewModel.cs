using System;
using System.Collections.Generic;
using System.Text;
using nida.tools_team_9b.Model;
using nida.tools_team_9b.Database;
using MySql.Data.MySqlClient;

namespace nida.tools_team_9b.ViewModel
{
    public class MødtMedarbejderViewModel : DBCon
    {
        /// <summary>
        /// LoadMedarbjderDataSet henter dataen ned fra databasen og gemmer den i en liste af timebank 
        /// </summary>
        /// <returns>metoden returner liste af timebank</returns>
        public static List<TimeBank> LoadMedarbjderDataSet()
        {
            List<TimeBank> MødtMedarbejder = new List<TimeBank>();

            MySqlConnection con = GetConnection();
            con.Open();
            string sqlQuery = "SELECT firstname, lastname, comment, type, time, comment from timeBank INNER JOIN employee ON timeBank.employee_id = employee.id WHERE time BETWEEN '"+ DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00' AND '" + DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59'";
            MySqlCommand cmd = new MySqlCommand(sqlQuery, con);

            using (MySqlDataReader Reader = cmd.ExecuteReader())
            {
                if (Reader.HasRows)
                {
                    while (Reader.Read())
                    {
                        MødtMedarbejder.Add(new TimeBank()
                        {
                            type = Reader.GetString(Reader.GetOrdinal("type")),
                            medarbejderNavn = (Reader.GetString(Reader.GetOrdinal("firstname")) + " " + Reader.GetString(Reader.GetOrdinal("lastname"))),
                            time = Reader.GetDateTime(Reader.GetOrdinal("time")),
                        });
                    }
                }
                Reader.Close();
            }

                con.Close();
            return MødtMedarbejder;
        }
    }
}
