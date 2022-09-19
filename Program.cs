using System;
using System.IO;

namespace Dejkstra
{
    class Program
    {
        static void Main(string[] args)
        {
            var path = "D:/Proger/Deikstra/TextFile.txt";
            using (StreamReader sr = new StreamReader(path))
            {
                string line = sr.ReadLine();
                //Console.WriteLine(line);
                int size = Convert.ToInt32(line); // Parse()
                                                  // Console.WriteLine(size);
                int n = int.Parse(line);
                //Console.WriteLine(n);
                var smeg = new int[size, size]; // м-ца смежности
                var cost = new int[size, size]; // м-ца стоимости  
                for (int i = 0; i < size; i++)
                {
                    for (int j = 0; j < size; j++)
                    {
                        smeg[i, j] = 0;
                        cost[i, j] = Int32.MaxValue;
                    }
                }
                string[] words;// массив строк
                int v; // вершина графа откуда дуга
                int w; // вершина графа куда входит дуга
                int c; // стоимость дуги
                while ((line = sr.ReadLine()) != null)
                {
                    words = line.Split(' ');
                    v = Convert.ToInt32(words[0]);
                    w = Convert.ToInt32(words[1]);
                    c = Convert.ToInt32(words[2]);
                    smeg[v - 1, w - 1] = 1;
                    cost[v - 1, w - 1] = c;
                    Console.WriteLine($"{v} {w} {c}");
                }
                //Console.WriteLine("Матрица смежности");
                //PrintMatrix(smeg);
                Console.WriteLine("Матрица стоимости");
                //PrintMatrix(cost);
                MakeNULL(cost);
                Console.WriteLine();
                DijkstraAlgo(cost, 0, n);
            }





            //DijkstraAlgo(cost, 0, n);
            Console.ReadKey();
        }

        private static int MinimumDistance(int[] distance, bool[] shortestPathTreeSet, int verticesCount)
        {
            int min = int.MaxValue;
            int minIndex = 0;

            for (int v = 0; v < verticesCount; ++v)
            {
                if (shortestPathTreeSet[v] == false && distance[v] <= min)
                {
                    min = distance[v];
                    minIndex = v;
                }
            }

            return minIndex;
        }

        private static void Print(int[] distance, int verticesCount)
        {
            Console.WriteLine("Вершина   Расстояние от источника");

            for (int i = 0; i < verticesCount; ++i)
                Console.WriteLine("{0}\t  {1}", i + 1, distance[i]);
        }

        public static void DijkstraAlgo(int[,] cost, int source, int verticesCount)
        {
            int[] distance = new int[verticesCount];
            bool[] shortestPathTreeSet = new bool[verticesCount];

            for (int i = 0; i < verticesCount; ++i)
            {
                distance[i] = int.MaxValue;
                shortestPathTreeSet[i] = false;
            }

            distance[source] = 0;

            for (int count = 0; count < verticesCount - 1; ++count)
            {
                int u = MinimumDistance(distance, shortestPathTreeSet, verticesCount);
                shortestPathTreeSet[u] = true;

                for (int v = 0; v < verticesCount; ++v)
                    if (!shortestPathTreeSet[v] && Convert.ToBoolean(cost[u, v]) && distance[u] != int.MaxValue && distance[u] + cost[u, v] < distance[v])
                        distance[v] = distance[u] + cost[u, v];
            }

            Print(distance, verticesCount);
        }
        static void PrintMatrix(int[,] arr)
        {
            int size = arr.GetLength(0);
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    Console.Write(arr[i, j] + " ");
                }
                Console.WriteLine();
            }

        }
        static void MakeNULL(int[,] arr)
        {
            int size = arr.GetLength(0);

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (arr[i, j] == Int32.MaxValue)
                    {
                        arr[i, j] = 0;
                    }
                    Console.Write(arr[i, j] + " ");
                }
                Console.WriteLine();
            }
        }
    }
}