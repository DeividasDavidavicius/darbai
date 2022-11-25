using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace AlgoritmaiL3
{

    class U3
    {
        public double leastSum { get; set; }
        public List<int> leastOrder { get; set; }
        public U3()
        {
            leastSum = int.MaxValue;
            leastOrder = new List<int>();
        }

        public void Copy(List<int> copy)
        {
            leastOrder = new List<int>();
            for(int i = 0; i < copy.Count; i++)
            {
                leastOrder.Add(copy[i]);
            }    
        }
    }
    class Program
    {
        static long sk = 0;
        static void Main(string[] args)
        {
            int n = 2;

            for(int i = 0; i < 25; i++)
            {
                sk = 0;
                //Stopwatch watch = new Stopwatch();
                //watch.Start();
                U3(n);
                //Console.WriteLine("{0} {1}", watch.Elapsed, n);
                Console.WriteLine("{0} {1}", sk, n);

                n = n * 2;
            }

            //U1(n);
            //U2(n);

        }
        static int sk2 = 0;
        public static void U3(int n)
        {
            int k = 2;
            List<int> puslapiai = new List<int>();
            List<int> knygos = new List<int>();
            U3 Ats = new U3();

            Random random = new Random();

            for(int i = 0; i < n; i++)
            {
                puslapiai.Add(random.Next(1, 100));
                //Console.Write(puslapiai[i] + " ");
            }
            //Console.WriteLine();

            //for (int i = 0; i < puslapiai.Count; i++)
            //{
            //    Console.WriteLine(puslapiai[i]);
            //}

            for (int i = 0; i < k; i++)
            {
                knygos.Add(1);
            }

            //U3Recursive(n, k, puslapiai, knygos, k, Ats);

            //for(int i = 0; i < k; i++)
            //{
            //    Console.Write(Ats.leastOrder[i] + " ");
            //}
            //Console.WriteLine();

            //Console.WriteLine("DINAMINIS:");
            List<int> knygos2 = new List<int>();
            U3Dynamic(n , k, puslapiai, knygos2);

            //for(int i = 0; i < knygos2.Count; i++)
            //{
            //    Console.Write(knygos2[i] + " ");
            //}


        }

        public static void U3Dynamic(int n, int k, List<int> puslapiai, List<int> knygos)
        {
            sk++; double sum = 0;

            sk++; for (int i = 0; i < n; i++)
            {
                sk++; sum += puslapiai[i];
            }

            sk++; double average = sum / k;
            sk++; int likutis = n;
            sk++; int pradzia = 0;

            sk++; for (int i = 0; i < k; i++)
            {
                sk++; knygos.Add(1);
                sk++; likutis--;
                sk++; while (likutis > k - i - 1)
                {
                    sk++; double notAdded = CalculateDiff2(puslapiai, average, knygos[i], pradzia);
                    sk++; double added = CalculateDiff2(puslapiai, average, knygos[i] + 1, pradzia);


                    sk++; if (notAdded > added)
                    {
                        sk++; knygos[i] += 1;
                        sk++; likutis--;
                    }
                    else
                    {
                        sk++; pradzia += knygos[i];
                        sk++; break;
                    }
                }
            }

            //Console.WriteLine("CIA LIKUTIS" + likutis);
            sk++; if (likutis > 0)
            {
                sk++; knygos[0] += likutis;
            }

            //for (int i = 0; i < knygos.Count; i++)
            //{
            //    Console.Write(knygos[i] + " ");
            //}
            //Console.WriteLine();

            sk++; for (int i = 0; i < k; i++)
            {
                sk++; for (int j = 0; j < knygos[i]; j++)
                {
                    sk++; if (knygos[i] == 1)
                    {
                        sk++; break;
                    }

                    sk++; double diff = CalculateDiff3(puslapiai, knygos, average);
                    sk++; int lNumber = -1;

                    sk++; for (int l = 0; l < k; l++)
                    {
                        sk++; if (i != l)
                        {
                            sk++; knygos[i]--;
                            sk++; knygos[l]++;
                            sk++; double diff2 = CalculateDiff3(puslapiai, knygos, average);
                            sk++; knygos[i]++;
                            sk++; knygos[l]--;

                            sk++; if (diff2 <= diff)
                            {
                                sk++; diff = diff2;
                                sk++; lNumber = l;
                            }
                        }
                    }

                    sk++; if (lNumber != -1)
                    {
                        sk++; knygos[i]--;
                        sk++; knygos[lNumber]++;
                    }
                }
            }
        }

        public static double CalculateDiff3(List<int> puslapiai, List<int> knygos, double Average)
        {
            sk++; List<int> sumos = new List<int>();
            sk++; int current = 0;
            sk++; for (int i = 0; i < knygos.Count; i++)
            {
                sk++; sumos.Add(0);
                sk++; for (int j = current; j < current + knygos[i]; j++)
                {
                    sk++; sumos[i] += puslapiai[j];
                }
                sk++; current += knygos[i];
            }

            sk++; double Difference = 0;

     sk++;       for (int i = 0; i < knygos.Count; i++)
            {
                sk++; Difference += Math.Abs(Average - sumos[i]);
            }

            sk++; return Difference;
        }

        public static double CalculateDiff2(List<int> puslapiai, double average, int knygosI, int pradzia)
        {
            sk++; double sum = 0;

            sk++; for (int i = pradzia; i < pradzia + knygosI; i++)
            {
                sk++; sum += puslapiai[i];
            }


            sk++; return Math.Abs(average - sum);
        }

        public static void U3Recursive(int n, int k, List<int> puslapiai, List<int> knygos, int suma, U3 Ats)
        {
            if(suma >= n)
            {
                CalculateDiff(puslapiai, knygos, Ats);
                return;
            }

            for(int i = 0; i < k; i++)
            {
                knygos[i]++;
                U3Recursive(n, k, puslapiai, knygos, suma + 1, Ats);
                knygos[i]--;
            }
        }

        public static void CalculateDiff(List<int> puslapiai, List<int> knygos, U3 Ats)
        {
            List<int> sumos = new List<int>();
            int current = 0;
            for(int i = 0; i < knygos.Count; i++)
            {
                sumos.Add(0);
                for (int j = current; j < current + knygos[i]; j++)
                {
                    sumos[i] += puslapiai[j];
                }
                current += knygos[i];
            }

            double Average = 0;

            for(int i = 0; i < knygos.Count; i++)
            {
                Average += sumos[i];
            }

            Average = Average / knygos.Count;

            double Difference = 0;

            for(int i = 0; i < knygos.Count; i++)
            {
                Difference += Math.Abs(Average - sumos[i]);
            }

            if (Ats.leastSum > Difference)
            {
                //Console.WriteLine(Average + " " + Difference);
                Ats.leastSum = Difference;
                Ats.Copy(knygos);
            }
        }

        public static void U2(int n)
        {
            Random random = new Random();
            List<List<int>> Data = new List<List<int>>();

            for(int i = 0; i < n; i++)
            {
                List<int> iData = new List<int>();
                for(int j = 0; j < n; j++)
                {
                    iData.Add(random.Next(1, 10));
                }
                Data.Add(iData);
            }

            //for(int i = Data.Count - 1; i >= 0; i--)
            //{
            //    for(int j = Data[i].Count - 1; j >= 0; j--)
            //    {
            //        Console.Write(Data[i][j] + " ");
            //    }
            //    Console.WriteLine();
            //}

            //int MaxValueRecursive = U2Recursive(Data, 0, 0);
            //Console.WriteLine("Max path recursive: {0}", MaxValueRecursive);
            int MaxPathDynamic = U2Dynamic(Data);
            //Console.WriteLine("Math path dynamic: {0}", MaxPathDynamic);
        }

        public static int U2Recursive(List<List<int>> Data, int x, int y)
        {
            if (x == Data.Count || y == Data.Count)
            {
                return int.MinValue;
            }

            if (x == Data.Count - 1 && y == Data.Count - 1)
            {
                return Data[x][y];
            }

            int goNorth = U2Recursive(Data, x, y + 1);
            int goWest = U2Recursive(Data, x + 1, y);

            if (goNorth > goWest)
            {
                return goNorth + Data[x][y];
            }
            else
            {
                return goWest + Data[x][y];
            }
        }

        public static int U2Dynamic(List<List<int>> Data)
        {
            int[,] Values = new int[Data.Count,Data.Count];
            for(int i = 0; i < Data.Count; i++)
            {
                for(int j = 0; j < Data[i].Count; j++)
                {
                    Values[i, j] = Data[i][j];

                    if(i == 0 && j > 0)
                    {
                        Values[0, j] += Values[0, j - 1];
                    }
                    else if(j == 0 && i > 0)
                    {
                        Values[i, 0] += Values[i - 1, 0];
                    }
                    else if(i > 0 && j > 0)
                    {
                        Values[i, j] += Math.Max(Values[i - 1, j], Values[i, j - 1]);
                    }
                }
            }
            return Values[Data.Count - 1, Data.Count - 1];
        }


        public static void U1(int n)
        { 
            Random random = new Random();
            List<List<int>> Data = new List<List<int>>();

            for (int i = 0; i < n; i++)
            {
                List<int> iData = new List<int>();
                for (int j = 0; j < i + 1; j++)
                {
                    iData.Add(random.Next(1, 11));
                }
                Data.Add(iData);
            }

            //for (int i = 0; i < Data.Count; i++)
            //{
            //    for (int j = 0; j < Data[i].Count; j++)
            //    {
            //        Console.Write(Data[i][j] + " ");
            //    }
            //    Console.WriteLine();
            //}

            int MinSumDynamic = U1Dynamic(n, Data);
            //int MinSumRecursive = U1Recursive(n, Data, 0, 0);
            //Console.WriteLine("MinSumDynamic: {0}", MinSumDynamic);
            //Console.WriteLine("MinSumRecursive: {0}", MinSumRecursive);
        }

        public static int U1Recursive(int n, List<List<int>> Data, int i, int MinSum)
        {
            if (i >= n)
            {
                return 0;
            }
            int Min = int.MaxValue;
            for (int j = 0; j < Data[i].Count; j++)
            {
                int CurrentMin = U1Recursive(n, Data, i + 1, MinSum) + Data[i][j];
                if (CurrentMin < Min)
                {
                    Min = CurrentMin;
                }
            }
            MinSum += Min;
            return MinSum;
        }

        public static int U1Dynamic(int n, List<List<int>> Data)
        {
            List<int> MinNumbers = new List<int>();
            int MinSum = 0;

            for(int i = 0; i < Data.Count; i++)
            {
                int Min = int.MaxValue;
                for(int j = 0; j < Data[i].Count; j++)
                {
                    if(Min > Data[i][j])
                    {
                        Min = Data[i][j];
                    }
                }
                MinNumbers.Add(Min);
            }

            for(int i = 0; i < MinNumbers.Count; i++)
            {
                MinSum += MinNumbers[i];
            }

            return MinSum;
        }
    }
}
