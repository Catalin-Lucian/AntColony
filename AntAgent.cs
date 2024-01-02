using ActressMas;
using System;
using System.Collections.Generic;

namespace AntColony
{
    public class AntAgent : Agent
    {
        private State _state;
        private Position _currentPosition;
        private string _currentTargetNode;

        public override void Setup()
        {
            Console.WriteLine($@"Starting {Name}");

            _currentTargetNode = "base";
            _currentPosition = Position.FromPoints(Utils.XPoints / 2, Utils.YPoints / 2);
            _state = State.Searching;

            SendState();
        }

        public override void Act(Message message)
        {
            //Thread.Sleep(1000);

            //Console.WriteLine($"[{Name} -> {message.Sender}]: {message.Content}");
            Utils.ParseMessage(message.Content, out string action, out List<string> parameters);

            switch (action)
            {
                case "move":
                    HandleMoveAction(parameters[0], new Position(parameters[1], parameters[2]));
                    break;

                case "food":
                    HandleFoodAction();
                    break;

                case "base":
                    HandleBaseAction();
                    break;
            }
        }

        private void HandleMoveAction(string nodeName, Position nodePosition)
        {
            // 1. calculate new position
            _currentPosition = _currentPosition.MoveTo(nodePosition, Utils.Speed);

            // 2. change ant curentNode
            _currentTargetNode = nodeName;

            // 3. send current state to planet
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
                    Send("planet", Utils.Str("carry", _currentTargetNode, _currentPosition.x, _currentPosition.y));
                    break;

                case State.Searching:
                    Send("planet", Utils.Str("search", _currentTargetNode, _currentPosition.x, _currentPosition.y));
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