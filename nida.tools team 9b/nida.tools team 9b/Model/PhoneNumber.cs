using System;
using System.Collections.Generic;
using System.Text;

namespace nida.tools_team_9b.Model
{
    class PhoneNumber
    {
        private int _Number;

        public int number
        {
            get { return _Number; }
            set { _Number = value; }
        }

        private int _MedarbejderId;

        public int medarbejderId
        {
            get { return _MedarbejderId; }
            set { _MedarbejderId = value; }
        }
        private string _Type;

        public string type
        {
            get { return _Type; }
            set { _Type = value; }
        }



    }
}
