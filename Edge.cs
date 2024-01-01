namespace AntColony
{
    public class Edge
    {
        public Node A;
        public Node B;
        public double Weight;

        public Edge(Node nodeA, Node nodeB, double weight)
        {
            A = nodeA;
            B = nodeB;
            Weight = weight;
        }
    }
}
