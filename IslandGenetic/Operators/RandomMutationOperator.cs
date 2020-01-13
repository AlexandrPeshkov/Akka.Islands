using IslandGenetic.Interfaces;
using System;
using System.Collections;

namespace IslandGenetic.Operators
{
    /// <summary>
    /// Двухточечный оператор мутации
    /// </summary>
    public class RandomMutationOperator : MutationOperator
    {
        public override IIndividual Mutate(IIndividual individual)
        {
            int chromosome1Index = Random.Next(0, individual.Genome.Count - 1);
            int chromosome2Index = -1;
            while (chromosome2Index == -1 || chromosome2Index == chromosome1Index)
            {
                chromosome2Index = Random.Next(0, individual.Genome.Count - 1);
            }

            IChromosome chromosome1 = individual.Genome[chromosome1Index];
            IChromosome chromosome2 = individual.Genome[chromosome2Index];

            chromosome1 = BitChanger(chromosome1);
            chromosome2 = BitChanger(chromosome2);
            return individual;
        }

        private IChromosome BitChanger(IChromosome chromosome)
        {
            BitArray bites = new BitArray(chromosome.Value.ToArray());
            int index = Random.Next(0, bites.Length - 1);
            bites[index] = !bites[index];
            return chromosome;
        }
    }
}