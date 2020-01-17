namespace GeneticCore
{
    public class GeneticAlgoritmConfig
    {
        public int MinPopulationSize { get; set; } = 100;
        public int MaxPopulationSize { get; set; } = 200;

        public int MinChromosomeValue { get; set; } = -100;
        public int MaxChromosomeValue { get; set; } = 100;

        public int ChromosomeSize { get; set; } = sizeof(double) * 8;

        public int ChromosomeFractionDigits { get; set; } = 5;

        public int StagnationGenerationCount { get; set; } = 20;

        public float MigrationProbability { get; set; } = 0.3f;

        public int MigrationSize { get; set; }
    }
}