using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorytmDijkstry
{
    class Dijkstra
    {
        private int[,] adjacencyMatrix;
        private double[] distance;
        public Dijkstra(int nodeCount)
        {
            this.adjacencyMatrix = new int[nodeCount, nodeCount];
            this.distance = new double[nodeCount];
        }
        public void SetAdjacencyMatrix(int m, int n, int value)
        {
            if (m == n)
                adjacencyMatrix[m, n] = 0;  //odległość z miasta m do miasta m musi być zerowa
            else
            {
                adjacencyMatrix[m, n] = value;
                adjacencyMatrix[n, m] = value;  //graf nieskierowany - macierz jest symetryczna
            }
        }
        public int[,] GetAdjacencyMatrix()
        {
            return adjacencyMatrix;
        }
        public void SetDistance(int cityNum, double value)
        {
            distance[cityNum] = value;
        }
        public double[] GetDistance()
        {
            return distance;
        }
        public void DisplayAdjacencyMatrix()
        {
            for (int m = 0; m < GetAdjacencyMatrix().GetLength(0); m++)
            {
                for (int n = 0; n < GetAdjacencyMatrix().GetLength(1); n++)
                    Console.Write(GetAdjacencyMatrix()[m, n] + "\t");
                Console.WriteLine();
            }
        }
        public void NewEdge(int m, int n, int weight)
        {
            if (weight > 0)
                SetAdjacencyMatrix(m, n, weight);
        }
        public void CreateEdges()
        {
            Random rnd = new Random();
            for (int i = 0; i < 10; i++)    //połączenie dla każdego miasta
            {
                int r;
                do
                {
                    r = rnd.Next(10);
                } while (r == i);   //krawędź musi znajdować się między dwoma różnymi węzłami
                SetAdjacencyMatrix(i, r, rnd.Next(1, 20));
            }
            for (int j = 0; j < 8; j++) //dodatkowe losowe połączenia
            {
                SetAdjacencyMatrix(rnd.Next(10), rnd.Next(10), rnd.Next(1, 20));
            }
        }
        public List<int>[] CalculateDistance(int cityNum)
        {
            List<int>[] waypoints = new List<int>[GetAdjacencyMatrix().GetLength(0)];

            for (int b = 0; b < GetAdjacencyMatrix().GetLength(0); b++)
            {
                SetDistance(b, double.PositiveInfinity);
                waypoints[b] = new List<int>();
            }
            bool[] visited = new bool[GetAdjacencyMatrix().GetLength(0)];
            SetDistance(cityNum, 0);

            Queue<int> nodeQueue = new Queue<int>();
            nodeQueue.Enqueue(cityNum);

            while (nodeQueue.Count > 0)
            {
                int v = nodeQueue.Dequeue();
                for (int c = 0; c < GetAdjacencyMatrix().GetLength(0); c++)
                {
                    if (!visited[c] && GetAdjacencyMatrix()[v, c] > 0)    //c jest nieodwiedzony i c jest sąsiadem v
                    {
                        nodeQueue.Enqueue(c);
                        if (GetAdjacencyMatrix()[v, c] + GetDistance()[v] < GetDistance()[c])
                        {
                            SetDistance(c, GetAdjacencyMatrix()[v, c] + GetDistance()[v]);
                            if (v != cityNum)
                            {
                                waypoints[c].Clear();
                                waypoints[c].InsertRange(0, waypoints[v]);  //punkty pośrednie od cityNum do v
                                waypoints[c].Add(v);
                            }
                        }
                    }
                }
                visited[v] = true;
            }
            return waypoints;
        }
    }
}
