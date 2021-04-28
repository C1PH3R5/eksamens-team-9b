using System;
using System.Collections.Generic;
using System.Text;

namespace nida.tools_team_9b.Model
{
   public class Medarbejder
    {
        private string _Navn;

        public string navn
        {
            get { return _Navn; }
            set { _Navn = value; }
        }
        private string _Team;

        public string team
        {
            get { return _Team; }
            set { _Team = value; }
        }
        private int _RoleId;

        public int roleId
        {
            get { return _RoleId; }
            set { _RoleId = value; }
        }
        private string _UserId;

        public string userId
        {
            get { return _UserId; }
            set { _UserId = value; }
        }
        private string _Efternavn;

        public string efternavn
        {
            get { return _Efternavn; }
            set { _Efternavn = value; }
        }
        private string _Email;

        public string email
        {
            get { return _Email; }
            set { _Email = value; }
        }
        private float _Workinghours;

        public float workinghours
        {
            get { return _Workinghours; }
            set { _Workinghours = value; }
        }



    }
}
