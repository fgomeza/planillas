using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace SistemaDePlanillas.Models
{
    public class FilesReader
    {
        public class CMSRegister
        {
            public string cmsid { get; set; }
            public long calls { get; set; }
            public TimeSpan hours { get; set; }
            public DateTime date { get; set; }
            public CMSRegister(string cmsid, int calls, TimeSpan hours, DateTime date)
            {
                this.cmsid = cmsid;
                this.calls = calls;
                this.hours = hours;
                this.date = date;
            }
        }

        public static List<CMSRegister> readFromCMSFile(Stream file)
        {
            List<CMSRegister> list = new List<CMSRegister>();
            StreamReader reader = new StreamReader(file);
            for (int i = 0; i < 9; i++)
            {
                reader.ReadLine();
            }
            while (!reader.EndOfStream)
            {
                string[] data = reader.ReadLine().Split('\t');
                string cmsid = data[0];
                int calls = Int32.Parse(data[2]);
                TimeSpan hours = new TimeSpan(0, 0, Int32.Parse(data[11]));
                DateTime date = DateTime.ParseExact(data[1], "dd/MM/yyyy", CultureInfo.CurrentCulture);
                list.Add(new CMSRegister(cmsid, calls, hours, date));
            }
            return list;
        }

        /*
        public static List<CMSRegister> readFromCMSFile(Stream file)
        {
            try
            {
                StreamReader reader = new StreamReader(file);
                for (int i = 0; i < 9; i++)
                {
                    reader.ReadLine();
                }

                string data = reader.ReadToEnd();
                string filePattern = @"^([^\t]+\t\d{2}/\d{2}/\d{4}\t\d+\t[^\n]\n)*$";
                //if (!Regex.IsMatch(data, filePattern))
                //{
                //    return null;
                //}

                var employees = new Dictionary<string, CMSRegister>();
                string dataPattern = @"(?<cmsid>[^\t]+)\t(?<date>\d{2}/\d{2}\d{4})\t(?<calls>\d+)\t[^\n]\n";
                foreach (Match match in Regex.Matches(data, dataPattern))
                {
                    string cmsid = match.Groups["cmsid"].Value;
                    int calls = int.Parse(match.Groups["calls"].Value);
                    int hours = 0;
                    if (!employees.ContainsKey(cmsid))
                    {
                        //employees[cmsid] = new CMSRegister(cmsid, calls, hours);
                    }
                    else
                    {
                        var register = employees[cmsid];
                        register.calls += calls;
                       // register.hours += hours;
                    }
                }
                var list = new List<CMSRegister>();
                foreach (CMSRegister register in employees.Values)
                {
                    list.Add(register);
                }
                return list;
            }
            catch (Exception)
            {
                return null;
            }
        }
        */
    }
}