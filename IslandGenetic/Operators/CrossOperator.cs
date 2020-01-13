using System;

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

            int splitIndex = parent1.Genome.Count / 2;
            for (var i = 0; i < parent1.Genome.Count; i++)
            {
                if (i <= splitIndex)
                {
                    parent1.Genome[i] = parent2.Genome[i];
                }
                else
                {
                    parent1
                }
            }
            parent1.Genome.
        }
    }
}