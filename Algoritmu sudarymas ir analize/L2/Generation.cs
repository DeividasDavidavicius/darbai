using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoritmaiL2
{
    class Generation
    {
        public static void MainGeneration(List<City> cities, int v, double fillRate, int mapSize, int resources, string filePath)
        {

            CreateMap(cities, v, mapSize);

            double maxEdges = (v - 1) * v / 2;
            double minEdges = v - 1;

            double extraEdges = Math.Round((maxEdges - minEdges) * fillRate);

            SingleChainGraph(cities);

            GenerateExtraEdges(cities, extraEdges);

            InOut.PrintData(cities, filePath, fillRate, resources);
        }

        public static void CreateMap(List<City> cities, int v, int mapSize)
        {
            for(int i = 0; i < v; i++)
            {
                City city = new City(i);
                cities.Add(city);
            }

            GenerateCoordinates(cities, mapSize);
            GeneratePoints(cities);
        }

        public static void GenerateCoordinates(List<City> cities, int mapSize)
        {
            Random random = new Random();
            List<int> xCoordinates = new List<int>();
            List<int> yCoordinates = new List<int>();

            int forLength = cities.Count;

            for(int i = 0; i < forLength; i++)
            {
                int xCoord = random.Next(0, mapSize);
                int yCoord = random.Next(0, mapSize);

                bool foundDuplicate = false;

                for(int j = 0; j < xCoordinates.Count; j++)
                {
                    if(xCoordinates[j] == xCoord && yCoordinates[j] == yCoord)
                    {
                        forLength++;
                        foundDuplicate = true;
                        break;
                    }
                }

                if (foundDuplicate)
                {
                    foundDuplicate = false;
                }
                else
                {
                    xCoordinates.Add(xCoord);
                    yCoordinates.Add(yCoord);
                }
            }

            SetCoordinates(cities, xCoordinates, yCoordinates);


        }

        public static void SetCoordinates(List<City> cities, List<int> xCoordinates, List<int> yCoordinates)
        {
            for(int i = 0; i < cities.Count; i++)
            {
                cities[i].SetX(xCoordinates[i]);
                cities[i].SetY(yCoordinates[i]);
            }
        }

        public static void GeneratePoints(List<City> cities)
        {
            Random random = new Random();

            for(int i = 0; i < cities.Count; i++)
            {
                int negOrPos = random.Next(1, 6);
                if(negOrPos == 1)
                {
                    cities[i].SetPoints(-1 * random.Next(1, 4));
                }
                else
                {
                    cities[i].SetPoints(random.Next(1, 6));
                }
            }
        }

        public static void SingleChainGraph(List<City> cities)
        {

            cities[0].AddNeighbour(1);
            for(int i = 1; i < cities.Count - 1; i++)
            {
                cities[i].AddNeighbour(i - 1);
                cities[i].AddNeighbour(i + 1);
            }
            cities[cities.Count - 1].AddNeighbour(cities.Count - 2);
        }

        public static void GenerateExtraEdges(List<City> cities, double extraEdges)
        {
            Random random = new Random();

            for(int i = 0; i < extraEdges; i++)
            {
                int vertice1 = random.Next(0, cities.Count);
                int vertice2 = random.Next(0, cities.Count);

                while(vertice1 == vertice2 || CheckIfHas(cities[vertice1], vertice2))
                {
                    vertice1 = random.Next(0, cities.Count);
                    vertice2 = random.Next(0, cities.Count);
                }

                cities[vertice1].AddNeighbour(vertice2);
                cities[vertice2].AddNeighbour(vertice1);
            }
        }

        public static bool CheckIfHas(City city, int vertice)
        {
            for(int i = 0; i < city.neighbourCount; i++)
            {
                if(city.GetNeighbour(i) == vertice)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
