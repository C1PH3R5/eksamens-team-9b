using System;
using System.Collections.Generic;
using System.Text;

namespace nida.tools_team_9b.Model
{
    class Medarbejder
    {
        private string _Navn;

        public string navn
        {
            get { return _Navn; }
            set { _Navn = value; }
        }
        private string _Status;

        public string status
        {
            get { return _Status; }
            set { _Status = value; }
        }
        private DateTime _StempleInd;

        public DateTime stempleInd
        {
            get { return _StempleInd; }
            set { _StempleInd = value; }
        }


    }
}
