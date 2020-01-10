using IslandGenetic.Interfaces;
using System;
using System.Collections;

namespace IslandGenetic.Operators
{
    public class RandomMutationOperator : MutationOperator<double>
    {
        public override IChromosome<double> Mutate(IChromosome<double> chromosome)
        {
            BitArray bites = new BitArray(BitConverter.GetBytes(chromosome.Value));
            int index = Random.Next(0, bites.Length - 1);
            bites[index] = !bites[index];
            return chromosome;
        }
    }
}