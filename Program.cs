﻿using ActressMas;
using System.Diagnostics.CodeAnalysis;
using System.Threading;

namespace AntColony
{
    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    public class Program
    {
        private static void Main()
        {
            var env = new EnvironmentMas(0, 0);

            var planetAgent = new PlanetAgent();
            env.Add(planetAgent, "planet");

            for (int i = 1; i <= Utils.NoAnts; i++)
            {
                var antAgent = new AntAgent();
                env.Add(antAgent, "ant" + i);
            }

            Thread.Sleep(500);

            env.Start();
        }
    }
}