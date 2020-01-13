using IslandGenetic.Interfaces;
using IslandGenetic.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IslandGenetic
{
    public sealed class GeneticService : IGeneticService
    {
        private readonly Random Random;

        private readonly GeneticServiceConfig Config;

        private readonly IFitnessFunction FitnessFunction;

        #region Operators

        private readonly IMutationOperator MutationOperator;

        private readonly ISelectionOperator SelectionOperator;

        #endregion Operators

        public List<Population> Populations { get; private set; }

        public GeneticService(GeneticServiceConfig config)
        {
            Config = config;
            FitnessFunction = config.FitnessFunction;
            MutationOperator = config.MutationOperator;
            SelectionOperator = config.SelectionOperator;
        }

        #region Algorithm

        private Population InitPopulation(int populationSize, int genomeSize, double minValueLimit, double maxValueLimit)
        {
            List<Individual> individuals = new List<Individual>();

            for (var i = 0; i < populationSize; i++)
            {
                Individual individual = new Individual
                {
                    Genome = new List<Chromosome>()
                };
                individuals.Add(individual);

                for (var j = 0; j < genomeSize; j++)
                {
                    double value = Random.NextDouble() * (maxValueLimit - minValueLimit) + minValueLimit;
                    Chromosome chromosome = new Chromosome
                    {
                        Value = BitConverter.GetBytes(value).ToList()
                    };
                    individual.Genome.Add(chromosome);
                }
            }

            Population population = new Population
            {
                Individuals = individuals
            };
            return population;
        }

        public void Start()
        {
            Population startPopulation = InitPopulation(Config.PopulationSize, Config.FitnessFunction.Size, Config.MinChromosomeValue, Config.MaxChromosomeValue);

            Populations = new List<Population>()
            {
                startPopulation
            };
        }
    }

    #endregion Algorithm
}