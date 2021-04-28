using System;
using System.Collections.Generic;
using System.Text;
using nida.tools_team_9b.Model;
using nida.tools_team_9b.Database;
using MySql.Data.MySqlClient;

namespace nida.tools_team_9b.ViewModel
{
    public class TeamListViewModel : DBCon
    {
        public static List<Team> GetTeam()
        {
            MySqlConnection con = GetConnection();
            con.Open();
            string sqlQuery = "SELECT id, name FROM team;";
            MySqlCommand cmd = new MySqlCommand(sqlQuery, con);

            List<Team> TeamList = new List<Team>();

            using (MySqlDataReader Reader = cmd.ExecuteReader())
            {
                if (Reader.HasRows)
                {
                    while (Reader.Read())
                    {
                        //I would also check for DB.Null here before reading the value.
                        TeamList.Add(new Team() 
                        {
                            name = Reader.GetString(Reader.GetOrdinal("name")),
                            id = Reader.GetInt32(Reader.GetOrdinal("id")),
                        });
                    }
                    Reader.Close();
                }
            }
            con.Close();
            return TeamList;
        }
    }
}
