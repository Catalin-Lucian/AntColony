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
        private string _resourceCarried;
        private bool _leftTrail;

        private enum State { Free, Carrying };

        public override void Setup()
        {
            Console.WriteLine("Starting " + Name);

            _x = Utils.Size / 2;
            _y = Utils.Size / 2;
            _state = State.Free;

            while (IsAtBase())
            {
                _x = Utils.RandNoGen.Next(Utils.Size);
                _y = Utils.RandNoGen.Next(Utils.Size);
            }

            Send("planet", Utils.Str("position", _x, _y));
        }

        private bool IsAtBase()
        {
            return (_x == Utils.Size / 2 && _y == Utils.Size / 2); // the position of the base
        }

        public override void Act(Message message)
        {
            Console.WriteLine("\t[{1} -> {0}]: {2}", this.Name, message.Sender, message.Content);

            string action;
            List<string> parameters;
            Utils.ParseMessage(message.Content, out action, out parameters);

            if (action == "block")
            {
                // R1. If you detect an obstacle, then change direction
                MoveRandomly();
                Send("planet", Utils.Str("change", _x, _y));
            }
            else if (action == "move" && _state == State.Carrying && IsAtBase())
            {
                // R2. If carrying samples and at the base, then unload samples
                _state = State.Free;
                Send("planet", Utils.Str("unload", _resourceCarried));
            }
            else if (action == "move" && _state == State.Carrying && !_leftTrail)
            {
                // R6 Dacă se transportă resurse și poziția diferă de bază, se mărește cu 2 ponderea
                // traseului către bază
                _leftTrail = true;
                Send("planet", Utils.Str("trail", _x, _y));
            }
            else if (action == "move" && _state == State.Carrying && !IsAtBase())
            {
                // R3. If carrying samples and not at the base, then travel up gradient
                _leftTrail = false;
                MoveToBase();
                Send("planet", Utils.Str("carry", _x, _y));
            }
            else if (action == "rock")
            {
                // R4. If you detect a sample, then pick sample up
                _leftTrail = false;
                _state = State.Carrying;
                _resourceCarried = parameters[0];
                Send("planet", Utils.Str("pick-up", _resourceCarried));
            }
            // R7: Dacă se detectează un traseu cu pondere > 0, ponderea scade cu 1 și agentul
            // se îndepărtează de bază.
            else if (action == "trail")
            {
                _leftTrail = false;
                FollowTrail(int.Parse(parameters[0]), int.Parse(parameters[1]));
                Send("planet", Utils.Str("change", _x, _y));
            }
            else if (action == "move")
            {
                // R5. If (true), then move randomly
                MoveRandomly();
                Send("planet", Utils.Str("change", _x, _y));
            }
        }

        private void MoveRandomly()
        {
            int d = Utils.RandNoGen.Next(4);
            switch (d)
            {
                case 0: if (_x > 0) _x--; break;
                case 1: if (_x < Utils.Size - 1) _x++; break;
                case 2: if (_y > 0) _y--; break;
                case 3: if (_y < Utils.Size - 1) _y++; break;
            }
        }

        private void MoveToBase()
        {
            int dx = _x - Utils.Size / 2;
            int dy = _y - Utils.Size / 2;

            if (Math.Abs(dx) > Math.Abs(dy))
                _x -= Math.Sign(dx);
            else
                _y -= Math.Sign(dy);
        }

        private void FollowTrail(int tail_x, int trail_y)
        {
            // go in the direction of the trail, the oposide direction of base
            int dx = _x - Utils.Size / 2;
            int dy = _y - Utils.Size / 2;

            if (Math.Abs(dx) > Math.Abs(dy))
                _x += Math.Sign(dx);
            else
                _y += Math.Sign(dy);
        }
    }
}