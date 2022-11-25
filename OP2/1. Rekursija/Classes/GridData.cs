using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rekursija
{
    /// <summary>
    /// Class used for storing the data of the chess table grid.
    /// </summary>
    public class GridData
    {
        private string[,] CellData;
        public int Size { get; private set; }

        public int MinMoves { get; set; }

        public int HorsePositionX { get; private set; }
        public int HorsePositionY { get; private set; }
        public int KingPositionX { get; private set; }
        public int KingPositionY { get; private set; }

        public GridData(string[,] cellData, int size, int minMoves)
        {
            this.CellData = new string[size, size];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    this.CellData[i, j] = cellData[i, j];
                }
            }
            this.Size = size;
            this.MinMoves = minMoves;
        }
        /// <summary>
        /// Method used for getting the data of specific grid cell
        /// </summary>
        /// <param name="i">x coordinate of cell in the grid</param>
        /// <param name="j">y coordinate of cell in the grid</param>
        /// <returns>Data of specific grid cell</returns>
        public string GetData(int i, int j)
        {
            return CellData[i, j];
        }
        /// <summary>
        /// Changes the data of specific grid cell
        /// </summary>
        /// <param name="i">x coordinate of cell in the grid</param>
        /// <param name="j">y coordinate of cell in the grid</param>
        /// <param name="symbol">symbol into which cell's date will be changed to</param>
        public void SetData(int i, int j, string symbol)
        {
            if (CellData[i, j] != "Z" && CellData[i, j] != "K")
            {
                CellData[i, j] = symbol;
            }
        }
        /// <summary>
        /// Method used for saving horse x and y coordinates in the chess grid data class
        /// </summary>
        /// <param name="x">x coordinate of horse in the grid</param>
        /// <param name="y">y coordinate of horse in the grid</param>
        public void SetHorsePosition(int x, int y)
        {
            HorsePositionX = x;
            HorsePositionY = y;
        }
        /// <summary>
        /// Method used for saving king x and y coordinates in the chess grid data class
        /// </summary>
        /// <param name="x">x coordinate of king in the grid</param>
        /// <param name="y">y coordinate of king in the grid</param>
        public void SetKingPosition(int x, int y)
        {
            KingPositionX = x;
            KingPositionY = y;
        }
    }
}