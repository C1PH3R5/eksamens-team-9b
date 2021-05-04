using System;
using System.Collections.Generic;
using System.Text;
using nida.tools_team_9b.Model;
using nida.tools_team_9b.Database;
using MySql.Data.MySqlClient;
using Microsoft.Win32;
using CsvHelper;
using System.IO;
using System.Globalization;
using System.Windows;

namespace nida.tools_team_9b.ViewModel
{
    public class TimeBankViewModel : DBCon
    {
        public static List<TimeBank> timeBankListe { get; set; }

        public static List<TimeBank> GetTimeBankList(string til, string fra)
        {
            timeBankListe = new List<TimeBank>();
            Dictionary<int, TimeSpan> tempMedarbejderList = new Dictionary<int, TimeSpan>();
            Dictionary<int, int> tempWorkDayCounter = new Dictionary<int, int>();

            MySqlConnection con = GetConnection();
            con.Open();
            string sqlQuery = "SELECT firstname, lastname, employee_id, type, time from timeBank INNER JOIN employee ON timeBank.employee_id = employee.id WHERE time BETWEEN '" + fra + "' AND '" + til + "'";
            MySqlCommand cmd = new MySqlCommand(sqlQuery, con);


            using (MySqlDataReader Reader = cmd.ExecuteReader())
            {
                if (Reader.HasRows)
                {
                    while (Reader.Read())
                    {
                        DateTime fratid = Reader.GetDateTime(Reader.GetOrdinal("time"));
                        int lastId = Reader.GetInt32(Reader.GetOrdinal("employee_id"));

                        if (Reader.Read() && lastId == Reader.GetInt32(Reader.GetOrdinal("employee_id")))
                        {
                            TimeSpan UdragenetTid = Reader.GetDateTime(Reader.GetOrdinal("time")).Subtract(fratid);
                            if (tempMedarbejderList.ContainsKey(Reader.GetInt32(Reader.GetOrdinal("employee_id"))))
                            {
                                tempMedarbejderList[Reader.GetInt32(Reader.GetOrdinal("employee_id"))] += UdragenetTid;
                                tempWorkDayCounter[Reader.GetInt32(Reader.GetOrdinal("employee_id"))]++;
                            }
                            else
                            {
                                tempMedarbejderList.Add(Reader.GetInt32(Reader.GetOrdinal("employee_id")), UdragenetTid);
                                tempWorkDayCounter.Add(Reader.GetInt32(Reader.GetOrdinal("employee_id")), 1);
                            }
                            
                        }

                    }
                    Reader.Close();

                    foreach (var item in tempMedarbejderList)
                    {

                        sqlQuery = "SELECT firstname, lastname, workinghours FROM employee WHERE id = " + item.Key;
                        MySqlCommand secondCmd = new MySqlCommand(sqlQuery, con);
                        using (MySqlDataReader secondReader = secondCmd.ExecuteReader())
                        {
                            if (secondReader.HasRows)
                            {
                                while (secondReader.Read())
                                {
                                    timeBankListe.Add(new TimeBank()
                                    {
                                        medarbejderNavn = secondReader.GetString(secondReader.GetOrdinal("firstname")) + " " + secondReader.GetString(secondReader.GetOrdinal("lastname")),
                                        medarbejderId = item.Key,
                                        udregnetTid = Math.Round(item.Value.TotalHours, 2, MidpointRounding.AwayFromZero),
                                        månedligeTimer = Math.Round(secondReader.GetFloat(secondReader.GetOrdinal("workinghours")) * tempWorkDayCounter[item.Key], 2, MidpointRounding.AwayFromZero),
                                        overarbejds = Math.Round((item.Value.TotalHours - (secondReader.GetFloat(secondReader.GetOrdinal("workinghours")) * tempWorkDayCounter[item.Key])), 2, MidpointRounding.AwayFromZero),

                                    });
                                }
                            }
                        }
                           
                    }
                }
            }
            
            // udregning af timer pr bruger 
            return timeBankListe;

        }
        public static void CsvFileWriter(string til, string fra)
        {
            SaveFileDialog sfd = new SaveFileDialog() { Filter = "CSV|*.csv", ValidateNames = true };
            if (sfd.ShowDialog() == true)
            {
                using (var sw = new StreamWriter(sfd.FileName, false, Encoding.UTF8))
                {
                    var writer = new CsvWriter(sw, CultureInfo.CurrentCulture);
                    writer.WriteField("Medarbejder Navn:");
                    writer.WriteField("Månedlige Timer:");
                    writer.WriteField("Overarbejds Timer:");
                    writer.WriteField("Udregnet Timer:");
                    writer.NextRecord();
                    foreach (TimeBank item in GetTimeBankList(til, fra))
                    {
                        writer.WriteField(item.medarbejderNavn);
                        writer.WriteField(item.månedligeTimer);
                        writer.WriteField(item.overarbejds);
                        writer.WriteField(item.udregnetTid);
                        writer.NextRecord();
                    }
                    writer.Flush();
                }
                MessageBox.Show("Din fil er blevet gemt succesful!" , "Message", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        } 

    }

}
