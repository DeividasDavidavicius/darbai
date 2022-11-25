using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _5.Deklaratyvusis_programavimas
{
    public class TaskUtils
    {
        /// <summary>
        /// Method used for combining two email lists into one without duplicates
        /// </summary>
        /// <param name="AllEmails">List of all email objects</param>
        /// <param name="Emails">List of email objects which will be added to AllEmails (except duplicates)</param>
        /// <returns>List of all email objects</returns>
        public static List<Email> CombineEmails(List<Email> AllEmails, List<Email> Emails)
        {
            AllEmails = AllEmails.Union<Email>(Emails).ToList<Email>(); // LINQ
            return AllEmails;
        }

        /// <summary>
        /// Method used for splitting all emails into lists by server and date
        /// </summary>
        /// <param name="EmailsByServer">List of email lists</param>
        /// <param name="Emails">List of all email objects</param>
        /// <param name="Servers">List of server objects</param>
        /// <param name="Dates">List of dates</param>
        public static void SplitIntoServerDates(List<List<Email>> EmailsByServer, List<Email> Emails, List<Server> Servers, List<DateTime> Dates)
        {
            for (int i = 0; i < Servers.Count; i++)
            {
                for (int j = 0; j < Dates.Count; j++)
                {
                    List<Email> emails = AddEmailsToServerDates(Emails, Servers[i].ServerName, Dates[j].Date);
                    if (emails.Count > 0)
                    {
                        EmailsByServer.Add(emails);
                    }
                }
            }
        }
        /// <summary>
        /// Method used for adding all email which were sent from specific server and date to one list
        /// </summary>
        /// <param name="Emails">List of all emails</param>
        /// <param name="ServerName">String of specific server</param>
        /// <param name="Date">DateTime of specific date</param>
        /// <returns>List of emails which were sent from specific server and date</returns>
        public static List<Email> AddEmailsToServerDates(List<Email> Emails, string ServerName, DateTime Date)
        {
            List<Email> emails = (from ar in Emails where ar.ServerName == ServerName && ar.Date == Date select ar).ToList<Email>(); // LINQ
            return emails;
        }

        /// <summary>
        /// Method used for calculating when all emails are finally sent
        /// </summary>
        /// <param name="AllEmails">List of email objects</param>
        /// <param name="Servers">List of server objects</param>
        public static void CalculateAllEndTimes(List<Email> AllEmails, List<Server> Servers)
        {
            for (int i = 0; i < AllEmails.Count; i++)
            {
                AllEmails[i].SetEndTime(CalculateEmailEndTime(AllEmails[i], Servers));
            }
        }
        /// <summary>
        /// Method used for calculating when email sending is finished
        /// </summary>
        /// <param name="Email">Object of email</param>
        /// <param name="Servers">List of server objects</param>
        /// <returns>DateTime when email sending is finished</returns>
        public static DateTime CalculateEmailEndTime(Email Email, List<Server> Servers)
        {
            DateTime EndDate = Email.Date;
            EndDate = EndDate.AddHours(Email.Time.Hour);
            EndDate = EndDate.AddMinutes(Email.Time.Minute);
            EndDate = EndDate.AddSeconds(Email.Time.Second);
            if(FindSpeed(Email, Servers) == 0)
            {
                throw new Exception("Vykdoma dalyba iš 0, kadangi vieno iš serverių greitis lygus 0! (" + Email.ServerName  + ")");
            }
            EndDate = EndDate.AddSeconds(Email.Bytes / FindSpeed(Email, Servers));
            return EndDate;
        }
        /// <summary>
        /// Method used for finding the speed (bytes/s) at which email was sent
        /// </summary>
        /// <param name="Email">Object of email</param>
        /// <param name="Servers">List of server objects</param>
        /// <returns>Speed at which email was sent</returns>
        public static double FindSpeed(Email Email, List<Server> Servers)
        {
            List<double> speed = (from ar in Servers where ar.ServerName == Email.ServerName select ar.ServerSpeed).ToList<double>(); //LINQ
            try
            {
                return speed[0];
            }
            catch
            {
                throw new Exception("Nėra serverio " + Email.ServerName + " duomenų serverių duomenų faile!");
            }
        }
        /// <summary>
        /// Method used for finding all hours during which no emails were sent / being sent
        /// </summary>
        /// <param name="Emails">List of all email objects</param>
        /// <param name="AllHours">List of all ServerHour objects</param>
        /// <returns></returns>
        public static List<ServerHour> FindMissingHours(List<Email> Emails, List<ServerHour> AllHours)
        {
            List<ServerHour> MissingHours = (from ar in AllHours where CheckHour(Emails, ar) == false select ar).ToList<ServerHour>(); // LINQ
            return MissingHours;
        }
        /// <summary>
        /// Method used for checking if an email was being sent during specific hour
        /// </summary>
        /// <param name="Emails">List of email objects</param>
        /// <param name="AllHours">ServerHour class object</param>
        /// <returns>True if at least 1 email was being sent during the hour, otherwise false</returns>
        public static bool CheckHour(List<Email> Emails, ServerHour AllHours)
        {
            List<Email> CheckHour = (from ar in Emails where ar.ServerName == AllHours.ServerName && ar.Date <= AllHours.Date && ar.Time.Hour <= AllHours.Hour && ar.EndTime >= AllHours.Date.AddHours(AllHours.Hour) select ar).ToList<Email>(); // LINQ
            if (CheckHour.Count > 0)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// Method used for sorting ServerHour objects
        /// </summary>
        /// <param name="MissingHours">List of ServerHour class objects</param>
        /// <returns>Sorted list of ServerHour ServerHour class objects</returns>
        public static List<ServerHour> Sort(List<ServerHour> MissingHours)
        {
            MissingHours = (from ar in MissingHours orderby ar.ServerName, ar.Date, ar.Hour select ar).ToList<ServerHour>(); // LINQ
            return MissingHours;
        }
    }
}