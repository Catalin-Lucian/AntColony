using System;
using System.Collections.Generic;

namespace AntColony
{
    public class Node
    {
        public int Resource;
        public Node ToHome;

        public Position Pos;

        public string Name;

        public Dictionary<string, Edge> Edges;

        public Node(String name, int x, int y, int resource)
        {
            Name = name;
            Pos = new Position(x, y);
            Resource = resource;
            Edges = new Dictionary<string, Edge>();
        }
    }
}