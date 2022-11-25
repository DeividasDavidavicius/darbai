using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace AlgoritmaiL2
{
    class Program
    {
        static void Main(string[] args)
        {
            int iterationCount = 100;
            string resultsFile = "..\\..\\results.txt";

            using(var fr = File.CreateText(resultsFile)) { }
            int nn = 1;
            for(int i = 0; i < iterationCount; i++)
            {
                int fileNumber = i + 1;

                List<City> cities = new List<City>();
                Generation.MainGeneration(cities, 50000, 0, 5000, nn, "..\\..\\duomenys" + fileNumber + ".txt");

                List<City> cities2 = new List<City>();

                int resources = 0;
                double fillRate = 0;

                InOut.ReadFile(cities2, "..\\..\\duomenys" + fileNumber + ".txt", ref fillRate, ref resources);;

                int startCity = 0;


                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                TaskUtils.MainCalculations(cities2, startCity, resources, resultsFile, "duomenys" + fileNumber + ".txt");
                stopwatch.Stop();
                Console.WriteLine(stopwatch.Elapsed + " " + nn);
                nn *= 2;
            }
        }
    }
}
