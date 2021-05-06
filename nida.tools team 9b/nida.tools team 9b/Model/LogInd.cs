using System;
using System.Collections.Generic;
using System.Text;

namespace nida.tools_team_9b.Model
{
    public class LogInd
    {
        private int _ID;

        public int id
        {
            get { return _ID; }
            set { _ID = value; }
        }

        private int _EmployeeId;
                
        public int employeeId
        {
            get { return _EmployeeId; }
            set { _EmployeeId = value; }
        }
        private string _PasswordStatus;

        public string passwordStatus
        {
            get { return _PasswordStatus; }
            set { _PasswordStatus = value; }
        }
        private int _InvalidAttemps;

        public int invalidAttemps
        {
            get { return _InvalidAttemps; }
            set { _InvalidAttemps = value; }
        }


    }
}
