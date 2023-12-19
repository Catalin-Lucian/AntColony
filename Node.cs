using System;
using System.Collections.Generic;

namespace AntColony
{
    public class Node
    {
        public int _resource;
        public Node _toHome;
        public int _x;
        public int _y;


        public Node(int x, int y, int resource)
        {
            _x = x;
            _y = y;
            _resource = resource;
            Edges = new Dictionary<string, Edge>();
        }


        public string Name => $"{_x}_{_y}";
        public int Resource => _resource;

        public Node ToHome => _toHome;

        public Dictionary<string, Edge> Edges { get; }

        public void AddResource(int resource)
        {
            _resource += resource;

            Console.WriteLine("Node: " + Name + " resource: " + _resource + " added: " + resource + " total: " + _resource);
        }

        public void RemoveResource(int resource)
        {
            _resource -= resource;

            Console.WriteLine("Node: " + Name + " resource: " + _resource + " removed: " + resource + " total: " + _resource);
        }

        public void AddEdge(string nodeName, Edge edge)
        {
            Edges.Add(nodeName, edge);
        }

        public void AddToHome(Node node)
        {
            _toHome = node;
        }
    }
}