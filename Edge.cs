using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntColony
{
    class Edge
    {
        public Node _nodeA;
        public Node _nodeB;
        public double _weight;

        public Edge(Node nodeA, Node nodeB, double weight)
        {
            _nodeA = nodeA;
            _nodeB = nodeB;
            _weight = weight;
        }
    }
}
