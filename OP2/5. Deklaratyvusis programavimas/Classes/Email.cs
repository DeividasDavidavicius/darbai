using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _5.Deklaratyvusis_programavimas
{
    /// <summary>
    /// Class used for storing emails data
    /// </summary>
    public class Email
    {
        public DateTime Date { get; private set; }
        public string ServerName { get; private set; }
        public DateTime Time { get; private set; }
        public string SenderAddress { get; private set; }
        public string ReceiverAddress { get; private set; }
        public double Bytes { get; private set; }

        public DateTime EndTime { get; private set; }


        public Email(DateTime date, string serverName, DateTime time, string senderAddress, string receiverAddress, double bytes)
        {
            this.Date = date;
            this.ServerName = serverName;
            this.Time = time;
            this.SenderAddress = senderAddress;
            this.ReceiverAddress = receiverAddress;
            this.Bytes = bytes;
        }

        /// <summary>
        /// Method used for formating emails data string
        /// </summary>
        /// <returns>Data string of email</returns>
        public override string ToString()
        {
            return string.Format("| {0, 10} | {1, -22} | {2, -22} | {3, 12} |", Time.ToString("HH:mm:ss"), SenderAddress, ReceiverAddress, Bytes);
        }
        /// <summary>
        /// Method used to compare this email to other email
        /// </summary>
        /// <param name="obj">Object of other email</param>
        /// <returns>True if both objects are equal, otherwise false</returns>
        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Email))
            {
                return false;
            }
            Email Temp = obj as Email;
            return this.Date == Temp.Date && this.ServerName == Temp.ServerName && this.Time == Temp.Time && this.SenderAddress == Temp.SenderAddress && this.ReceiverAddress == Temp.ReceiverAddress && this.Bytes == Temp.Bytes;
        }
        /// <summary>
        /// Method used to get hash code
        /// </summary>
        /// <returns>Hash code of an object</returns>
        public override int GetHashCode()
        {
            return this.Time.GetHashCode();
        }
        /// <summary>
        /// Method used for changing EndTime
        /// </summary>
        /// <param name="Date">Date to which endtime will be changed</param>
        public void SetEndTime(DateTime Date)
        {
            this.EndTime = Date;
        }
    }
}