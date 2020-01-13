using IslandGenetic.Interfaces;
using System.Collections.Generic;

namespace IslandGenetic
{
    public class Chromosome : IChromosome
    {
        public List<byte> Value { get; set; }
    }
}