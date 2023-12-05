﻿using ActressMas;
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
            
            _x = 0;
            _y = 0;
            _state = State.Searching;
            
            // Send("planet", Utils.Str("position", _x, _y));
        }

        private bool IsAtBase()
        {
            return _x == 0 && _y == 0; // the position of the base
        }

        public override void Act(Message message)
        {
            Console.WriteLine($@"[{Name} -> {message.Sender}]: {message.Content}");

            Utils.ParseMessage(message.Content, out string action, out List<string> parameters);

            switch (action)
            {
                case "move":
                    HandleMoveAction();
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

        public void HandleMoveAction()
        {
            // 1. schimbare coordonte
            // 2. in functie de starea interna (searching sau carry) se trimite mesaj la planeta cu SEARCH sau CARRY
        }

        public void HandleFoodAction()
        {
            // 1. schimbare stare interna in carry
            // 2. trimite la planeta CARRY
        }

        public void HandleBaseAction()
        {
            // 1. schimbare stare in searching
            // 2.  trimite la planeta SEARCH
        }

    }
}