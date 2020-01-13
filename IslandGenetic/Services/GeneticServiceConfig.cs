using IslandGenetic.Interfaces;

namespace IslandGenetic.Services
{
    public class GeneticServiceConfig
    {
        public IMutationOperator MutationOperator { get; set; }

        public ISelectionOperator SelectionOperator { get; set; }

        public IFitnessFunction FitnessFunction { get; set; }

        public double MaxChromosomeValue { get; set; }

        public double MinChromosomeValue { get; set; }

        public int PopulationSize { get; set; } = 10;

        public float Diversity { get; set; } = 0.3f;
    }
}