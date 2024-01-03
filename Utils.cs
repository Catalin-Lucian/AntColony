using System;
using System.Collections.Generic;

namespace AntColony
{
    public static class Utils
    {
        public static bool IsOptimizationActive = true;
        public static int NrOfNodesToCarry = 3;

        // World data
        public const int NoAnts = 10;
        public const int Speed = 15;

        // side of the square world
        public const int XPoints = 40;
        public const int YPoints = 20;

        public static int XPointSize = 30;
        public static int YPointSize = 30;

        //Graph data
        public const double MaxWeight = 30; // max node Weight
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
        public int x;
        public int y;

        public Position(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public Position(string x, string y)
        {
            this.x = int.Parse(x);
            this.y = int.Parse(y);
        }

        public Position(Position p)
        {
            this.x = p.x;
            this.y = p.y;
        }

        static public Position FromPoints(int xPoint, int yPoint)
        {
            return new Position(xPoint * Utils.XPointSize, yPoint * Utils.YPointSize);
        }

        public override string ToString()
        {
            return $"{x} {y}";
        }

        public bool IsEqual(Position p)
        {
            return x == p.x && y == p.y;
        }

        public bool IsNotEqual(Position p)
        {
            return x != p.x || y != p.y;
        }

        public Position MoveTo(Position toPosition, int speed)
        {
            int x = toPosition.x;
            int y = toPosition.y;

            if (x == this.x && y == this.y)
                return new Position(this);

            int dx = x - this.x;
            int dy = y - this.y;

            double distance = Math.Sqrt(dx * dx + dy * dy);
            if (distance < speed)
                // we are close enough to the target
                return new Position(toPosition);

            // calculate next point 
            double ratio = speed / distance;
            int nextX = (int)(this.x + dx * ratio);
            int nextY = (int)(this.y + dy * ratio);
            return new Position(nextX, nextY);
        }
    }

}