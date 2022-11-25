using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace Rekursija
{
    public partial class Forma2 : System.Web.UI.Page
    {
        /// <summary>
        /// Method used for reading the data from the data file and putting it into chess grid data class
        /// </summary>
        /// <param name="size">The size of chess grid in each direction (it is a square)</param>
        /// <param name="fileName">The name of data file</param>
        /// <param name="maxMoves">The maximum amount of moves the horse could move before stopping</param>
        /// <returns>Class GridData object which has all of the data of chess grid</returns>
        public static GridData ReadData(int size, string fileName, int maxMoves)
        {
            using(StreamReader reader = new StreamReader(fileName))
            {
                string[,] gridData = new string[size, size];

                string line;
                int currentLine = 0;
                while((line = reader.ReadLine()) != null)
                {
                    string[] parts = line.Split(' ');

                    int currentColumn = 0;

                    foreach(string part in parts)
                    {
                        gridData[currentLine, currentColumn] = Convert.ToString(part);
                        currentColumn++;
                    }


                    currentLine++;
                }

                GridData Data = new GridData(gridData, size, maxMoves + 1);

                return Data;
            }
        }
        /// <summary>
        /// Method which is used for finding x and y coordinates of specific chess piece
        /// </summary>
        /// <param name="Data">Class GridData object which has all of the data of chess grid</param>
        /// <param name="symbol">Symbol of the piece whose coordinates are being found</param>
        /// <param name="ii">x coordinate of chess piece</param>
        /// <param name="jj">y coordinate of chess piece</param>
        public static void PieceCoordinates(GridData Data, string symbol, out int ii, out int jj)
        {
            ii = -1;
            jj = -1;
            for(int i = 0; i < Data.Size; i++)
            {
                for(int j = 0; j < Data.Size; j++)
                {
                    if(Data.GetData(i, j) == symbol)
                    {
                        ii = i;
                        jj = j;
                    }
                }
            }
        }
        /// <summary>
        /// Method used for checking if horse has already been on specific grid cell (Excluding king and horse cells)
        /// </summary>
        /// <param name="Data">Class GridData object which has all of the data of chess grid</param>
        /// <param name="ii">x coordinates of grid cell</param>
        /// <param name="jj">y coordinates of grid cell</param>
        /// <returns>returns true if horse has already been on specific grid cell and false if it hasn't</returns>
        public static bool CheckIfTaken(GridData Data, int ii, int jj)
        {
            if (Data.GetData(ii, jj) == "0" || Data.GetData(ii, jj) == "K" || Data.GetData(ii, jj) == "Z")
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// Method used for creating results table
        /// </summary>
        /// <param name="Data">Class GridData object which has all of the data of chess grid</param>
        /// <param name="Table1">Table into which the results will be written</param>
        public static void CreateTable(GridData Data, Table Table1)
        {
            for(int i = 0; i < Data.Size + 1; i++)
            {
                TableRow Row = new TableRow();
                Table1.Rows.Add(Row);

                for(int j = 0; j < Data.Size + 1; j++)
                {
                    TableCell Cell = new TableCell();
                    if (i > 0 && j > 0)
                    {
                        if(Data.GetData(i - 1, j - 1) == "0")
                        {
                            Cell.Text = "-";
                        }
                        else
                        {
                            Cell.Text = Data.GetData(i - 1, j - 1).ToString();
                        }
                    }
                    else if (i == 0)
                    {
                        if (j != 0)
                        {
                            Cell.Text = j.ToString();
                        }

                        if(j == 0)
                        {
                            Cell.Text = " ";
                        }
                    }
                    else
                    {
                        Cell.Text = i.ToString();
                    }

                    Cell.Width = 15;
                    Cell.Height = 15;
                    Row.Cells.Add(Cell);
                }
            }
        }
        /// <summary>
        /// Method used for creating data table
        /// </summary>
        /// <param name="Data">Class GridData object which has all of the data of chess grid</param>
        /// <param name="Table2">Table into which the results will be written</param>
        public static void CreateTable2(GridData Data, Table Table2)
        {
            for (int i = 0; i < Data.Size; i++)
            {
                TableRow Row = new TableRow();
                Table2.Rows.Add(Row);

                for (int j = 0; j < Data.Size; j++)
                {
                    TableCell Cell = new TableCell();
                    Cell.Text = Data.GetData(i, j).ToString();
                    Cell.Width = 15;
                    Cell.Height = 15;
                    Row.Cells.Add(Cell);
                }
            }
        }
        /// <summary>
        /// Method used for printing the results into txt file
        /// </summary>
        /// <param name="ResultsFile">Name of the results txt file</param>
        /// <param name="Table1">Table from which the data will be taken from and later printed into txt file</param>
        public static void PrintResults(string ResultsFile, Table Table1)
        {
            using(var fr = File.AppendText(ResultsFile))
            {
                for(int i = 0; i < Table1.Rows.Count; i++)
                {
                    for(int j = 0; j < Table1.Rows[i].Cells.Count; j++)
                    {
                        fr.Write(Table1.Rows[i].Cells[j].Text + " ");
                    }
                    fr.WriteLine();
                }
            }
        }
        /// <summary>
        /// Method used for printing date into results file
        /// </summary>
        /// <param name="Data">Class GridData object which has all of the data of chess grid</param>
        /// <param name="ResultsFile">Name of the results txt file</param>
        public static void PrintData(GridData Data, string ResultsFile)
        {
            using (var fr = File.CreateText(ResultsFile))
            {
                for(int i = 0; i < Data.Size; i++)
                {
                    for(int j = 0; j < Data.Size; j++)
                    {
                        fr.Write(Data.GetData(i, j) + " ");

                    }
                    fr.WriteLine();
                }
                fr.WriteLine();
            }
        }
        /// <summary>
        /// Method which uses recursion to find the least amounts of moves horse would need to move in order to cross king and return to its original position
        /// </summary>
        /// <param name="Data">Class GridData object which has all of the data of chess grid</param>
        /// <param name="iz">The current x coordinates of the horse</param>
        /// <param name="jz">The current y coordinates of the horse</param>
        /// <param name="size">The size of the chess grid table in each direction (it is a square)</param>
        /// <param name="maxMoves">The maximum amount of moves horse will move before stopping</param>
        /// <param name="currentMove">The index of current move</param>
        /// <param name="foundKing">Used for checking if the horse has already found king</param>
        public static void FindLengh(GridData Data, int iz, int jz, int size, int maxMoves, int currentMove, int foundKing)
        {
            if (iz == Data.KingPositionX && jz == Data.KingPositionY) foundKing = 1;

            if(currentMove > maxMoves || currentMove >= Data.MinMoves)
            {
                return;
            }

            if(foundKing == 1 && iz == Data.HorsePositionX && jz == Data.HorsePositionY)
            {
                Data.MinMoves = currentMove;
                return;
            }
            
            FindLengh2(Data, iz + 2, jz - 1, size, maxMoves, currentMove, foundKing);
            FindLengh2(Data, iz + 2, jz + 1, size, maxMoves, currentMove, foundKing);
            FindLengh2(Data, iz - 2, jz - 1, size, maxMoves, currentMove, foundKing);
            FindLengh2(Data, iz - 2, jz + 1, size, maxMoves, currentMove, foundKing);
            FindLengh2(Data, iz + 1, jz - 2, size, maxMoves, currentMove, foundKing);
            FindLengh2(Data, iz - 1, jz - 2, size, maxMoves, currentMove, foundKing);
            FindLengh2(Data, iz + 1, jz + 2, size, maxMoves, currentMove, foundKing);
            FindLengh2(Data, iz - 1, jz + 2, size, maxMoves, currentMove, foundKing);
        }
        /// <summary>
        /// Method which uses recursion to find the least amounts of moves horse would need to move in order to cross king and return to its original position
        /// </summary>
        /// <param name="Data">Class GridData object which has all of the data of chess grid</param>
        /// <param name="iz">The current x coordinates of the horse</param>
        /// <param name="jz">The current y coordinates of the horse</param>
        /// <param name="size">The size of the chess grid table in each direction (it is a square)</param>
        /// <param name="maxMoves">The maximum amount of moves horse will move before stopping</param>
        /// <param name="currentMove">The index of current move</param>
        /// <param name="foundKing">Used for checking if the horse has already found king</param>
        public static void FindLengh2(GridData Data, int iz, int jz, int size, int maxMoves, int currentMove, int foundKing)
        {
            if (iz >= 0 && iz < size && jz >= 0 && jz < size && CheckIfTaken(Data, iz, jz) == false)
            {
                Data.SetData(iz, jz, "+");
                FindLengh(Data, iz, jz, size, maxMoves, currentMove + 1, foundKing);
                Data.SetData(iz, jz, "0");
            }
        }
        /// <summary>
        /// Method which uses recursion to find the shortest path to cross the king and return to its original position
        /// </summary>
        /// <param name="Data">Class GridData object which has all of the data of chess grid</param>
        /// <param name="iz">The current x coordinates of the horse</param>
        /// <param name="jz">The current y coordinates of the horse</param>
        /// <param name="size">The size of the chess grid table in each direction (it is a square)</param>
        /// <param name="currentMove">The index of current move</param>
        /// <param name="foundKing">Used for checking if the horse has already found king</param>
        /// <param name="foundPath">Used for checking if horse has found the shortest path to cross the king and return to its original position</param>
        public static void FindPath(GridData Data, int iz, int jz, int size, int currentMove, int foundKing, ref int foundPath)
        {
            if (iz == Data.KingPositionX && jz == Data.KingPositionY) foundKing = 1;

            if (currentMove > Data.MinMoves)
            {
                return;
            }

            if (foundKing == 1 && iz == Data.HorsePositionX && jz == Data.HorsePositionY && currentMove == Data.MinMoves)
            {
                foundPath = 1;
                return;
            }

            FindPath2(Data, iz + 2, jz - 1, size, currentMove, foundKing, ref foundPath);
            FindPath2(Data, iz + 2, jz + 1, size, currentMove, foundKing, ref foundPath);
            FindPath2(Data, iz - 2, jz - 1, size, currentMove, foundKing, ref foundPath);
            FindPath2(Data, iz - 2, jz + 1, size, currentMove, foundKing, ref foundPath);
            FindPath2(Data, iz + 1, jz - 2, size, currentMove, foundKing, ref foundPath);
            FindPath2(Data, iz - 1, jz - 2, size, currentMove, foundKing, ref foundPath);
            FindPath2(Data, iz + 1, jz + 2, size, currentMove, foundKing, ref foundPath);
            FindPath2(Data, iz - 1, jz + 2, size, currentMove, foundKing, ref foundPath);
        }
        /// <summary>
        /// Method which uses recursion to find the shortest path to cross the king and return to its original position
        /// </summary>
        /// <param name="Data">Class GridData object which has all of the data of chess grid</param>
        /// <param name="iz">The current x coordinates of the horse</param>
        /// <param name="jz">The current y coordinates of the horse</param>
        /// <param name="size">The size of the chess grid table in each direction (it is a square)</param>
        /// <param name="currentMove">The index of current move</param>
        /// <param name="foundKing">Used for checking if the horse has already found king</param>
        /// <param name="foundPath">Used for checking if horse has found the shortest path to cross the king and return to its original position</param>
        public static void FindPath2(GridData Data, int iz, int jz, int size, int currentMove, int foundKing, ref int foundPath)
        {
            if (iz >= 0 && iz < size && jz >= 0 && jz < size && CheckIfTaken(Data, iz, jz) == false && foundPath == 0)
            {
                Data.SetData(iz, jz, "+");
                FindPath(Data, iz, jz, size, currentMove + 1, foundKing, ref foundPath);
                Data.SetData(iz, jz, "0");

                if (foundPath == 1)
                {
                    if (Data.GetData(iz, jz) == "0")
                    {
                        Data.SetData(iz, jz, currentMove.ToString());
                    }
                    return;
                }
            }
        }

    }
}