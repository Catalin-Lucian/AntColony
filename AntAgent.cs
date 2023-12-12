using ActressMas;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace AntColony
{
    public class AntAgent : Agent
    {
        private int _x, _y;
        private State _state;
        

        private enum State { Searching, Carrying };

        public override void Setup()
        {
            Console.WriteLine($@"Starting {Name}");
            
            this._x = 0;
            this._y = 0;
            this._state = State.Searching;

            SendState();
        }

        public override void Act(Message message)
        {
            Console.WriteLine($@"[{Name} -> {message.Sender}]: {message.Content}");

            Utils.ParseMessage(message.Content, out string action, out List<string> parameters);

            switch (action)
            {
                case "move":
                    HandleMoveAction(int.Parse(parameters[0]), int.Parse(parameters[1]));
                    break;
                case "food":
                    HandleFoodAction();
                    break;
                case "base":
                    HandleBaseAction();
                    break;
                default:
                    break;
            }
        }

        private bool IsAtBase()
        {
            return this._x == 0 && this._y == 0; // the position of the base
        }

        private void HandleMoveAction(int x, int y)
        {
            // 1. change ant coordonates
            this._x = x;
            this._y = y;

            // 2. send current state to planet
            SendState();
        }

        private void HandleFoodAction()
        {
            // 1. change state to Carrying
            this._state = State.Carrying;

            // 2. send state Carrying to planet
            SendState();
        }

        private void HandleBaseAction()
        {
            // 1. change state to Searching
            this._state = State.Searching;

            // 2. send state Searching to planet
            SendState();
        }

        private void SendState()
        {
            switch (this._state)
            {
                case State.Carrying:
                    Send("planet", Utils.Str("carry", this._x, this._y));
                    break;

                case State.Searching:
                    Send("planet", Utils.Str("search", this._x, this._y));
                    break;

                default:
                    break;
            }
        }
    }
}