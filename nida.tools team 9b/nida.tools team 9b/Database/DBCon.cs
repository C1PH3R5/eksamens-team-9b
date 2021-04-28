using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;

namespace nida.tools_team_9b.Database
{
    public class DBCon
    {

        public static MySqlConnection GetConnection()
        {
            StringBuilder ConnString = new StringBuilder();
            ConnString.Append("Server=176.20.170.241;");
            ConnString.Append("Port=3306;");
            ConnString.Append("Database=NidaTools;");
            ConnString.Append("Uid=NidaTools;");
            ConnString.Append("password=PwNidaTools;");
            return new MySqlConnection(ConnString.ToString());
        }
    }
}
