using System;
using System.Collections.Generic;
using System.Text;
using nida.tools_team_9b.Model;
using nida.tools_team_9b.Database;
using MySql.Data.MySqlClient;


namespace nida.tools_team_9b.ViewModel
{
    public class MedarbejderListViewModel : DBCon
    {
        public static List<Medarbejder> GetMedarbejder()
        {
            List<Medarbejder> medarbejderList = new List<Medarbejder>();
            MySqlConnection con = GetConnection();
            con.Open();
            string sqlQuery = "SELECT * FROM employee;";

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
                            
                            roleId = Reader.GetInt32(Reader.GetOrdinal("roleid")),
                            userId = Reader.GetString(Reader.GetOrdinal("userid")),
                            navn = Reader.GetString(Reader.GetOrdinal("firstname")),
                            efternavn = Reader.GetString(Reader.GetOrdinal("lastname")),
                            email = Reader.GetString(Reader.GetOrdinal("email")),
                            workinghours = Reader.GetFloat(Reader.GetOrdinal("workinghours")),
                        });
                    }
                    Reader.Close();
                }
            }
            con.Close();
            return medarbejderList;
        }
    }
}
