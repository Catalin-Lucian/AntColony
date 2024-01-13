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
        public Dictionary<string, Node> nodes;
        public List<Edge> edges;

        public Dictionary<string, Position> antsPositions;
        public Node baseNode;

        public Dictionary<string, int> antNodesUntilDrop;
        public List<String> antJustLeftFood;

        public PlanetAgent()
        {
            nodes = new Dictionary<string, Node>();
            edges = new List<Edge>();
            antNodesUntilDrop = new Dictionary<string, int>();
            antJustLeftFood = new List<String>();
            antsPositions = new Dictionary<string, Position>();
            CreateNodes();

            var t = new Thread(GuiThread);
            t.Start();

            var d = new Thread(DecayEdges);
            d.Start();
        }


        private void CreateNodes()
        {
            // create all nodes 
            baseNode = CreateNode("base", Utils.XPoints / 2, Utils.YPoints / 2, 0);
            var node1 = CreateNode("n1", 22, 11, 0, baseNode, 1);
            var node2 = CreateNode("n2", 18, 11, 0, baseNode, 1);
            var node3 = CreateNode("n3", 18, 8, 0, baseNode, 1);
            var node4 = CreateNode("n4", 21, 7, 0, baseNode, 1);
            var node5 = CreateNode("n5", 24, 7, 0, node1, 2);
            var node6 = CreateNode("n6", 26, 11, 0, node1, 2);
            var node7 = CreateNode("n7", 23, 14, 0, node1, 2);
            var node8 = CreateNode("n8", 19, 15, 0, node2, 2);
            var node9 = CreateNode("n9", 14, 14, 5, node2, 2);
            var node10 = CreateNode("n10", 13, 11, 0, node2, 2);
            var node11 = CreateNode("n11", 14, 8, 0, node2, 2);
            var node12 = CreateNode("n12", 15, 6, 0, node11, 3);
            var node13 = CreateNode("n13", 19, 5, 0, node3, 2);
            var node14 = CreateNode("n14", 23, 5, 0, node5, 3);
            var node15 = CreateNode("n15", 27, 5, 5, node14, 4);
            var node16 = CreateNode("n16", 28, 7, 5, node5, 3);
            var node17 = CreateNode("n17", 29, 10, 0, node6, 3);
            var node18 = CreateNode("n18", 26, 15, 5, node7, 3);
            var node19 = CreateNode("n19", 20, 18, 5, node8, 3);
            var node20 = CreateNode("n20", 17, 17, 0, node8, 3);
            var node21 = CreateNode("n21", 15, 19, 0, node20, 4);
            var node22 = CreateNode("n22", 14, 16, 0, node9, 3);
            var node23 = CreateNode("n23", 12, 17, 0, node22, 4);
            var node24 = CreateNode("n24", 9, 19, 5, node23, 5);
            var node25 = CreateNode("n25", 8, 16, 5, node23, 5);
            var node26 = CreateNode("n26", 6, 15, 10, node25, 6);
            var node27 = CreateNode("n27", 7, 13, 0, node9, 3);
            var node28 = CreateNode("n28", 9, 11, 5, node10, 3);
            var node29 = CreateNode("n29", 9, 8, 0, node11, 3);
            var node31 = CreateNode("n31", 16, 4, 5, node3, 2);
            var node30 = CreateNode("n30", 11, 5, 0, node31, 3);
            var node32 = CreateNode("n32", 19, 1, 0, node31, 3);
            var node33 = CreateNode("n33", 21, 2, 0, node13, 3);
            var node34 = CreateNode("n34", 24, 1, 0, node33, 4);
            var node35 = CreateNode("n35", 25, 3, 0, node14, 4);
            var node36 = CreateNode("n36", 28, 1, 0, node34, 5);
            var node37 = CreateNode("n37", 29, 3, 10, node35, 5);
            var node38 = CreateNode("n38", 33, 1, 0, node37, 6);
            var node39 = CreateNode("n39", 32, 6, 15, node16, 4);
            var node40 = CreateNode("n40", 31, 8, 10, node17, 4);
            var node41 = CreateNode("n41", 31, 12, 0, node17, 4);
            var node42 = CreateNode("n42", 30, 14, 5, node6, 3);
            var node43 = CreateNode("n43", 29, 17, 20, node42, 4);
            var node44 = CreateNode("n44", 27, 19, 5, node18, 4);
            var node45 = CreateNode("n45", 23, 19, 0, node7, 3);
            var node46 = CreateNode("n46", 35, 3, 20, node39, 5);
            var node47 = CreateNode("n47", 33, 9, 0, node40, 5);
            var node48 = CreateNode("n48", 34, 11, 0, node41, 5);
            var node49 = CreateNode("n49", 33, 15, 0, node42, 4);
            var node50 = CreateNode("n50", 30, 19, 5, node43, 5);
            var node51 = CreateNode("n51", 38, 1, 25, node46, 6);
            var node52 = CreateNode("n52", 38, 5, 0, node46, 6);
            var node53 = CreateNode("n53", 37, 7, 0, node39, 5);
            var node54 = CreateNode("n54", 38, 10, 0, node47, 6);
            var node55 = CreateNode("n55", 37, 14, 0, node48, 6);
            var node56 = CreateNode("n56", 35, 16, 0, node49, 5);
            var node57 = CreateNode("n57", 34, 18, 0, node56, 6);
            var node58 = CreateNode("n58", 38, 17, 0, node56, 6);
            var node59 = CreateNode("n59", 6, 19, 15, node24, 6);
            var node60 = CreateNode("n60", 3, 18, 20, node25, 6);
            var node61 = CreateNode("n61", 4, 14, 15, node26, 7);
            var node62 = CreateNode("n62", 6, 9, 0, node28, 4);
            var node63 = CreateNode("n63", 4, 11, 0, node62, 5);
            var node64 = CreateNode("n64", 4, 7, 5, node62, 5);
            var node65 = CreateNode("n65", 7, 5, 5, node29, 4);
            var node66 = CreateNode("n66", 11, 2, 5, node30, 4);
            var node67 = CreateNode("n67", 15, 1, 0, node31, 3);
            var node68 = CreateNode("n68", 1, 11, 0, node61, 8);
            var node69 = CreateNode("n69", 1, 8, 0, node63, 6);
            var node70 = CreateNode("n70", 2, 5, 15, node64, 6);
            var node71 = CreateNode("n71", 5, 3, 15, node65, 5);
            var node72 = CreateNode("n72", 6, 1, 10, node66, 5);
            var node73 = CreateNode("n73", 3, 2, 20, node71, 6);

            // create all edges
            CreateEdge(baseNode, node1);
            CreateEdge(baseNode, node2);
            CreateEdge(baseNode, node3);
            CreateEdge(baseNode, node4);
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
            nodes.Add(node.name, node);
            return node;
        }

        Node CreateNode(string name, int row, int column, int res, Node toBase, int nodesToHome)
        {
            var node = new Node(name, row, column, res, nodesToHome);
            nodes.Add(node.name, node);
            node.toHome = toBase;
            return node;
        }

        Edge CreateEdge(Node node1, Node node2)
        {
            var edge = new Edge(node1, node2, 0);
            edges.Add(edge);
            node1.edges.Add(node2.name, edge);
            node2.edges.Add(node1.name, edge);
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
                foreach (var edge in edges)
                    if (edge.Weight > 0)
                        edge.DecreseWeight(0.1);
            }

        }

        public override void Act(Message message)
        {
            // Console.WriteLine($@"  [{Name} -> {message.Sender}]: {message.Content}");
            Utils.ParseMessage(message.Content, out string action, out List<string> parameters);

            switch (action)
            {
                case "search":
                    HandleSearch(message.Sender, parameters[0], new Position(parameters[1], parameters[2]));
                    break;

                case "carry":
                    HandleCarry(message.Sender, parameters[0], new Position(parameters[1], parameters[2]));
                    break;
            }
            _formGui.UpdatePlanetGUI();
        }

        private void HandleCarry(string messageSender, string targetNodeName, Position currentPosition)
        {
            // register ant position
            antsPositions[messageSender] = currentPosition;

            var targetNode = nodes[targetNodeName];

            // if ant is not on the target node send move with target node position
            if (currentPosition.IsNotEqual(targetNode.position))
            {
                Send(messageSender, Utils.Str("move", targetNodeName, targetNode.position.x, targetNode.position.y));
                return;
            }

            // check if ant is on base node or on targent node (half way) and send base message 
            if ((Utils.IsOptimizationActive && CheckTargetNode(messageSender, targetNode)) || targetNode == baseNode)
            {
                targetNode.resource++;
                Send(messageSender, "base");
                return;
            }

            // if not on base node get next targetNode to base and send it
            var nextNode = targetNode.toHome;
            Send(messageSender, Utils.Str("move", nextNode.name, nextNode.position.x, nextNode.position.y));
            // increase weight of edge
            targetNode.edges[nextNode.name].IncreseWeight(2);
        }

        private void HandleSearch(string messageSender, string targetNodeName, Position currentPosition)
        {
            // register ant position
            antsPositions[messageSender] = currentPosition;

            Node targetNode = nodes[targetNodeName];
            // if ant is not on the target targetNode send move with target targetNode position
            if (currentPosition.IsNotEqual(targetNode.position))
            {
                Send(messageSender, Utils.Str("move", targetNodeName, targetNode.position.x, targetNode.position.y));
                return;
            }

            // check if targetNode has food (skin if food was just left)
            if (!(Utils.IsOptimizationActive && CheckIfAntJustLeftFood(messageSender)) && targetNode != baseNode && targetNode.resource > 0)
            {    
                // if targetNode has food send food message to ant
                // targetNode represent currentNode if ant achieve it
                //RegisterUntilDrop(messageSender, targetNode);

                Send(messageSender, "food");
                targetNode.resource--;
                return;
            }

            // get best next targetNode to food
            string nextNodeName = GetBestNextNodeName(targetNodeName);
            Node nextNode = nodes[nextNodeName];
            // send move message to ant with next targetNode name and position
            Send(messageSender, Utils.Str("move", nextNodeName, nextNode.position.x, nextNode.position.y));
            // decrease weight of edge
            targetNode.edges[nextNodeName].DecreseWeight(1);
        }

        private void RegisterUntilDrop(string antName, Node currentNode)
        {
            antNodesUntilDrop[antName] = (currentNode.nodesToHome + 1) / 2;
        }

        private bool CheckTargetNode(string antName, Node currentNode)
        {
            if (!antNodesUntilDrop.ContainsKey(antName))
            {
                RegisterUntilDrop(antName, currentNode);
                return false;
            }

            antNodesUntilDrop[antName]--;

            if(antNodesUntilDrop[antName] == 0)
            {
                antNodesUntilDrop.Remove(antName);
                antJustLeftFood.Add(antName);
                return true;
            }

            return false;
        }

        private bool CheckIfAntJustLeftFood(string antName)
        {
            if (antJustLeftFood.Contains(antName))
            {
                antJustLeftFood.Remove(antName);
                return true;
            }
            return false;
        }

        private string GetBestNextNodeName(string nodeName)
        {
            var node = nodes[nodeName];
            var edges = new Dictionary<string, Edge>(node.edges);

            if (edges.Count == 1)
            {
                // if edge has only one edge home return it
                return edges.First().Key;
            }
            //else
            //{
            //    // else if edge has more than one edge home remove edge to home only if it has weight != 0
            //    // this is done to prevent ants from going to base when they have no food
            //    // but allow them to go to base when all other edges have weight 0
            //    if (node.toHome != null && edges[node.toHome.name].Weight != 0)
            //        edges.Remove(node.toHome.name);
            //}

            // get maxWeight edges
            var maxEdgePairs = new List<KeyValuePair<string, Edge>> { edges.First() };
            double maxWeight = maxEdgePairs[0].Value.Weight;
            for (int i = 1; i < edges.Count; i++)
            {
                var pair = edges.ElementAt(i);
                if (pair.Value.Weight > maxWeight)
                {
                    // if new maxWeight clear list and add new pair
                    maxEdgePairs.Clear();
                    maxEdgePairs.Add(pair);
                    maxWeight = pair.Value.Weight;
                }
                else if (pair.Value.Weight == maxWeight)
                {
                    // if new pair has same weight as maxWeight add it to list
                    maxEdgePairs.Add(pair);
                }
            }

            if (maxWeight == 0)
                // if all edges have weight 0 return random edge
                return edges.ElementAt(Utils.RandNoGen.Next(edges.Count)).Key;
            else
                // return random edge from maxWeight edges
                return maxEdgePairs.ElementAt(Utils.RandNoGen.Next(maxEdgePairs.Count)).Key;
        }
    }
}