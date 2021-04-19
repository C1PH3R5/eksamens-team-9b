using System;
using System.Collections.Generic;
using System.Text;
using nida.tools_team_9b.Model;

namespace nida.tools_team_9b.ViewModel
{
    public class MødtMedarbejder
    {
        public static List<Medarbejder> LoadMedarbjderDataSet()
        {
            List<Medarbejder> MødtMedarbejder = new List<Medarbejder>();
            MødtMedarbejder.Add(new Medarbejder()
            {
                navn = "Mikkel Christensen",
                status = "mødt",
                stempleInd = new DateTime(2021, 04, 01, 7, 0, 0),
                stempleUd = new DateTime(),
                kommentar = ""
            });
            MødtMedarbejder.Add(new Medarbejder()
            {
                navn = "Tor Kristiansen",
                status = "gået",
                stempleInd = new DateTime(2021, 04, 01, 7, 0, 0),
                stempleUd = new DateTime(2021, 04, 01, 12, 0, 0),
                kommentar = ""
            });

            return MødtMedarbejder;
        }
    }
}
