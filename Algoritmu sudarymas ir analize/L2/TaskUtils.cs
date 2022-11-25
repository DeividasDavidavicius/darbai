using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoritmaiL2
{
    class TaskUtils
    {
        public static void MainCalculations(List<City> cities, int startCity, int resources, string resultsFile, string dataFile)
        {
            Path path = new Path(startCity);
            List<int> alreadyVisited = new List<int>();
            List<int> currentPath = new List<int>();

            for(int i = 0; i < cities.Count; i++)
            {
                alreadyVisited.Add(0);
            }

            //FindPath(cities, path, resources, startCity, currentPathPoints, alreadyVisited, currentPath);

            InOut.PrintResults(path, resultsFile, dataFile);

            FindPath2(cities, resources, startCity);
        }

        public static void FindPath2(List<City> cities, int resources, int startCity)
        {
            int startPoints = cities[startCity].cityPoints;
            List<int> foundPath = new List<int>();
            int maxPoints = 0;
            for(int i = 0; i < cities[startCity].neighbourCount; i++)
            {
                int currentResources = resources / 2;
                int currentCity = cities[startCity].GetNeighbour(i);
                currentResources -= CalculateDistance(cities[startCity].xCoord, cities[startCity].yCoord, cities[currentCity].xCoord, cities[currentCity].yCoord);
                List<int> visited = new List<int>();
                visited.Add(startCity);
                visited.Add(currentCity);

                List<int> bestPath = new List<int>();
                List<int> path = new List<int>();
                bestPath.Add(startCity);
                path.Add(startCity);
                bestPath.Add(currentCity);
                path.Add(currentCity);

                int currentPoints = startPoints;
                currentPoints += cities[currentCity].cityPoints;

                List<int> alreadyVisited = new List<int>();
                for(int j = 0; j < cities.Count; j++)
                {
                    alreadyVisited.Add(0);
                }

                alreadyVisited[startCity] = 1;
                alreadyVisited[currentCity] = 1;


                while (currentResources > 0)
                {
                    if (currentCity == startCity)
                    {
                        break;
                    }

                    int bestNeighbour = -1;
                    int bestPoints = -100;
                    int neighbourCount = cities[currentCity].neighbourCount;

                    for(int j = 0; j < neighbourCount; j++)
                    {
                        int neighbourNumber = cities[currentCity].GetNeighbour(j);
                        if(cities[neighbourNumber].cityPoints > bestPoints && alreadyVisited[neighbourNumber] == 0)
                        {
                            bestPoints = cities[neighbourNumber].cityPoints;
                            bestNeighbour = neighbourNumber;
                        }
                    }

                    int cost = 0;

                    if(bestNeighbour == -1)
                    {
                        path.RemoveAt(path.Count - 1);
                        cost = CalculateDistance(cities[currentCity].xCoord, cities[currentCity].yCoord, cities[path[path.Count - 1]].xCoord, cities[path[path.Count - 1]].yCoord);
                        if(cost > currentResources)
                        {
                            break;
                        }
                        bestPath.Add(path[path.Count - 1]);
                        currentResources -= cost;
                        currentCity = path[path.Count - 1];
                    }
                    else
                    {
                        cost = CalculateDistance(cities[currentCity].xCoord, cities[currentCity].yCoord, cities[bestNeighbour].xCoord, cities[bestNeighbour].yCoord);
                        if(cost > currentResources)
                        {
                            break;
                        }

                        alreadyVisited[bestNeighbour] = 1;
                        bestPath.Add(bestNeighbour);
                        path.Add(bestNeighbour);

                        currentResources -= cost;
                        currentCity = bestNeighbour;
                        currentPoints += bestPoints;
                    }    
                }
                if(maxPoints < currentPoints)
                {
                    maxPoints = currentPoints;
                    foundPath = bestPath;
                }

            }
            Console.WriteLine(maxPoints + " maxPoints");
            for(int i = 0; i < foundPath.Count; i++)
            {
                //Console.Write(foundPath[i] + " ");
            }
            for(int i = foundPath.Count - 2; i >= 0; i--)
            {
                //Console.Write(foundPath[i] + " ");
            }
            //Console.WriteLine();
        }

        public static void FindPath(List<City> cities, Path path, int resources, int currentCity, int currentPathPoints, List<int> alreadyVisited, List<int> currentPath)
        {
            currentPath.Add(currentCity);

            if (alreadyVisited[currentCity] == 0)
            {
                currentPathPoints += cities[currentCity].cityPoints;
            }

            alreadyVisited[currentCity]++;

            if(resources <= 0)
            {
                return;
            }

            if (path.leftStart == true && currentCity == path.startCity)
            {
                if(path.maxPoints < currentPathPoints)
                {
                    path.SetMaxPoints(currentPathPoints);
                    path.SetPath(currentPath);
                }
                return;
            }

            if(path.leftStart == false)
            {
                path.SetLeftStart(true);
            }

            for(int i = 0; i < cities[currentCity].neighbourCount; i++)
            {
                int cost = CalculateDistance(cities[currentCity].xCoord, cities[currentCity].yCoord, cities[cities[currentCity].GetNeighbour(i)].xCoord, cities[cities[currentCity].GetNeighbour(i)].yCoord);
                FindPath(cities, path, resources - cost, cities[currentCity].GetNeighbour(i), currentPathPoints, alreadyVisited, currentPath);
                alreadyVisited[cities[currentCity].GetNeighbour(i)]--;
                currentPath.RemoveAt(currentPath.Count - 1);
            }
        }

        public static int CalculateDistance(int x1, int y1, int x2, int y2)
        {
            return 15;
            double distance = Math.Ceiling(Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2)));
            return Convert.ToInt32(distance);
        }
    }
}
