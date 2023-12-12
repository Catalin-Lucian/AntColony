using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntColony
{
    public class Node
    {
        public int _x;
        public int _y;
        public int _resource;
        public Node _toHome;
        public Dictionary<string, Edge> _edges;
        
        public string Name => $"{_x}_{_y}";

        public Node(int x, int y, int resource)
        {
            _x = x;
            _y = y;
            _resource = resource;
        }

        public void AddEdge(string nodeName, Edge edge)
        {
            _edges.Add(nodeName, edge);
        }

        public void AddToHome(Node node)
        {
            _toHome = node;
        }
    }
}
