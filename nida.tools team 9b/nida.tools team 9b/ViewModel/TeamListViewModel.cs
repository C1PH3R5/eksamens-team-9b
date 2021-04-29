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
            string sqlQuery = "SELECT employee.firstname, employee.lastname, team.teamNavn FROM ((employee INNER JOIN employee_team ON employee.id = employee_team.employee_id) INNER JOIN team ON employee_team.team_id = team.id) WHERE employee.roleid = 2;";
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
                            name = Reader.GetString(Reader.GetOrdinal("teamNavn")),
                            leder = (Reader.GetString(Reader.GetOrdinal("firstname")) + " " + Reader.GetString(Reader.GetOrdinal("lastname"))),
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
