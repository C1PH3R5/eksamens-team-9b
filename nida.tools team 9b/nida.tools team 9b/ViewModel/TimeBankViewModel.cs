using System;
using System.Collections.Generic;
using System.Text;
using nida.tools_team_9b.Model;
using nida.tools_team_9b.Database;
using MySql.Data.MySqlClient;


namespace nida.tools_team_9b.ViewModel
{
    public class TimeBankViewModel : DBCon
    {
        public static List<TimeBank> getTimeBankList(string fra, string til)
        {
            List<TimeBank> timeBankListe = new List<TimeBank>();
            List<TimeBank> timeBankTjekIndListe = new List<TimeBank>();
            List<TimeBank> timeBankTjekUdListe = new List<TimeBank>();

            MySqlConnection con = GetConnection();
            con.Open();
            string sqlQuery = "SELECT firstname, lastname, employee_id, type, time from timeBank INNER JOIN employee ON timeBank.employee_id = employee.id WHERE time BETWEEN " + fra + " AND " + til + "";

            MySqlCommand cmd = new MySqlCommand(sqlQuery, con);
            using (MySqlDataReader Reader = cmd.ExecuteReader())
            {
                if (Reader.HasRows)
                {
                    while (Reader.Read())
                    {
                        if (Reader.GetString(Reader.GetOrdinal("type")) == "ind")
                        {
                            timeBankTjekIndListe.Add(new TimeBank()
                            {
                                medarbejderNavn = Reader.GetString(Reader.GetOrdinal("firstname")) + " " + Reader.GetString(Reader.GetOrdinal("lastname")),
                                type = Reader.GetString(Reader.GetOrdinal("type")),
                                medarbejderId = Reader.GetInt32(Reader.GetOrdinal("employee_id")),
                                time = Reader.GetDateTime(Reader.GetOrdinal("time")),
                            });
                        }
                        else
                        {
                            timeBankTjekUdListe.Add(new TimeBank()
                            {
                                medarbejderNavn = Reader.GetString(Reader.GetOrdinal("firstname")) + " " + Reader.GetString(Reader.GetOrdinal("lastname")),
                                type = Reader.GetString(Reader.GetOrdinal("type")),
                                medarbejderId = Reader.GetInt32(Reader.GetOrdinal("employee_id")),
                                time = Reader.GetDateTime(Reader.GetOrdinal("time")),
                            });
                        }


                    }
                    Reader.Close();
                }
            }
            con.Close();
            return timeBankTjekUdListe;

        }

    }

}
