using ActressMas;
using Message = ActressMas.Message;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;

namespace AntColony
{
    public class PlanetAgent : Agent
    {
        private PlanetForm _formGui;
        public Dictionary<string, Node> Nodes { get; set; }
        public List<Edge> Edges { get; set; }
        
        public Node BaseNode { get; set; }
        

        public PlanetAgent()
        {
            CreateNodes();
            
            var t = new Thread(GuiThread);
            t.Start();

        }

        private void CreateNodes()
        {
            Nodes = new Dictionary<string, Node>();
            Edges = new List<Edge>();
            
            // create base node 
            BaseNode = new Node(Utils.SizeX/2, Utils.SizeY/2, 0);
            
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
            
            Nodes.Add(n15.Name, n15);
            Nodes.Add(n16.Name, n16);
            
            Edges.Add(Utils.Connect(BaseNode, n15));
            Edges.Add(Utils.Connect(n15, n16));
            Edges.Add(Utils.Connect(n16, n3));
                
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
            // Console.WriteLine("\t[{1} -> {0}]: {2}", this.Name, message.Sender, message.Content);
            //
            // string action; string parameters;
            // Utils.ParseMessage(message.Content, out action, out parameters);
            //
            // switch (action)
            // {
            //     case "position":
            //         HandlePosition(message.Sender, parameters);
            //         break;
            //
            //     case "change":
            //         HandleChange(message.Sender, parameters);
            //         break;
            //
            //     case "pick-up":
            //         HandlePickUp(message.Sender, parameters);
            //         break;
            //
            //     case "carry":
            //         HandleCarry(message.Sender, parameters);
            //         break;
            //
            //     case "unload":
            //         HandleUnload(message.Sender);
            //         break;
            //
            //     case "trail":
            //         HandleTrail(message.Sender, parameters);
            //         break;
            //
            //     default:
            //         break;
            // }
            // _formGui.UpdatePlanetGUI();
        }
    //
    //     private void HandlePosition(string sender, string position)
    //     {
    //         ExplorerPositions.Add(sender, position);
    //         Send(sender, "move");
    //     }
    //
    //     private void HandleChange(string sender, string position)
    //     {
    //         ExplorerPositions[sender] = position;
    //
    //         // check for out of bounds positions
    //         string[] values = position.Split();
    //         int x = int.Parse(values[0]);
    //         int y = int.Parse(values[1]);
    //
    //         if (x < 0 || x > Utils.Size || y < 0 || y > Utils.Size)
    //         {
    //             Send(sender, "block");
    //             return;
    //         }
    //
    //         foreach (string k in ExplorerPositions.Keys)
    //         {
    //             if (k == sender)
    //                 continue;
    //             if (ExplorerPositions[k] == position)
    //             {
    //                 Send(sender, "block");
    //                 return;
    //             }
    //         }
    //
    //         foreach (string k in ResourcePositions.Keys)
    //         {
    //             if (position != _basePosition && ResourcePositions[k] == position)
    //             {
    //                 Send(sender, "rock " + k);
    //                 return;
    //             }
    //         }
    //
    //         // check for trails and send them to the explorer
    //         foreach (string k in Trails.Keys)
    //         {
    //             if (position == k)
    //             {
    //                 Send(sender, "trail " + k);
    //                 Trails[k] = Trails[k] - 1;
    //                 if (Trails[k] < max_streanth[0])
    //                     Trails.Remove(k);
    //                 return;
    //             }
    //         }
    //
    //         Send(sender, "move");
    //     }
    //
    //     private void HandlePickUp(string sender, string position)
    //     {
    //         Loads[sender] = position;
    //         Send(sender, "move");
    //     }
    //
    //     private void HandleCarry(string sender, string position)
    //     {
    //         ExplorerPositions[sender] = position;
    //         string res = Loads[sender];
    //         ResourcePositions[res] = position;
    //         Send(sender, "move");
    //     }
    //
    //     private void HandleUnload(string sender)
    //     {
    //         Loads.Remove(sender);
    //         Send(sender, "move");
    //     }
    //
    //     private void HandleTrail(string sender, string position)
    //     {
    //         if(Trails.ContainsKey(position))
    //         {
    //             Trails[position] = Trails[position] + 2;
    //             if (Trails[position] > max_streanth[1])
    //                 Trails[position] = max_streanth[1]; 
    //         }
    //         else
    //         {
    //             Trails.Add(position, 2);
    //         }
    //         Send(sender, "move");
    //     }
    }
}