using ActressMas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Message = ActressMas.Message;

namespace AntColony
{
    public class PlanetAgent : Agent
    {
        private PlanetForm _formGui;


        public PlanetAgent()
        {
            CreateNodes();

            var t = new Thread(GuiThread);
            t.Start();
        }

        public Dictionary<string, Node> Nodes { get; set; }
        public List<Edge> Edges { get; set; }

        public Node BaseNode { get; set; }

        private void CreateNodes()
        {
            Nodes = new Dictionary<string, Node>();
            Edges = new List<Edge>();

            // create base node 
            BaseNode = new Node(Utils.SizeX / 2, Utils.SizeY / 2, 0);

            // create left nodes
            var n1 = new Node(260, 200, 0);
            var n2 = new Node(230, 160, 0);
            var n3 = new Node(230, 240, 0);
            var n4 = new Node(230, 120, 0);
            var n5 = new Node(200, 160, 0);
            var n6 = new Node(200, 120, 5);
            var n7 = new Node(170, 200, 0);
            var n8 = new Node(140, 160, 0);
            var n9 = new Node(140, 230, 0);
            var n10 = new Node(140, 120, 8);
            var n11 = new Node(110, 160, 20);
            var n12 = new Node(140, 280, 0);
            var n13 = new Node(110, 240, 0);
            var n14 = new Node(110, 320, 10);

            Nodes.Add(n1.Name, n1);
            Nodes.Add(n2.Name, n2);
            Nodes.Add(n3.Name, n3);
            Nodes.Add(n4.Name, n4);
            Nodes.Add(n5.Name, n5);
            Nodes.Add(n6.Name, n6);
            Nodes.Add(n7.Name, n7);
            Nodes.Add(n8.Name, n8);
            Nodes.Add(n9.Name, n9);
            Nodes.Add(n10.Name, n10);
            Nodes.Add(n11.Name, n11);
            Nodes.Add(n12.Name, n12);
            Nodes.Add(n13.Name, n13);
            Nodes.Add(n14.Name, n14);

            Edges.Add(Utils.Connect(BaseNode, n1));
            Edges.Add(Utils.Connect(n1, n2));
            Edges.Add(Utils.Connect(n1, n3));
            Edges.Add(Utils.Connect(n2, n4));
            Edges.Add(Utils.Connect(n2, n5));
            Edges.Add(Utils.Connect(n5, n6));
            Edges.Add(Utils.Connect(n5, n7));
            Edges.Add(Utils.Connect(n7, n8));
            Edges.Add(Utils.Connect(n7, n9));
            Edges.Add(Utils.Connect(n8, n10));
            Edges.Add(Utils.Connect(n8, n11));
            Edges.Add(Utils.Connect(n3, n12));
            Edges.Add(Utils.Connect(n12, n13));
            Edges.Add(Utils.Connect(n12, n14));

            // create right nodes
            var n15 = new Node(300, 240, 0);
            var n16 = new Node(300, 280, 10);
            var n17 = new Node(330, 160, 10);
            var n18 = new Node(330, 200, 0);
            var n19 = new Node(330, 280, 0);
            var n20 = new Node(330, 320, 10);
            var n21 = new Node(360, 160, 10);
            var n22 = new Node(360, 240, 0);
            var n23 = new Node(360, 280, 0);
            var n24 = new Node(360, 120, 0);
            var n25 = new Node(390, 120, 10);
            var n26 = new Node(390, 160, 0);
            var n27 = new Node(390, 200, 0);
            var n28 = new Node(390, 280, 0);
            var n29 = new Node(390, 320, 10);
            var n30 = new Node(420, 160, 10);

            Nodes.Add(n15.Name, n15);
            Nodes.Add(n16.Name, n16);
            Nodes.Add(n17.Name, n17);
            Nodes.Add(n18.Name, n18);
            Nodes.Add(n19.Name, n19);
            Nodes.Add(n20.Name, n20);
            Nodes.Add(n21.Name, n21);
            Nodes.Add(n22.Name, n22);
            Nodes.Add(n23.Name, n23);
            Nodes.Add(n24.Name, n24);
            Nodes.Add(n25.Name, n25);
            Nodes.Add(n26.Name, n26);
            Nodes.Add(n27.Name, n27);
            Nodes.Add(n28.Name, n28);
            Nodes.Add(n29.Name, n29);
            Nodes.Add(n30.Name, n30);

            Edges.Add(Utils.Connect(BaseNode, n15));
            Edges.Add(Utils.Connect(n15, n16));
            Edges.Add(Utils.Connect(n16, n3));
            Edges.Add(Utils.Connect(BaseNode, n17));
            Edges.Add(Utils.Connect(BaseNode, n18));
            Edges.Add(Utils.Connect(n16, n19));
            Edges.Add(Utils.Connect(n19, n20));
            Edges.Add(Utils.Connect(n18, n21));
            Edges.Add(Utils.Connect(n18, n22));
            Edges.Add(Utils.Connect(n15, n23));
            Edges.Add(Utils.Connect(n22, n23));
            Edges.Add(Utils.Connect(n23, n29));
            Edges.Add(Utils.Connect(n21, n24));
            Edges.Add(Utils.Connect(n21, n25));
            Edges.Add(Utils.Connect(n21, n26));
            Edges.Add(Utils.Connect(n22, n27));
            Edges.Add(Utils.Connect(n22, n28));
            Edges.Add(Utils.Connect(n27, n30));

            Nodes.Add(BaseNode.Name, BaseNode);
        }

