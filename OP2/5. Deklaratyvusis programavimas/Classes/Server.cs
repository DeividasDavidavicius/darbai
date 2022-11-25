using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _5.Deklaratyvusis_programavimas
{
    /// <summary>
    /// Class used for storing server data
    /// </summary>
    public class Server
    {
        public string ServerName { get; private set; }
        public double ServerSpeed { get; private set; }

        public Server(string serverName, double serverSpeed)
        {
            this.ServerName = serverName;
            this.ServerSpeed = serverSpeed;
        }
        /// <summary>
        /// Method used for formating emails data string
        /// </summary>
        /// <returns>Data string of server</returns>
        public override string ToString()
        {
            return string.Format("| {0, -14} | {1, 7} |", ServerName, ServerSpeed);
        }
    }
}