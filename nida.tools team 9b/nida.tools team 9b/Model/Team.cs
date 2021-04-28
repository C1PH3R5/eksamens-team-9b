using System;
using System.Collections.Generic;
using System.Text;

namespace nida.tools_team_9b.Model
{
    public class Team
    {
        private string _Name;

        public string name
        {
            get { return _Name; }
            set { _Name = value; }
        }
        private int _Id;

        public int id
        {
            get { return _Id; }
            set { _Id = value; }
        }


    }
}
