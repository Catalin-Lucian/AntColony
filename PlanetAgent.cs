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
        public Dictionary<string, string> ExplorerPositions { get; set; }
        public Dictionary<string, string> ResourcePositions { get; set; }
        public Dictionary<string, string> Loads { get; set; }

        //will have <pos , streanth> 
        public int[] max_streanth = new int[2];

        public Dictionary<string, int> Trails { get; set; }

        private string _basePosition;

        public PlanetAgent()
        {
            ExplorerPositions = new Dictionary<string, string>();
            ResourcePositions = new Dictionary<string, string>();
            Loads = new Dictionary<string, string>();
            Trails = new Dictionary<string, int>();
            _basePosition = Utils.Str(Utils.Size / 2, Utils.Size / 2);

            Thread t = new Thread(new ThreadStart(GUIThread));
            t.Start();

            max_streanth[0] = 0;
            max_streanth[1] = 5;
        }

        private void GUIThread()
        {
            _formGui = new PlanetForm();
            _formGui.SetOwner(this);
            _formGui.ShowDialog();
            Application.Run();
        }

        public override void Setup()
        {
            Console.WriteLine("Starting " + Name);

            List<string> resPos = new List<string>();
            string compPos = Utils.Str(Utils.Size / 2, Utils.Size / 2);
            resPos.Add(compPos); // the position of the base

            int gridSize = (int)Math.Sqrt(Utils.NoSpots);
            int regionSize = Utils.Size / gridSize;

            for (int i = 1; i <= Utils.NoResources; i++)
            {
                // Select a region for this resource
                int regionX = Utils.RandNoGen.Next(gridSize);
                int regionY = Utils.RandNoGen.Next(gridSize);

                // Generate position within the selected region
                int posX = regionX * regionSize + Utils.RandNoGen.Next(regionSize);
                int posY = regionY * regionSize + Utils.RandNoGen.Next(regionSize);

                compPos = Utils.Str(posX, posY);

                while (resPos.Contains(compPos)) // resources do not overlap
                {
                    // If position is already taken, generate a new position within the same region
                    posX = regionX * regionSize + Utils.RandNoGen.Next(regionSize);
                    posY = regionY * regionSize + Utils.RandNoGen.Next(regionSize);

                    compPos = Utils.Str(posX, posY);
                }

                ResourcePositions.Add("res" + i, compPos);
                resPos.Add(compPos);
            }
        }

        public override void Act(Message message)
        {
            Console.WriteLine("\t[{1} -> {0}]: {2}", this.Name, message.Sender, message.Content);

            string action; string parameters;
            Utils.ParseMessage(message.Content, out action, out parameters);

            switch (action)
            {
                case "position":
                    HandlePosition(message.Sender, parameters);
                    break;

                case "change":
                    HandleChange(message.Sender, parameters);
                    break;

                case "pick-up":
                    HandlePickUp(message.Sender, parameters);
                    break;

                case "carry":
                    HandleCarry(message.Sender, parameters);
                    break;

                case "unload":
                    HandleUnload(message.Sender);
                    break;

                case "trail":
                    HandleTrail(message.Sender, parameters);
                    break;

                default:
                    break;
            }
            _formGui.UpdatePlanetGUI();
        }

        private void HandlePosition(string sender, string position)
        {
            ExplorerPositions.Add(sender, position);
            Send(sender, "move");
        }

        private void HandleChange(string sender, string position)
        {
            ExplorerPositions[sender] = position;

            // check for out of bounds positions
            string[] values = position.Split();
            int x = int.Parse(values[0]);
            int y = int.Parse(values[1]);

            if (x < 0 || x > Utils.Size || y < 0 || y > Utils.Size)
            {
                Send(sender, "block");
                return;
            }

            foreach (string k in ExplorerPositions.Keys)
            {
                if (k == sender)
                    continue;
                if (ExplorerPositions[k] == position)
                {
                    Send(sender, "block");
                    return;
                }
            }

            foreach (string k in ResourcePositions.Keys)
            {
                if (position != _basePosition && ResourcePositions[k] == position)
                {
                    Send(sender, "rock " + k);
                    return;
                }
            }

            // check for trails and send them to the explorer
            foreach (string k in Trails.Keys)
            {
                if (position == k)
                {
                    Send(sender, "trail " + k);
                    Trails[k] = Trails[k] - 1;
                    if (Trails[k] < max_streanth[0])
                        Trails.Remove(k);
                    return;
                }
            }

            Send(sender, "move");
        }

        private void HandlePickUp(string sender, string position)
        {
            Loads[sender] = position;
            Send(sender, "move");
        }

        private void HandleCarry(string sender, string position)
        {
            ExplorerPositions[sender] = position;
            string res = Loads[sender];
            ResourcePositions[res] = position;
            Send(sender, "move");
        }

        private void HandleUnload(string sender)
        {
            Loads.Remove(sender);
            Send(sender, "move");
        }

        private void HandleTrail(string sender, string position)
        {
            if(Trails.ContainsKey(position))
            {
                Trails[position] = Trails[position] + 2;
                if (Trails[position] > max_streanth[1])
                    Trails[position] = max_streanth[1]; 
            }
            else
            {
                Trails.Add(position, 2);
            }
            Send(sender, "move");
        }
    }
}