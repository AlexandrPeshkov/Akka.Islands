using System;
using System.Linq;

namespace IslandGenetic.Operators
{
    public class CrossOperator
    {
        private readonly Random Random = new Random();

        /// <summary>
        /// Скрестить особи
        /// </summary>
        /// <param name="parent1">Родительская особь 1</param>
        /// <param name="parent2">Родительская особь 2</param>
        /// <returns>Пара дочерних особей</returns>
        public (Individual, Individual) Cross(Individual parent1, Individual parent2)
        {
            if (parent1.Genome.Count != parent2.Genome.Count) throw new ArgumentException("Размер генома родительских особей должен совпадать");

            Individual child1 = new Individual();
            Individual child2 = new Individual();

            for (var i = 0; i < parent1.Genome.Count; i++)
            {
                (Chromosome c1, Chromosome c2) chromosomes = CrossChromosome(parent1.Genome[i], parent2.Genome[i]);
                if (Random.NextDouble() > 0.5)
                {
                    child1.Genome.Add(chromosomes.c1);
                    child2.Genome.Add(chromosomes.c2);
                }
                else
                {
                    child1.Genome.Add(chromosomes.c2);
                    child2.Genome.Add(chromosomes.c1);
                }
            }
            return (child1, child2);
        }

        private (Chromosome, Chromosome) CrossChromosome(Chromosome chromosome1, Chromosome chromosome2)
        {
            if (chromosome1.Value.Count != chromosome1.Value.Count) throw new ArgumentException("Размер генома родительских особей должен совпадать");
            if (chromosome1.Value.Count < 2) throw new ArgumentException("Размер хромосомы должен быть больше 2");

            int splitIndex = Random.Next(0, chromosome1.Value.Count - 1);

            var gen1 = chromosome1.Value.Skip(splitIndex).ToList();
            gen1.AddRange(chromosome2.Value.Take(splitIndex));

            Chromosome newChromosome1 = new Chromosome
            {
                Value = gen1
            };

            var gen2 = chromosome2.Value.Skip(splitIndex).ToList();
            gen2.AddRange(chromosome1.Value.Take(splitIndex));

            Chromosome newChromosome2 = new Chromosome
            {
                Value = gen2
            };

            return (newChromosome1, newChromosome2);
        }
    }
}