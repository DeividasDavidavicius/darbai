using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AlgoritmaiL2
{
    class InOut
    {
        public static void PrintData(List<City> cities, string fileName, double fillRate, int resources)
        {
            using (var fr = File.CreateText(fileName))
            {
                fr.WriteLine(resources);
                fr.WriteLine(cities.Count);
                fr.WriteLine(fillRate);
            }

            PrintCityNeighbours(cities, fileName);
            PrintCityCoordinates(cities, fileName);
            PrintCityPoints(cities, fileName);
        }

        public static void PrintCityNeighbours(List<City> cities, string fileName)
        {
            using(var fr = File.AppendText(fileName))
            {
                for (int i = 0; i < cities.Count; i++)
                {
                    for (int j = 0; j < cities[i].neighbourCount; j++)
                    {
                        fr.Write(cities[i].GetNeighbour(j) + " ");
                    }
                    fr.WriteLine();
                }
            }
        }

        public static void PrintCityCoordinates(List<City> cities, string fileName)
        {
            using (var fr = File.AppendText(fileName))
            {
                for (int i = 0; i < cities.Count; i++)
                {
                    fr.WriteLine(cities[i].xCoord + " " + cities[i].yCoord);
                }
            }
        }

        public static void PrintCityPoints(List<City> cities, string fileName)
        {
            using (var fr = File.AppendText(fileName))
            {
                for (int i = 0; i < cities.Count; i++)
                {
                    fr.WriteLine(cities[i].cityPoints);
                }
            }
        }

        public static void ReadFile(List<City> cities, string filePath, ref double fillRate, ref int resources)
        {
            using(StreamReader reader = new StreamReader(filePath))
            {
                resources = Convert.ToInt32(reader.ReadLine());
                int cityCount = Convert.ToInt32(reader.ReadLine());
                fillRate = Convert.ToDouble(reader.ReadLine());

                for (int i = 0; i < cityCount; i++)
                {
                    City city = new City(i);

                    string line = reader.ReadLine();
                    string[] parts = line.Split(' ');

                    for(int j = 0; j < parts.Count() - 1; j++)
                    {
                        city.AddNeighbour(Convert.ToInt32(parts[j]));
                    }

                    cities.Add(city);
                }

                for(int i = 0; i < cityCount; i++)
                {
                    string line = reader.ReadLine();
                    string[] coordinates = line.Split(' ');
                    int xCoord = Convert.ToInt32(coordinates[0]);
                    int yCoord = Convert.ToInt32(coordinates[1]);

                    cities[i].SetX(xCoord);
                    cities[i].SetY(yCoord);
                }

                for(int i = 0; i < cityCount; i++)
                {
                    string line = reader.ReadLine();
                    int cityPoints = Convert.ToInt32(line);
                    cities[i].SetPoints(cityPoints);
                }
            }
        }

        public static void PrintResults(Path path, string resultsFile, string dataFile)
        {
            using(var fr = File.AppendText(resultsFile))
            {
                fr.WriteLine("Rezultatai iš duomenų failo " + dataFile + ":");
                fr.WriteLine("Max points: " + path.maxPoints);
                fr.WriteLine("Cities visited: " + path.GetPathCount());
                fr.Write("City sequence: ");
                for (int i = 0; i < path.GetPathCount(); i++)
                {
                    fr.Write(path.GetPath(i) + " ");
                }
                fr.WriteLine();
            }
        }

    }
}
