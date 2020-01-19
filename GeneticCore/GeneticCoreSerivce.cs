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
        private GeneticAlgoritmConfig Config { get; set; }

        private readonly IRandomization _randomization;

        public event Action<IEnumerable<IChromosome>> MigrationReady;

        public GeneticAlgorithm geneticAlgorithm { get; private set; }

        public double BestFitnessValue => geneticAlgorithm.Fitness.Evaluate(geneticAlgorithm.BestChromosome);

        public GeneticCoreSerivce(IRandomization randomization)
        {
            _randomization = randomization;
        }

        public void InitiGA(GeneticAlgoritmConfig config)
        {
            Config = config;
            //IChromosome adamChromosome = new FloatingPointChromosome(config.MinChromosomeValue, config.MaxChromosomeValue, config.ChromosomeSize, config.ChromosomeFractionDigits);

            float maxWidth = 1000000;
            float maxHeight = 1000000;
            IChromosome adamChromosome = new FloatingPointChromosome(
                 new double[] { 0, 0, 0, 0 },
                 new double[] { maxWidth, maxHeight, maxWidth, maxHeight },
                 new int[] { 64, 64, 64, 64 },
                 new int[] { 2, 2, 2, 2 });

            Population population = new Population(config.MinPopulationSize, config.MinPopulationSize, adamChromosome);

            IFitness fitness = new SimpleFitness();
            ISelection selection = new EliteSelection();
            ICrossover crossover = new UniformCrossover(0.1f);
            IMutation mutation = new FlipBitMutation();
            ITermination termination = new FitnessStagnationTermination(config.StagnationGenerationCount);

            population.GenerationStrategy = new PerformanceGenerationStrategy();

            geneticAlgorithm = new GeneticAlgorithm(population, fitness, selection, crossover, mutation)
            {
                Termination = termination
            };

            geneticAlgorithm.CrossoverProbability = config.CrossoverProbability;
            geneticAlgorithm.MutationProbability = config.MutationProbability;

            geneticAlgorithm.GenerationRan += OnNewGeneration;
            geneticAlgorithm.TerminationReached += OnTermination;
            geneticAlgorithm.Start();
        }

        private void OnTermination(object sender, EventArgs e)
        {
            Console.WriteLine($"FINISHED {BestFitnessValue}");
        }

        private void OnNewGeneration(object sender, EventArgs e)
        {
            if (_randomization.GetFloat() <= Config.MigrationProbability)
            {
                IEnumerable<IChromosome> migration = GetPersonForMigration(Config.MigrationSize);
                MigrationReady?.Invoke(migration);
            }
            Console.WriteLine($"Best value {BestFitnessValue}");
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