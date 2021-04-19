using System;
using System.Collections.Generic;
using System.Text;
using nida.tools_team_9b.Model;

namespace nida.tools_team_9b.ViewModel
{
    public class MødtMedarbejder
    {
        private List<Medarbejder> LoadMedarbjderDataSet()
        {
            List<Medarbejder> MødtMedarbejder = new List<Medarbejder>();
            MødtMedarbejder.Add(new Medarbejder()
            {
                navn = "Mikkel Christensen",
                status = "mødt",
                stempleInd = new DateTime(2021 - 04 - 01 - 07,00,00),
                stempleUd = new DateTime(),
                kommentar = ""
            });
            MødtMedarbejder.Add(new Medarbejder()
            {
                navn = "Tor Kristiansen",
                status = "gået",
                stempleInd = new DateTime(2021 - 04 - 01 - 08, 00, 00),
                stempleUd = new DateTime(2021 - 04 - 01 - 12, 00, 00),
                kommentar = ""
            });

            return MødtMedarbejder;
        }
    }
}
