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

        private string _Leder;

        public string leder
        {
            get { return _Leder; }
            set { _Leder = value; }
        }


        private int _Id;

        public int id
        {
            get { return _Id; }
            set { _Id = value; }
        }

        private int _LederId;

        public int lederId
        {
            get { return _LederId; }
            set { _LederId = value; }
        }

    }
}
