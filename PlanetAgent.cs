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
        public Dictionary<string, Edge> Edges { get; set; }
        
        public Node BaseNode { get; set; }
        

        public PlanetAgent()
        {
            var t = new Thread(new ThreadStart(GUIThread));
            t.Start();

            CreateNodes();
        }

        private void CreateNodes()
        {
            // create base node 
            BaseNode = new Node(Utils.SizeX / 2, Utils.SizeY / 2, 0);
        }

        private void GUIThread()
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