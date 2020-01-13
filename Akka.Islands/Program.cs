using IslandGenetic;
using IslandGenetic.Services;
using System;

namespace Akka.Islands
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            GeneticServiceConfig config = new GeneticServiceConfig
            {
            };

            GeneticService geneticService = new GeneticService(config);

            geneticService.
        }
    }
}