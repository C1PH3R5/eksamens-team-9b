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
        public static List<TimeBank> GetTimeBankList(string til, string fra)
        {
            List<TimeBank> timeBankListe = new List<TimeBank>();
            Dictionary<int, TimeSpan> tempMedarbejderList = new Dictionary<int, TimeSpan>();

            MySqlConnection con = GetConnection();
            con.Open();
            string sqlQuery = "SELECT firstname, lastname, employee_id, type, time from timeBank INNER JOIN employee ON timeBank.employee_id = employee.id WHERE time BETWEEN '" + fra + "' AND '" + til + "'";
            MySqlCommand cmd = new MySqlCommand(sqlQuery, con);

            using (MySqlDataReader Reader = cmd.ExecuteReader())
            {
                if (Reader.HasRows)
                {
                    while (Reader.Read())
                    {
                        DateTime fratid = Reader.GetDateTime(Reader.GetOrdinal("time"));
                        int lastId = Reader.GetInt32(Reader.GetOrdinal("employee_id"));

                        if (Reader.Read() && lastId == Reader.GetInt32(Reader.GetOrdinal("employee_id")))
                        {
                            TimeSpan UdragenetTid = Reader.GetDateTime(Reader.GetOrdinal("time")).Subtract(fratid);
                            if (tempMedarbejderList.ContainsKey(Reader.GetInt32(Reader.GetOrdinal("employee_id"))))
                            {
                                tempMedarbejderList[Reader.GetInt32(Reader.GetOrdinal("employee_id"))] += UdragenetTid;
                            }
                            else
                            {
                                tempMedarbejderList.Add(Reader.GetInt32(Reader.GetOrdinal("employee_id")), UdragenetTid);
                            }
                        }

                    }
                    Reader.Close();

                    foreach (var item in tempMedarbejderList)
                    {

                        sqlQuery = "SELECT firstname, lastname from employee WHERE id = " + item.Key;
                        cmd = new MySqlCommand(sqlQuery, con);
                        using (MySqlDataReader secondReader = cmd.ExecuteReader())
                        {
                            if (secondReader.HasRows)
                            {
                                while (secondReader.Read())
                                {
                                    timeBankListe.Add(new TimeBank()
                                    {
                                        medarbejderNavn = secondReader.GetString(secondReader.GetOrdinal("firstname")) + " " + secondReader.GetString(secondReader.GetOrdinal("lastname")),
                                        medarbejderId = item.Key,
                                        udregnetTid = item.Value,
                                    });
                                }
                            }
                        }
                           
                    }
                }
            }
            
            // udregning af timer pr bruger 

            return timeBankListe;

        }

    }

}
