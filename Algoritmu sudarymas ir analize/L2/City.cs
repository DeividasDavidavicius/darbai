using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoritmaiL2
{
    class City
    {
        public int cityNumber { get; private set; }
        public int cityPoints { get; private set; }
        public int xCoord { get; private set; }
        public int yCoord { get; private set; }
        public int neighbourCount { get; private set; }

        private List<int> neighbours;
        public City(int cityNumber)
        {
            this.cityNumber = cityNumber;
            neighbours = new List<int>();
            neighbourCount = 0;
            cityPoints = 0;
            xCoord = 0;
            yCoord = 0;
        }

        public int GetNeighbour(int i)
        {
            return neighbours[i];
        }

        public void AddNeighbour(int value)
        {
            neighbours.Add(value);
            neighbourCount++;
        }

        public void SetX(int x)
        {
            xCoord = x;
        }

        public void SetY(int y)
        {
            yCoord = y;
        }

        public void SetPoints(int points)
        {
            cityPoints = points;
        }
    }
}
