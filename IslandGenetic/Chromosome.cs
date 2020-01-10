using IslandGenetic.Interfaces;

namespace IslandGenetic
{
    internal class Chromosome : IChromosome<double>
    {
        public double Value { get; set; }

        internal Chromosome(double value) => (Value) = (value);
    }
}