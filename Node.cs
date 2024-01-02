using System.Collections.Generic;

namespace AntColony
{
    public class Node
    {
        public string name;
        public int resource;

        public Dictionary<string, Edge> edges;
        public Position position;
        public Node toHome;

        public Node(string name, int xPoint, int yPoint, int resource)
        {
            this.name = name;
            position = Position.FromPoints(xPoint, yPoint);
            this.resource = resource;
            edges = new Dictionary<string, Edge>();
        }
    }
}