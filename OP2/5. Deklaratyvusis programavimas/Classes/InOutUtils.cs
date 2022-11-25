using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace _5.Deklaratyvusis_programavimas
{
    public class InOutUtils
    {
        /// <summary>
        /// Method used for reading email data
        /// </summary>
        /// <param name="Emails">List of email objects</param>
        /// <param name="fileName">String data fileName</param>
        /// <param name="Dates">List of all different dates</param>
        /// <param name="AllHours">List of server hour objects</param>
        public static void ReadEmails(List<Email> Emails, string fileName, List<DateTime> Dates, List<ServerHour> AllHours)
        {
            using (StreamReader reader = new StreamReader(fileName))
            {
                string line;
                line = reader.ReadLine();

                if (line == null)
                {
                    throw new Exception("Trūksta pradinių duomenų!");
                }

                string[] parts = line.Split(';');

                DateTime Date = DateTime.ParseExact(parts[0], "yyyy/MM/dd", System.Globalization.CultureInfo.InvariantCulture);
                if (!Dates.Contains(Date))
                {
                    Dates.Add(Date);
                }

                string ServerName = parts[1];

                for (int i = 0; i < 24; i++)
                {
                    ServerHour serverHour = new ServerHour(ServerName, i, Date);
                    if (!AllHours.Contains(serverHour))
                    {
                        AllHours.Add(serverHour);
                    }
                }

                while ((line = reader.ReadLine()) != null)
                {
                    parts = line.Split(';');
                    DateTime Time = DateTime.ParseExact(parts[0], "HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                    string SenderAddress = parts[1];
                    string ReceiverAddress = parts[2];
                    double Bytes = Convert.ToDouble(parts[3]);

                    Email email = new Email(Date, ServerName, Time, SenderAddress, ReceiverAddress, Bytes);
                    Emails.Add(email);
                }
            }
        }

        /// <summary>
        /// Method used for reading server data
        /// </summary>
        /// <param name="Servers">List of server objects</param>
        /// <param name="fileName">String of data fileName</param>
        public static void ReadServers(List<Server> Servers, string fileName)
        {
            using (StreamReader reader = new StreamReader(fileName))
            {
                string line;
                int lineCount = 0;
                while ((line = reader.ReadLine()) != null)
                {
                    lineCount++;
                    string[] parts = line.Split(';');

                    string ServerName = parts[0];
                    double ServerSpeed = Convert.ToDouble(parts[1]);

                    Server server = new Server(ServerName, ServerSpeed);
                    Servers.Add(server);
                }
                if(lineCount == 0)
                {
                    throw new Exception("Serverių informacijos failas yra tuščias!");
                }
            }

        }
        /// <summary>
        /// Method used for printing emails information into txt file
        /// </summary>
        /// <param name="Emails">List of email objects</param>
        /// <param name="fileName">String of results fileName</param>
        /// <param name="Header">Header information string</param>
        public static void PrintEmails(List<Email> Emails, string fileName, string Header)
        {
            if(Emails.Count == 0)
            {
                throw new Exception(" duomenų failas neturi pradinių duomenų(Nebuvo išsiųstas nei vienas laiškas)!");
            }
            using (var fr = File.AppendText(fileName))
            {
                fr.WriteLine(Header + " (" + Emails[0].Date.ToString("d") + ", " + Emails[0].ServerName + "):");
                fr.WriteLine(new string('-', 79));
                fr.WriteLine("| {0, -10} | {1, -22} | {2, -22} | {3, -12} |", "Laikas", "Gavėjas", "Siuntėjas", "Laiško dydis");
                fr.WriteLine(new string('-', 79));
                for (int i = 0; i < Emails.Count; i++)
                {
                    fr.WriteLine(Emails[i]);
                }
                fr.WriteLine(new string('-', 79));
                fr.WriteLine();
            }
        }
        /// <summary>
        /// Method used for printing serrvers information into txt file
        /// </summary>
        /// <param name="Servers">List of server objects</param>
        /// <param name="fileName">String of results fileName</param>
        /// <param name="Header">Header information string</param>
        public static void PrintServers(List<Server> Servers, string fileName, string Header)
        {
            using (var fr = File.AppendText(fileName))
            {
                fr.WriteLine(Header);
                fr.WriteLine(new string('-', 28));
                fr.WriteLine("| {0, -14} | {1, 7} |", "Serverio vard.", "Greitis");
                fr.WriteLine(new string('-', 28));
                for (int i = 0; i < Servers.Count; i++)
                {
                    fr.WriteLine(Servers[i]);
                }
                fr.WriteLine(new string('-', 28));
                fr.WriteLine();
            }
        }
        /// <summary>
        /// Method used for printing missing hours information into txt file
        /// </summary>
        /// <param name="AllHours">List of server hour objects</param>
        /// <param name="fileName">String of results fileName</param>
        /// <param name="Header">Header information string</param>
        public static void PrintMissingHours(List<ServerHour> AllHours, string fileName, string Header)
        {
            if(AllHours.Count == 0)
            {
                throw new Exception("Nei vienam serveryje nėra valandų, kai nebuvo perduoti duomenys!");
            }

            using (var fr = File.AppendText(fileName))
            {
                fr.WriteLine(Header);
                fr.WriteLine(new string('-', 44));
                fr.WriteLine("| {0, -14} | {1, -12} | {2, -8} |", "Serverio vard.", "Diena", "Valand.");
                fr.WriteLine(new string('-', 44));
                for (int i = 0; i < AllHours.Count; i++)
                {
                    fr.WriteLine(AllHours[i]);
                }
                fr.WriteLine(new string('-', 44));
                fr.WriteLine();
            }
        }
    }
}