using System;

namespace GeneticCore
{
    public class GeneticAlgoritmConfig
    {
        public int MinPopulationSize { get; set; } = 10;
        public int MaxPopulationSize { get; set; } = 100;

        public int MinChromosomeValue { get; set; } = -50000;
        public int MaxChromosomeValue { get; set; } = 50000;

        public int ChromosomeSize { get; set; } = sizeof(double) * 8;

        public int ChromosomeFractionDigits { get; set; } = 0;

        public int StagnationGenerationCount { get; set; } = 1000000;

        public float MigrationProbability { get; set; } = 0.02f;

        public float CrossoverProbability { get; set; } = 0.01f;
        public float MutationProbability { get; set; } = 0.01f;

        public int MigrationSize { get; set; } = 2;

        public bool IsMaximization { get; set; } = false;

        public bool Predicate(double val1, double val2) 
        {
            if(IsMaximization) return val1 > val2;
            return val1 < val2;
        }
    }
}