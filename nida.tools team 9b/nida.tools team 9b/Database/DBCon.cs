using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;

namespace nida.tools_team_9b.Database
{
    class DBCon
    {
        protected MySqlConnection GetConnection()
        {
            StringBuilder connString = new StringBuilder();
            connString.Append("Server=localhost;");
            connString.Append("Port=3306;");
            connString.Append("Database=NidaTools;");
            connString.Append("Uid=nidatools;");
            connString.Append("password=nidatools;");
            return new MySqlConnection(connString.ToString());
        }
    }
    
}
