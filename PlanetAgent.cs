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
        public Dictionary<string, Node> Nodes;
        public List<Edge> Edges;

        public Dictionary<string, Position> AntsPositions;
        public Node BaseNode;



        public PlanetAgent()
        {
            Nodes = new Dictionary<string, Node>();
            Edges = new List<Edge>();
            AntsPositions = new Dictionary<string, Position>();
            CreateNodes();

            var t = new Thread(GuiThread);
            t.Start();

            var d = new Thread(DecayEdges);
            d.Start();
        }


        private void CreateNodes()
        {
            // create all nodes 
            BaseNode = CreateNode("base", Utils.XPoints / 2, Utils.YPoints / 2, 0);
            var node1 = CreateNode("n1", 22, 11, 0, BaseNode);
            var node2 = CreateNode("n2", 18, 11, 0, BaseNode);
            var node3 = CreateNode("n3", 18, 8, 0, BaseNode);
            var node4 = CreateNode("n4", 21, 7, 0, BaseNode);
            var node5 = CreateNode("n5", 24, 7, 0, BaseNode);
            var node6 = CreateNode("n6", 26, 11, 0, node1);
            var node7 = CreateNode("n7", 23, 14, 0, node1);
            var node8 = CreateNode("n8", 19, 15, 0, node2);
            var node9 = CreateNode("n9", 14, 14, 5, node2);
            var node10 = CreateNode("n10", 13, 11, 0, node2);
            var node11 = CreateNode("n11", 14, 8, 0, node2);
            var node12 = CreateNode("n12", 15, 6, 0, node11);
            var node13 = CreateNode("n13", 19, 5, 0, node3);
            var node14 = CreateNode("n14", 23, 5, 0, node5);
            var node15 = CreateNode("n15", 27, 5, 5, node14);
            var node16 = CreateNode("n16", 28, 7, 5, node5);
            var node17 = CreateNode("n17", 29, 10, 0, node6);
            var node18 = CreateNode("n18", 26, 15, 5, node7);
            var node19 = CreateNode("n19", 20, 18, 5, node8);
            var node20 = CreateNode("n20", 17, 17, 0, node8);
            var node21 = CreateNode("n21", 15, 19, 0, node20);
            var node22 = CreateNode("n22", 14, 16, 0, node9);
            var node23 = CreateNode("n23", 12, 17, 0, node22);
            var node24 = CreateNode("n24", 9, 19, 5, node23);
            var node25 = CreateNode("n25", 8, 16, 5, node23);
            var node26 = CreateNode("n26", 6, 15, 10, node25);
            var node27 = CreateNode("n27", 7, 13, 0, node9);
            var node28 = CreateNode("n28", 9, 11, 5, node10);
            var node29 = CreateNode("n29", 9, 8, 0, node11);
            var node31 = CreateNode("n31", 16, 4, 5, node3);
            var node30 = CreateNode("n30", 11, 5, 0, node31);
            var node32 = CreateNode("n32", 19, 1, 0, node31);
            var node33 = CreateNode("n33", 21, 2, 0, node13);
            var node34 = CreateNode("n34", 24, 1, 0, node33);
            var node35 = CreateNode("n35", 25, 3, 0, node14);
            var node36 = CreateNode("n36", 28, 1, 0, node34);
            var node37 = CreateNode("n37", 29, 3, 10, node35);
            var node38 = CreateNode("n38", 33, 1, 0, node37);
            var node39 = CreateNode("n39", 32, 6, 15, node16);
            var node40 = CreateNode("n40", 31, 8, 10, node17);
            var node41 = CreateNode("n41", 31, 12, 0, node17);
            var node42 = CreateNode("n42", 30, 14, 5, node6);
            var node43 = CreateNode("n43", 29, 17, 20, node42);
            var node44 = CreateNode("n44", 27, 19, 5, node18);
            var node45 = CreateNode("n45", 23, 19, 0, node7);
            var node46 = CreateNode("n46", 35, 3, 20, node39);
            var node47 = CreateNode("n47", 33, 9, 0, node40);
            var node48 = CreateNode("n48", 34, 11, 0, node41);
            var node49 = CreateNode("n49", 33, 15, 0, node42);
            var node50 = CreateNode("n50", 30, 19, 5, node43);
            var node51 = CreateNode("n51", 38, 1, 25, node46);
            var node52 = CreateNode("n52", 38, 5, 0, node46);
            var node53 = CreateNode("n53", 37, 7, 0, node39);
            var node54 = CreateNode("n54", 38, 10, 0, node47);
            var node55 = CreateNode("n55", 37, 14, 0, node48);
            var node56 = CreateNode("n56", 35, 16, 0, node49);
            var node57 = CreateNode("n57", 34, 18, 0, node56);
            var node58 = CreateNode("n58", 38, 17, 0, node56);
            var node59 = CreateNode("n59", 6, 19, 15, node24);
            var node60 = CreateNode("n60", 3, 18, 20, node25);
            var node61 = CreateNode("n61", 4, 14, 15, node26);
            var node62 = CreateNode("n62", 6, 9, 0, node28);
            var node63 = CreateNode("n63", 4, 11, 0, node62);
            var node64 = CreateNode("n64", 4, 7, 5, node62);
            var node65 = CreateNode("n65", 7, 5, 5, node29);
            var node66 = CreateNode("n66", 11, 2, 5, node30);
            var node67 = CreateNode("n67", 15, 1, 0, node31);
            var node68 = CreateNode("n68", 1, 11, 0, node61);
            var node69 = CreateNode("n69", 1, 8, 0, node63);
            var node70 = CreateNode("n70", 2, 5, 15, node64);
            var node71 = CreateNode("n71", 5, 3, 15, node65);
            var node72 = CreateNode("n72", 6, 1, 10, node66);
            var node73 = CreateNode("n73", 3, 2, 20, node71);

            // create all edges
            CreateEdge(BaseNode, node1);
            CreateEdge(BaseNode, node2);
            CreateEdge(BaseNode, node3);
            CreateEdge(BaseNode, node4);
            CreateEdge(BaseNode, node5);
            CreateEdge(node1, node5);
            CreateEdge(node1, node6);
            CreateEdge(node1, node7);
            CreateEdge(node2, node8);
            CreateEdge(node2, node9);
            CreateEdge(node2, node10);
            CreateEdge(node2, node11);
            CreateEdge(node3, node13);
            CreateEdge(node3, node31);
            CreateEdge(node4, node5);
            CreateEdge(node4, node13);
            CreateEdge(node5, node14);
            CreateEdge(node5, node16);
            CreateEdge(node5, node6);
            CreateEdge(node6, node17);
            CreateEdge(node6, node42);
            CreateEdge(node6, node18);
            CreateEdge(node7, node18);
            CreateEdge(node7, node45);
            CreateEdge(node7, node19);
            CreateEdge(node8, node19);
            CreateEdge(node8, node20);
            CreateEdge(node9, node20);
            CreateEdge(node9, node22);
            CreateEdge(node9, node27);
            CreateEdge(node9, node10);
            CreateEdge(node10, node28);
            CreateEdge(node10, node11);
            CreateEdge(node11, node29);
            CreateEdge(node11, node12);
            CreateEdge(node12, node31);
            CreateEdge(node13, node33);
            CreateEdge(node13, node14);
            CreateEdge(node14, node33);
            CreateEdge(node14, node35);
            CreateEdge(node14, node15);
            CreateEdge(node15, node37);
            CreateEdge(node15, node16);
            CreateEdge(node16, node39);
            CreateEdge(node16, node17);
            CreateEdge(node17, node40);
            CreateEdge(node17, node41);
            CreateEdge(node18, node44);
            CreateEdge(node20, node21);
            CreateEdge(node21, node22);
            CreateEdge(node22, node23);
            CreateEdge(node23, node24);
            CreateEdge(node23, node25);
            CreateEdge(node24, node25);
            CreateEdge(node24, node59);
            CreateEdge(node25, node26);
            CreateEdge(node25, node60);
            CreateEdge(node26, node61);
            CreateEdge(node26, node27);
            CreateEdge(node27, node28);
            CreateEdge(node28, node62);
            CreateEdge(node28, node29);
            CreateEdge(node29, node30);
            CreateEdge(node29, node65);
            CreateEdge(node30, node31);
            CreateEdge(node30, node66);
            CreateEdge(node31, node32);
            CreateEdge(node31, node67);
            CreateEdge(node32, node33);
            CreateEdge(node33, node34);
            CreateEdge(node34, node35);
            CreateEdge(node34, node36);
            CreateEdge(node35, node37);
            CreateEdge(node36, node38);
            CreateEdge(node37, node38);
            CreateEdge(node37, node39);
            CreateEdge(node38, node39);
            CreateEdge(node38, node46);
            CreateEdge(node39, node40);
            CreateEdge(node39, node46);
            CreateEdge(node39, node53);
            CreateEdge(node40, node47);
            CreateEdge(node41, node48);
            CreateEdge(node42, node43);
            CreateEdge(node42, node49);
            CreateEdge(node43, node44);
            CreateEdge(node43, node50);
            CreateEdge(node44, node45);
            CreateEdge(node46, node51);
            CreateEdge(node46, node52);
            CreateEdge(node47, node53);
            CreateEdge(node47, node54);
            CreateEdge(node47, node48);
            CreateEdge(node48, node55);
            CreateEdge(node49, node56);
            CreateEdge(node50, node57);
            CreateEdge(node51, node52);
            CreateEdge(node52, node53);
            CreateEdge(node54, node55);
            CreateEdge(node55, node58);
            CreateEdge(node56, node58);
            CreateEdge(node56, node57);
            CreateEdge(node59, node60);
            CreateEdge(node60, node61);
            CreateEdge(node61, node63);
            CreateEdge(node61, node68);
            CreateEdge(node62, node63);
            CreateEdge(node62, node64);
            CreateEdge(node63, node69);
            CreateEdge(node64, node70);
            CreateEdge(node64, node65);
            CreateEdge(node65, node71);
            CreateEdge(node66, node67);
            CreateEdge(node68, node69);
            CreateEdge(node69, node70);
            CreateEdge(node70, node73);
            CreateEdge(node71, node72);
            CreateEdge(node71, node73);
            CreateEdge(node72, node66);
        }

        Node CreateNode(string name, int row, int column, int res)
        {
            var node = new Node(name, row, column, res);
            Nodes.Add(node.Name, node);
            return node;
        }

        Node CreateNode(string name, int row, int column, int res, Node toBase)
        {
            var node = new Node(name, row, column, res);
            Nodes.Add(node.Name, node);
            node.ToHome = toBase;
            return node;
        }

        Edge CreateEdge(Node node1, Node node2)
        {
            var edge = new Edge(node1, node2, 0);
            Edges.Add(edge);
            node1.Edges.Add(node2.Name, edge);
            node2.Edges.Add(node1.Name, edge);
            return edge;
        }


        private void GuiThread()
        {
            _formGui = new PlanetForm();
            _formGui.SetOwner(this);
            _formGui.ShowDialog();
            Application.Run();
        }

        private void DecayEdges()
        {
            while (true)
            {
                Thread.Sleep(1000);
                foreach (var edge in Edges)
                    if (edge.Weight > 0)
                        edge.Weight -= 0.1;
            }

        }

        public override void Act(Message message)
        {
            Console.WriteLine($@"  [{Name} -> {message.Sender}]: {message.Content}");

            Utils.ParseMessage(message.Content, out string action, out string nodeName);

            switch (action)
            {
                case "search":
                    HandleSearch(message.Sender, nodeName);
                    break;

                case "carry":
                    HandleCarry(message.Sender, nodeName);
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
                BaseNode.Resource++;
                Send(messageSender, "base");
                return;
            }

            // get next node to base 
            var nextNode = node.ToHome;
            Send(messageSender, Utils.Str("move", nextNode.Name));
            // register ant position
            AntsPositions[messageSender] = Nodes[nextNode.Name].Pos;
            // increase weight of edge
            node.Edges[nextNode.Name].Weight += 2;
        }

        private void HandleSearch(string messageSender, string nodeName)
        {
            // check if node has food
            // if node has food send food message to ant
            var node = Nodes[nodeName];
            if (node != BaseNode && node.Resource > 0)
            {
                Send(messageSender, "food");
                node.Resource--;
                return;
            }

            // else get edge with highest weight ( first if multiple edges have the same weight)
            // send move message to ant with food if there is food
            string newNodeName = GetHighestWeightNodeName(nodeName);
            Send(messageSender, Utils.Str("move", newNodeName));
            // register ant position
            AntsPositions[messageSender] = Nodes[newNodeName].Pos;
            // decrease weight of edge
            node.Edges[newNodeName].Weight--;
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