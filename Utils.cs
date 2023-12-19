using System;
using System.Collections.Generic;

namespace AntColony
{
    public static class Utils
    {
        // World data
        public const int NoAnts = 5;
        public const int Delay = 200;

        // side of the square world
        public const int SizeX = 600;
        public const int SizeY = 400;

        //Graph data
        public const double MaxWeight = 50; // max node Weight
        public const double ResourceAddWeight = 2; // weight added when ant with resource passes
        public const double PassRemoveWeight = 1; // weight removed when ant passes without resource
        public const double TimePassRemoveWeight = 0.1; // weight removed when ant passes without resource


        public readonly static Random RandNoGen = new Random();


        public static void ParseMessage(string content, out string action, out List<string> parameters)
        {
            string[] t = content.Split();

            action = t[0];

            parameters = new List<string>();
            for (int i = 1; i < t.Length; i++)
                parameters.Add(t[i]);
        }

        public static void ParseMessage(string content, out string action, out string parameters)
        {
            string[] t = content.Split();

            action = t[0];

            parameters = "";

            if (t.Length <= 1)
                return;

            for (int i = 1; i < t.Length - 1; i++)
                parameters += t[i] + " ";
            parameters += t[t.Length - 1];
        }

        public static string Str(object p1, object p2)
        {
            return $"{p1} {p2}";
        }

        public static string Str(object p1, object p2, object p3)
        {
            return $"{p1} {p2}_{p3}";
        }

        public static Edge Connect(Node n1, Node n2)
        {
            var edge = new Edge(n1, n2, 0);
            n1.AddEdge(n2.Name, edge);
            n2.AddEdge(n1.Name, edge);

            if (n2.ToHome == null)
                n2.AddToHome(n1);

            return edge;
        }
    }
}