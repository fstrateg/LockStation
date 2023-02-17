using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LockStation.Models
{
    public class StateWriter
    {
        private static string fileName { get { return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data.ini"); } }
        public static void SaveState(int day, int time)
        { 
            TextWriter writer = new StreamWriter(fileName);
            writer.Write($"{day}:{time}");
            writer.Flush();
            writer.Close();
        }

        public static int LoadState()
        {
            if (!File.Exists(fileName)) return 0;
            TextReader reader = new StreamReader(fileName);
            string data=reader.ReadLine();
            reader.Close();
            string[] pp=data.Split(new char[] { ':' });
            int vl = int.Parse(pp[0]);
            if (vl.Equals(((int)DateTime.Now.DayOfWeek))) return int.Parse(pp[1]);
            return 0;
        }

        public static int LoadTimeForDay(int day)
        {
            Configuration config =ConfigurationManager.OpenExeConfiguration( ConfigurationUserLevel.None);
            var vl = config.AppSettings;
            
            return "vl.Value".Length;
        }
    }
}
