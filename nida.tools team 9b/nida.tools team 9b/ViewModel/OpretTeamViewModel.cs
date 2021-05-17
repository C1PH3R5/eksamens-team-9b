using System;
using System.Collections.Generic;
using System.Text;
using nida.tools_team_9b.View.page;
using nida.tools_team_9b.Database;
using MySql.Data.MySqlClient;
using nida.tools_team_9b.Model;


namespace nida.tools_team_9b.ViewModel
{
    public class OpretTeamViewModel : DBCon
    {
        public static void OpretTeamData(opretTeam window)
        {
            MySqlConnection con = GetConnection();
            String sqlQuery = "INSERT INTO team SET teamNavn ='" + window.teamNavn.Text + "';";

            MySqlCommand cmd = new MySqlCommand(sqlQuery, con);
            con.Open();
            cmd.ExecuteNonQuery();
            SetTeamLeder(window, con);
            con.Close();

        }

        public static void SetTeamLeder(opretTeam window, MySqlConnection con)
        {
            string SqlQuery = "INSERT INTO employee_team SET team_id = "+ GetTeamId(window, con) +", employee_id = "+ GetEmployeeId(window, con) +";";

            MySqlCommand cmd = new MySqlCommand(SqlQuery, con);
            cmd.ExecuteNonQuery();
        }

        public static int GetTeamId(opretTeam window, MySqlConnection con)
        {
            string sqlQuery = "SELECT id FROM team WHERE teamNavn = '"+ window.teamNavn.Text +"';";
            MySqlCommand cmd = new MySqlCommand(sqlQuery, con);

            int teamId = 0;

            using (MySqlDataReader Reader = cmd.ExecuteReader())
            {
                if (Reader.HasRows)
                {
                    while (Reader.Read())
                    {
                        //I would also check for DB.Null here before reading the value.
                        teamId = Reader.GetInt32(Reader.GetOrdinal("id"));
                    }
                    Reader.Close();
                }
            }

            return teamId;
        }

        public static int GetEmployeeId(opretTeam window, MySqlConnection con)
        {
            string sqlQuery = "SELECT id FROM employee WHERE firstname ='"+ window.vælgTeamLeder.Text.Split(' ')[0] +"' AND lastname ='"+ window.vælgTeamLeder.Text.Split(' ')[1]+"';";
            MySqlCommand cmd = new MySqlCommand(sqlQuery, con);

            int lederId = 0;

            using (MySqlDataReader Reader = cmd.ExecuteReader())
            {
                if (Reader.HasRows)
                {
                    while (Reader.Read())
                    {
                        //I would also check for DB.Null here before reading the value.
                        lederId = Reader.GetInt32(Reader.GetOrdinal("id"));
                    }
                    Reader.Close();
                }
            }

            return lederId;
        }

        public static List<Team> GetTeamlederList(opretTeam window)
        {
            MySqlConnection con = GetConnection();
            con.Open();
            string sqlQuery = "SELECT firstname, lastname, id FROM employee WHERE roleid = 2;";
            MySqlCommand cmd = new MySqlCommand(sqlQuery, con);

            List<Team> lederList = new List<Team>();

            using(MySqlDataReader Reader = cmd.ExecuteReader())
            {
                if (Reader.HasRows)
                {
                    while (Reader.Read())
                    {
                        lederList.Add(new Team()
                        {
                            lederId = Reader.GetInt32(Reader.GetOrdinal("id")),
                            name = (Reader.GetString(Reader.GetOrdinal("firstname")) + " " + Reader.GetString(Reader.GetOrdinal("lastname")))

                        });
                    } 
                    Reader.Close();
                }
            }
            window.vælgTeamLeder.ItemsSource = lederList;
            window.vælgTeamLeder.SelectedValue = "id";
            window.vælgTeamLeder.DisplayMemberPath = "name";
            return lederList;
        }
    }
        
}