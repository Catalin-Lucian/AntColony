using ActressMas;
using System;

namespace AntColony
{
    public class AntAgent : Agent
    {
        private State _state;
        private int _x;
        private int _y;

        public override void Setup()
        {
            Console.WriteLine($@"Starting {Name}");

            _x = 300;
            _y = 200;
            _state = State.Searching;

            SendState();
        }

        public override void Act(Message message)
        {
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

        private bool IsAtBase()
        {
            return _x == 0 && _y == 0; // the position of the base
        }

        private void HandleMoveAction(string parameter)
        {
            // 1. change ant coordonates
            string[] values = parameter.Split('_');
            int x = int.Parse(values[0]);
            int y = int.Parse(values[1]);

            _x = x;
            _y = y;

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
                    Send("planet", Utils.Str("carry", _x, _y));
                    break;

                case State.Searching:
                    Send("planet", Utils.Str("search", _x, _y));
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