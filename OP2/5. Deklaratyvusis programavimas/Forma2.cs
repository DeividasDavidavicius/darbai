using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace _5.Deklaratyvusis_programavimas
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        /// <summary>
        /// Method used for printing email data into textbox
        /// </summary>
        /// <param name="textBox">textBox object</param>
        /// <param name="Emails">List of email objects</param>
        /// <param name="Header">Header information string</param>
        public static void PrintEmailsTextBox(TextBox textBox, List<Email> Emails, string Header)
        {
            textBox.Text += Header + " (" + Emails[0].Date.ToString("d") + ", " + Emails[0].ServerName + "):" + '\n';
            textBox.Text += new string('-', 79) + '\n';
            textBox.Text += string.Format("| {0, -10} | {1, -22} | {2, -22} | {3, -12} |", "Laikas", "Gavėjas", "Siuntėjas", "Laiško dydis") + '\n';
            textBox.Text += new string('-', 79) + '\n';
            for (int i = 0; i < Emails.Count; i++)
            {
                textBox.Text += Emails[i];
                textBox.Text += '\n';
            }
            textBox.Text += (new string('-', 79) + '\n' + '\n');
        }
        /// <summary>
        /// Method used for printing server info into table
        /// </summary>
        /// <param name="Table">Table object</param>
        /// <param name="Label">Label object</param>
        /// <param name="Servers">List of server objects</param>
        /// <param name="Header">Header information string</param>
        public static void PrintServerInfoTable(Table Table, Label Label, List<Server> Servers, string Header)
        {
            Label.Text = Header;

            TableRow Row = new TableRow();
            Table.Rows.Add(Row);

            TableCell Name = new TableCell();
            Name.Text = "Serverio vard.";
            Name.Width = 80;
            Name.Height = 20;
            Row.Cells.Add(Name);

            TableCell Speed = new TableCell();
            Speed.Text = "Greitis";
            Speed.Width = 80;
            Speed.Height = 20;
            Row.Cells.Add(Speed);

            for(int i = 0; i < Servers.Count; i++)
            {
                Row = new TableRow();
                Table.Rows.Add(Row);

                Name = new TableCell();
                Name.Text = Servers[i].ServerName;
                Name.Width = 100;
                Name.Height = 20;
                Row.Cells.Add(Name);

                Speed = new TableCell();
                Speed.Text = Servers[i].ServerSpeed.ToString();
                Speed.Width = 100;
                Speed.Height = 20;
                Speed.HorizontalAlign = HorizontalAlign.Right;
                Row.Cells.Add(Speed);
            }
        }
        /// <summary>
        /// Method used for printing missing hours into table
        /// </summary>
        /// <param name="Table">Table object</param>
        /// <param name="Label">Label object</param>
        /// <param name="MissingHours">List of missing hour objects</param>
        /// <param name="Header">Header information string</param>
        public static void PrintMissingHourTable(Table Table, Label Label, List<ServerHour> MissingHours, string Header)
        {
            Label.Text = Header;

            TableRow Row = new TableRow();
            Table.Rows.Add(Row);

            TableCell Name = new TableCell();
            Name.Text = "Serverio vard.";
            Name.Width = 80;
            Name.Height = 20;
            Row.Cells.Add(Name);

            TableCell Date = new TableCell();
            Date.Text = "Diena";
            Date.Width = 80;
            Date.Height = 20;
            Row.Cells.Add(Date);

            TableCell Hour = new TableCell();
            Hour.Text = "Valand.";
            Hour.Width = 80;
            Hour.Height = 20;
            Row.Cells.Add(Hour);

            for(int i = 0; i < MissingHours.Count; i++)
            {
                Row = new TableRow();
                Table.Rows.Add(Row);

                Name = new TableCell();
                Name.Text = MissingHours[i].ServerName;
                Name.Width = 80;
                Name.Height = 20;
                Row.Cells.Add(Name);

                Date = new TableCell();
                Date.Text = MissingHours[i].Date.ToString("d");
                Date.Width = 80;
                Date.Height = 20;
                Row.Cells.Add(Date);
                Date.HorizontalAlign = HorizontalAlign.Right;

                Hour = new TableCell();
                Hour.Text = MissingHours[i].Hour.ToString();
                Hour.Width = 80;
                Hour.Height = 20;
                Row.Cells.Add(Hour);
                Hour.HorizontalAlign = HorizontalAlign.Right;
            }
        }
    }
}