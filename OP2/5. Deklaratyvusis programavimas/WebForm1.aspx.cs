using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.UI.HtmlControls;

namespace _5.Deklaratyvusis_programavimas
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        List<List<Email>> EmailsByServerDates = new List<List<Email>>();
        List<Email> Emails = new List<Email>();
        List<Server> Servers = new List<Server>();
        List<ServerHour> AllHours = new List<ServerHour>();
        List<DateTime> Dates = new List<DateTime>();

        protected void Page_Load(object sender, EventArgs e)
        {
            
            if(Session["Emails"] != null && Session["Servers"] != null && Session["AllHours"] != null && Session["Dates"] != null)
            {
                Emails = (List<Email>)Session["Emails"];
                Servers = (List<Server>)Session["Servers"];
                AllHours = (List<ServerHour>)Session["AllHours"];
                Dates = (List<DateTime>)Session["Dates"];

            }        
            Button2.Enabled = false;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string EmailDataPath = Server.MapPath("Email_Data");
            string ServerData = Server.MapPath("App_Data\\ServerData.txt");
            string Res = Server.MapPath("App_Data\\Results.txt");

            try
            {
                if (!File.Exists(Res))
                {
                    throw new FileNotFoundException();
                }
            }
            catch (FileNotFoundException)
            {
                Label1.Text = "Nėra visų duomenų rezultatų failo!";
                return;
            }

            using (var fr = File.CreateText(Res)) { }

            Label1.Text = "Programa sėkmingai paleista!";

            DirectoryInfo d = new DirectoryInfo(EmailDataPath);
            FileInfo[] Files = d.GetFiles("*.txt");

            foreach (FileInfo file in Files)
            {
                List<Email> emails = new List<Email>();
                try
                {
                    InOutUtils.ReadEmails(emails, file.FullName, Dates, AllHours);

                    Emails = TaskUtils.CombineEmails(Emails, emails);
                }
                catch (Exception ex)
                {
                    Label1.Text = ex.Message + " (" + file.Name + ")";
                    File.AppendAllText(Res, ex.Message + " (" + file.Name + ")");
                    return;
                }             

                try
                {
                    InOutUtils.PrintEmails(emails, Res, "Serverio pradiniai duomenys");
                    PrintEmailsTextBox(TextBox1, emails, "Serverio pradiniai duomenys");
                    
                }
                catch (Exception ex)
                {
                    File.AppendAllText(Res, file.Name + ex.Message + '\n' + '\n');
                    TextBox1.Text += file.Name + ex.Message + '\n' + '\n';
                }
            }

            try
            {
                InOutUtils.ReadServers(Servers, ServerData);
                InOutUtils.PrintServers(Servers, Res, "Serverių pradiniai duomenys:");
                PrintServerInfoTable(Table1, Label2, Servers, "Serverių pradiniai duomenys:");
            }
            catch (Exception ex)
            {
                Label1.Text = ex.Message;
                File.AppendAllText(Res, ex.Message);
                return;
            }

            Session["Emails"] = Emails;
            Session["Servers"] = Servers;
            Session["AllHours"] = AllHours;
            Session["Dates"] = Dates;

            Button2.Enabled = true;
            Button1.Enabled = false;
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            string Res = Server.MapPath("App_Data\\Results.txt");
            Label1.Text = "Skaičiavimai sėkmingai atlikti!";

            PrintServerInfoTable(Table1, Label2, Servers, "Serverių pradiniai duomenys:");

            TaskUtils.SplitIntoServerDates(EmailsByServerDates, Emails, Servers, Dates);

            for (int i = 0; i < EmailsByServerDates.Count; i++)
            {
                InOutUtils.PrintEmails(EmailsByServerDates[i], Res, "Pradiniai duomenys sulieti pagal serverį ir datą");
                PrintEmailsTextBox(TextBox1, EmailsByServerDates[i], "Pradiniai duomenys sulieti pagal serverį ir datą");
            }

            try
            {
                TaskUtils.CalculateAllEndTimes(Emails, Servers);
            }
            catch (Exception ex)
            {
                File.AppendAllText(Res, ex.Message);
                Label1.Text = ex.Message;
                Session["Emails"] = null;
                Session["Servers"] = null;
                Session["AllHours"] = null;
                Session["Dates"] = null;
                return;
            }

            List<ServerHour> MissingHours = TaskUtils.FindMissingHours(Emails, AllHours);

            try
            {
                InOutUtils.PrintMissingHours(MissingHours, Res, "Sąrašas valandų, kai serveriuose nebuvo perduoti duomenys (Nesurikiuotas):");
                PrintMissingHourTable(Table2, Label3, MissingHours, "Sąrašas valandų, kai serveriuose nebuvo perduoti duomenys (Nesurikiuotas):");
                MissingHours = TaskUtils.Sort(MissingHours);
                InOutUtils.PrintMissingHours(MissingHours, Res, "Sąrašas valandų, kai serveriuose nebuvo perduoti duomenys (Surikiuotas):");
                PrintMissingHourTable(Table3, Label4, MissingHours, "Sąrašas valandų, kai serveriuose nebuvo perduoti duomenys (Surikiuotas):");

            }
            catch (Exception ex)
            {
                File.AppendAllText(Res, ex.Message);
                Label3.Text = ex.Message;
            }

            Session["Emails"] = null;
            Session["Servers"] = null;
            Session["AllHours"] = null;
            Session["Dates"] = null;
        }
    }
}