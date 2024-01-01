using ActressMas;
using System;

namespace AntColony
{
    public class AntAgent : Agent
    {
        private State _state;
        //public Position Position;
        public string _currentNode;

        public override void Setup()
        {
            Console.WriteLine($@"Starting {Name}");

            //Position = new Position(Utils.XPoints / 2, Utils.YPoints / 2);
            _currentNode = "base";
            _state = State.Searching;

            SendState();
        }

        public override void Act(Message message)
        {
            // sleep for 1 second
            //Thread.Sleep(1000);

            Console.WriteLine($"[{Name} -> {message.Sender}]: {message.Content}");

            Utils.ParseMessage(message.Content, out string action, out string parameter);

            switch (action)
            {
                case "move":
                    HandleMoveAction(parameter);
                    break;

                case "food":
                    HandleFoodAction();
                    break;

                case "base":
                    HandleBaseAction();
                    break;
            }
        }

        private void HandleMoveAction(string nodeName)
        {
            // 1. change ant curentNode
            _currentNode = nodeName;

            // 2. send current state to planet
            SendState();
        }

        private void HandleFoodAction()
        {
            // 1. change state to Carrying
            _state = State.Carrying;

            // 2. send state Carrying to planet
            SendState();
        }

        private void HandleBaseAction()
        {
            // 1. change state to Searching
            _state = State.Searching;

            // 2. send state Searching to planet
            SendState();
        }

        private void SendState()
        {
            switch (_state)
            {
                case State.Carrying:
                    Send("planet", Utils.Str("carry", _currentNode));
                    break;

                case State.Searching:
                    Send("planet", Utils.Str("search", _currentNode));
                    break;
            }
        }


        private enum State
        {
            Searching,
            Carrying
        }
    }
}