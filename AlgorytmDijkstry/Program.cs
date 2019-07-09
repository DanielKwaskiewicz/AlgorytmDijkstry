using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorytmDijkstry
{
    class Program
    {
        public static string[] cityNames = {"Kraków", "Rzeszów", "Gdańsk", "Wrocław", "Warszawa", "Łódź", "Bydgoszcz",
            "Szczecin", "Lublin", "Opole"};
        public static int ChooseCity(string[] cityNames)
        {
            Console.WriteLine("Wybierz miasto przesuwając kursor klawiszami w/s i zatwierdź wybór klawiszem " +
                "enter:");
            for (int k = 0; k < cityNames.GetLength(0); k++)
            {
                Console.WriteLine("\t{0}", cityNames[k]);
            }
            Console.SetCursorPosition(0, Console.CursorTop - cityNames.GetLength(0));
            Console.Write(">");
            bool choice = false;
            int position = 0;
            while (!choice)
            {
                char cursor = Console.ReadKey(true).KeyChar;
                switch (cursor)
                {
                    case 'w':
                        if (position > 0)
                        {
                            Console.Write("\b \b");
                            Console.SetCursorPosition(0, Console.CursorTop - 1);
                            Console.Write(">");
                            position--;
                        }
                        break;
                    case 's':
                        if (position < cityNames.GetLength(0) - 1)
                        {
                            Console.Write("\b \b");
                            Console.SetCursorPosition(0, Console.CursorTop + 1);
                            Console.Write(">");
                            position++;
                        }
                        break;
                    case (char)13:
                        Console.SetCursorPosition(0, Console.CursorTop + cityNames.GetLength(0) - position);
                        choice = true;
                        break;
                    default:
                        break;
                }
            }
            return position;
        }
        public static void PrintRoutes(string[] cities, int cityNum, double[] distance, List<int>[] waypoints)
        {
            for (int l = 0; l < waypoints.GetLength(0); l++)
            {
                if (l == cityNum)
                    Console.WriteLine("{0} jest miastem startowym. Odległość wynosi {1} km.", cities[l], distance[l]);
                else if (waypoints[l].Count == 0)
                    Console.WriteLine("Najkrótsza droga do miasta {1} prowadzi bezpośrednio z " +
                        "miasta {0} i wynosi {2} km.", cities[cityNum], cities[l], distance[l]);
                else
                {
                    Console.Write("Najkrótsza droga do miasta {0} prowadzi przez następujące miasta: ", cities[l]);
                    foreach (int waypoint in waypoints[l])
                        Console.Write("{0}, ", cities[waypoint]);
                    Console.WriteLine("\b\b. Trasa ma długość {0} km.", distance[l]);
                }
            }
        }
        static void Main(string[] args)
        {
            Dijkstra di = new Dijkstra(10);
            di.CreateEdges();
            int cityNumber = ChooseCity(cityNames);
            PrintRoutes(cityNames, cityNumber, di.GetDistance(), di.CalculateDistance(cityNumber));

            Console.Write("\nNaciśnij 1, aby wyświetlić macierz sąsiedztwa lub naciśnij dowolny klawisz," +
                " aby zakończyć program.");
            bool display = false;
            while (true)
            {
                char matrix = Console.ReadKey().KeyChar;
                switch (matrix)
                {
                    case '1':
                        if (!display)
                        {
                            Console.WriteLine();
                            di.DisplayAdjacencyMatrix();
                            Console.WriteLine("Naciśnij dowolny klawisz, aby zakończyć program.");
                            display = true;
                        }
                        else
                            Environment.Exit(0);
                        break;
                    default:
                        Environment.Exit(0);
                        break;
                }
            }
        }
    }
}
