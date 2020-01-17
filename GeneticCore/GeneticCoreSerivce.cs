using GeneticSharp.Domain;
using GeneticSharp.Domain.Chromosomes;
using GeneticSharp.Domain.Crossovers;
using GeneticSharp.Domain.Fitnesses;
using GeneticSharp.Domain.Mutations;
using GeneticSharp.Domain.Populations;
using GeneticSharp.Domain.Randomizations;
using GeneticSharp.Domain.Selections;
using GeneticSharp.Domain.Terminations;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GeneticCore
{
    public class GeneticCoreSerivce
    {
        private readonly GeneticAlgoritmConfig config;

        public GeneticAlgorithm geneticAlgorithm { get; private set; }

        private readonly IRandomization randomization;

        public event Action<IEnumerable<IChromosome>> MigrationReady;

        public GeneticCoreSerivce(IRandomization randomization)
        {
            config = new GeneticAlgoritmConfig();
            InitiGA(config);
        }

        public void InitiGA(GeneticAlgoritmConfig config)
        {
            IChromosome adamChromosome = new FloatingPointChromosome(config.MinPopulationSize, config.MaxChromosomeValue, config.ChromosomeSize, config.ChromosomeFractionDigits);
            Population population = new Population(config.MinPopulationSize, config.MinPopulationSize, adamChromosome);

            IFitness fitness = new SimpleFitness();
            ISelection selection = new EliteSelection();
            ICrossover crossover = new CutAndSpliceCrossover();
            IMutation mutation = new UniformMutation();
            ITermination termination = new FitnessStagnationTermination(config.StagnationGenerationCount);

            population.GenerationStrategy = new PerformanceGenerationStrategy();

            GeneticAlgorithm ga = new GeneticAlgorithm(population, fitness, selection, crossover, mutation);
            ga.Termination = termination;

            ga.GenerationRan += OnNewGeneration;

            geneticAlgorithm = ga;
        }

        private void OnNewGeneration(object sender, EventArgs e)
        {
            if (randomization.GetFloat() <= config.MigrationProbability)
            {
                IEnumerable<IChromosome> migration = GetPersonForMigration(config.MigrationSize);
                MigrationReady(migration);
            }
        }

        /// <summary>
        /// Добавить особи в новое поколение
        /// </summary>
        /// <param name="persons"></param>
        public void AddIndividuals(List<IChromosome> persons)
        {
            if (geneticAlgorithm.IsRunning && persons.Count <= geneticAlgorithm.Population.MaxSize)
            {
                var currentChromosomes = geneticAlgorithm.Population.CurrentGeneration.Chromosomes;

                //Место для новых особей, если нет, сколько худших нужно убрать
                int countWorstForRemoving = geneticAlgorithm.Population.MaxSize - (geneticAlgorithm.Population.Generations.Count + persons.Count);

                if (countWorstForRemoving <= 0)
                {
                    persons.AddRange(currentChromosomes);
                }
                else
                {
                    var worstChromosomes = geneticAlgorithm.Population.CurrentGeneration.Chromosomes.OrderBy(c => c.Fitness).Take(countWorstForRemoving);
                    foreach (var worst in worstChromosomes)
                    {
                        geneticAlgorithm.Population.CurrentGeneration.Chromosomes.Remove(worst);
                    }
                }

                geneticAlgorithm.Population.CreateNewGeneration(persons.Shuffle(RandomizationProvider.Current).ToList());
            }
        }

        private IEnumerable<IChromosome> GetPersonForMigration(int migrationSize)
        {
            return geneticAlgorithm.Population.CurrentGeneration.Chromosomes.OrderByDescending(c => c.Fitness).Take(migrationSize);
        }
    }
}