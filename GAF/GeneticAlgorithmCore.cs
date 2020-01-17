using GeneticSharp.Domain;
using GeneticSharp.Domain.Chromosomes;
using GeneticSharp.Domain.Fitnesses;
using GeneticSharp.Domain.Populations;
using System;

namespace Genetic.Core
{
    public class GeneticAlgorithmCore
    {
        public GenetocAlgorithmConfig Config { get; private set; }

        public GeneticAlgorithmCore()
        {
            Config = new GenetocAlgorithmConfig();
        }

        private void Start()
        {
            IChromosome adam = new FloatingPointChromosome(Config.MinChromosomeValue, Config.MaxChromosomeValue, 128, 8);
            Population population = new Population(Config.PopulationMinSize, Config.PopulationMaxSize, adam);

            IFitness fitness = new
            GeneticAlgorithm ga = new GeneticAlgorithm(Population, )
        }
    }
}