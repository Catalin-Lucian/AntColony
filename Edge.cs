namespace AntColony
{
    public class Edge
    {
        public Node A;
        public Node B;

        private double _weight;
        public double Weight => _weight;

        public Edge(Node nodeA, Node nodeB, double weight)
        {
            A = nodeA;
            B = nodeB;
            _weight = weight;
        }

        public void IncreseWeight(double value)
        {
            _weight += value;
            if (_weight > Utils.MaxWeight) _weight = Utils.MaxWeight;
        }

        public void DecreseWeight(double value)
        {
            _weight -= value;
            if (_weight < 0) _weight = 0;
        }
    }
}
