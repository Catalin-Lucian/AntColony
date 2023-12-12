using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntColony
{
    public class Edge
    {
        public Node NodeA;
        public Node NodeB;
        public double Weight;

        public Edge(Node nodeA, Node nodeB, double weight)
        {
            NodeA = nodeA;
            NodeB = nodeB;
            Weight = weight;
        }
    }
}