        private void GuiThread()
        {
            _formGui = new PlanetForm();
            _formGui.SetOwner(this);
            _formGui.ShowDialog();
            Application.Run();
        }


        public override void Act(Message message)
        {
            Console.WriteLine($@"	[{Name} -> {message.Sender}]: {message.Content}");

            Utils.ParseMessage(message.Content, out string action, out string parameters);

            switch (action)
            {
                case "search":
                    HandleSearch(message.Sender, parameters);
                    break;

                case "carry":
                    HandleCarry(message.Sender, parameters);
                    break;
            }
            _formGui.UpdatePlanetGUI();
        }

        private void HandleCarry(string messageSender, string parameters)
        {
            // check if node is base
            var node = Nodes[parameters];
            if (node == BaseNode)
            {
                BaseNode.AddResource(1);
                Send(messageSender, "base");
                return;
            }

            // get next node to base 
            var nextNode = node.ToHome;
            node.Edges[nextNode.Name].Weight += 2;
            Send(messageSender, Utils.Str("move", nextNode.Name));
        }

        private void HandleSearch(string messageSender, string parameters)
        {
            // check if node has food
            // if node has food send food message to ant
            var node = Nodes[parameters];
            if (node != BaseNode && node.Resource > 0)
            {
                Send(messageSender, "food");
                node.RemoveResource(1);
                return;
            }

            // else get edge with highest weight ( first if multiple edges have the same weight)
            // send move message to ant with food if there is food
            string nodeName = GetHighestWeightNodeName(parameters);
            Send(messageSender, Utils.Str("move", nodeName));
        }

        private string GetHighestWeightNodeName(string nodeName)
        {
            var node = Nodes[nodeName];

            var maxEdgePair = node.Edges.First();
            double maxWeight = maxEdgePair.Value.Weight;
            foreach (var edgePair in node.Edges)
            {
                var edge = edgePair.Value;
                if (!(edge.Weight > maxWeight))
                    continue;

                maxWeight = edge.Weight;
                maxEdgePair = edgePair;
            }

            return maxWeight == 0
                ? node.Edges.ElementAt(Utils.RandNoGen.Next(node.Edges.Count)).Key
                : maxEdgePair.Key;
        }
    }
}