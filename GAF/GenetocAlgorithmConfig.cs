namespace Genetic.Core
{
    public class GenetocAlgorithmConfig
    {
        public double CrossoverProbability { get; set; } = 0.85;
        public double MutationProbability { get; set; } = 0.08;
        public int ElitismPercentage { get; set; } = 5;
        public int PopulationSize { get; set; } = 100;
        public int ChromosomeLength { get; set; } = 44;

        public int PopulationMaxSize { get; set; } = 200;
        public int PopulationMinSize { get; set; } = 100;

        public int MaxChromosomeValue { get; set; } = 500;
        public int MinChromosomeValue { get; set; } = -500;
    }
}