using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _5.Deklaratyvusis_programavimas
{
    /// <summary>
    /// Class used for storing the data of specific server hour
    /// </summary>
    public class ServerHour
    {
        public string ServerName { get; private set; }
        public int Hour { get; private set; }
        public DateTime Date { get; private set; }
        public ServerHour(string serverName, int hour, DateTime date)
        {
            this.ServerName = serverName;
            this.Hour = hour;
            this.Date = date;
        }
        /// <summary>
        ///  Method used to compare this server hour to other server hour
        /// </summary>
        /// <param name="obj">Object of other server hour</param>
        /// <returns>True if both objects are equal, otherwise false</returns>
        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is ServerHour))
            {
                return false;
            }

            ServerHour Temp = obj as ServerHour;
            return this.Date == Temp.Date && this.ServerName == Temp.ServerName && this.Hour == Temp.Hour;
        }
        /// <summary>
        /// Method used to get hash code
        /// </summary>
        /// <returns>Hash code of an object</returns>
        public override int GetHashCode()
        {
            return this.ServerName.GetHashCode();
        }
        /// <summary>
        /// Method used for formating emails data string
        /// </summary>
        /// <returns>Data string of server hour</returns>
        public override string ToString()
        {
            return string.Format("| {0, -14} | {1, 12} | {2, 8} |", ServerName, Date.ToString("d"), Hour);
        }
    }
}