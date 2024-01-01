using System;
using System.Collections.Generic;

namespace AntColony
{
    public static class Utils
    {
        // World data
        public const int NoAnts = 10;
        public const int Speed = 200;

        // side of the square world
        public const int XPoints = 40;
        public const int YPoints = 20;

        public static int XPointSize = 30;
        public static int YPointSize = 30;

        //Graph data
        public const double MaxWeight = 50; // max node Weight
        public const double ResourceAddWeight = 2; // weight added when ant with resource passes
        public const double PassRemoveWeight = 1; // weight removed when ant passes without resource
        public const double TimePassRemoveWeight = 0.1; // weight removed when ant passes without resource


        public readonly static Random RandNoGen = new Random();


        public static void ParseMessage(string content, out string action, out List<string> parameters)
        {
            string[] t = content.Remove(content.Length - 1).Split();

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

        public static string Str(params object[] ps)
        {
            string s = "";

            for (int i = 0; i < ps.Length - 1; i++)
            {
                s += $"{ps[i]} ";
            }
            s += $"{ps[ps.Length - 1]}";

            return s;
        }
    }

    public class Position
    {
        private int _x;
        private int _y;

        public int X => _x * Utils.XPointSize;
        public int Y => _y * Utils.YPointSize;

        public Position(int x, int y) { _x = x; _y = y; }
        public Position(string x, string y)
        {
            _x = int.Parse(x);
            _y = int.Parse(y);
        }

        public override string ToString()
        {
            return $"{X} {Y}";
        }
    }

}