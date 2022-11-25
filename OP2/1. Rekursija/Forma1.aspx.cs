using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace Rekursija
{

    public partial class Forma1 : System.Web.UI.Page
    {
        public Forma2 Forma2
        {
            get => default;
            set
            {
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string DataFile = Server.MapPath("App_Data/U3.txt");
            string ResultsFile = Server.MapPath("Results.txt");
            int size = 8, maxMoves = 10, iz, jz, ik, jk;
            GridData Data = Forma2.ReadData(size, DataFile, maxMoves);
            Label2.Text = "Data:";
            Forma2.CreateTable2(Data, Table2);
            Forma2.PrintData(Data, ResultsFile);


            Forma2.PieceCoordinates(Data, "Z", out iz, out jz);
            Forma2.PieceCoordinates(Data, "K", out ik, out jk);
            Data.SetHorsePosition(iz, jz);
            Data.SetKingPosition(ik, jk);

            int foundKing = 0, currentMove = 1, foundPath = 0;
            Forma2.FindLengh(Data, iz, jz, size, maxMoves, currentMove, foundKing);

            if(Data.MinMoves <= maxMoves)
            {
                Forma2.FindPath(Data, iz, jz, size, currentMove, foundKing, ref foundPath);
                Forma2.CreateTable(Data, Table1);
                Forma2.PrintResults(ResultsFile, Table1);
                Label1.Text = "Path succesfully found!";
                Label3.Text = "Results:";
            }
            else
            {
                Label1.Text = "The best path wasn't found or it had more than " + maxMoves + " moves.";

                using(var fr = File.AppendText(ResultsFile))
                {
                    fr.WriteLine("Horse hasn't found the best path");
                }
            }


        }
    }
}