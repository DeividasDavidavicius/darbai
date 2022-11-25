using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoritmaiL2
{
    class Path
    {

        public bool leftStart { get; private set; }
        public int startCity { get; private set; }

        public int maxPoints { get; private set; }
        List<int> bestPath;
        public Path(int startCity)
        {
            this.startCity = startCity;
            leftStart = false;
            maxPoints = 0;
            bestPath = new List<int>();
        }

        public void SetLeftStart(bool hadLeft)
        {
            leftStart = hadLeft;
        }

        public void SetMaxPoints(int points)
        {
            this.maxPoints = points;
        }

        public void SetPath(List<int> path)
        {
            bestPath = new List<int>(path);
        }

        public int GetPath(int i)
        {
            return bestPath[i];
        }

        public int GetPathCount()
        {
            return bestPath.Count();
        }


    }
}
