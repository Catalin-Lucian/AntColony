using ActressMas;
using System.Diagnostics.CodeAnalysis;
using System.Threading;

namespace AntColony
{
    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    public class Program
    {
        private static void Main(string[] args)
        {
            var env = new EnvironmentMas(0, 100);

            var planetAgent = new PlanetAgent();
            env.Add(planetAgent, "planet");
            
            for (int i = 1; i <= Utils.NoExplorers; i++)
            {
                var explorerAgent = new ExplorerAgent();
                env.Add(explorerAgent, "explorer" + i);
            }

            Thread.Sleep(500);

            env.Start();
        }
    }
}