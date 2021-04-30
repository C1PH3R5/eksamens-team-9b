using System;
using System.Collections.Generic;
using System.Text;

namespace nida.tools_team_9b.Model
{
   public class TimeBank
   {

        private string _Type;

        public string type
        {
            get { return _Type; }
            set { _Type = value; }
        }
        private DateTime _Time;

        public DateTime time
        {
            get { return _Time; }
            set { _Time = value; }
        }
        private string _Kommentar;

        public string kommentar
        {
            get { return _Kommentar; }
            set { _Kommentar = value; }
        }
 

        private int _MedarnejderId;

        public int medarbejderId
        {
            get { return _MedarnejderId; }
            set { _MedarnejderId = value; }
        }
        private string _MedarbejderNavn;

        public string medarbejderNavn
        {
            get { return _MedarbejderNavn; }
            set { _MedarbejderNavn = value; }
        }
        private int _UdregnetTid;

        public int udregnetTid
        {
            get { return _UdregnetTid; }
            set { _UdregnetTid = value; }
        }
        private int _Overarbejds;

        public int overarbejds
        {
            get { return _Overarbejds; }
            set { _Overarbejds = value; }
        }
        private int _MånedligeTimer;

        public int månedligeTimer
        {
            get { return _MånedligeTimer; }
            set { _MånedligeTimer = value; }
        }







        public TimeBank()
       {

       }

   }
}
