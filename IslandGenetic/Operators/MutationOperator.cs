using IslandGenetic.Interfaces;
using System;

namespace IslandGenetic.Operators
{
    public abstract class MutationOperator<TChromosome> : IMutationOperator<TChromosome>
    {
        protected readonly Random Random;

        public MutationOperator()
        {
            Random = new Random();
        }

        public abstract IChromosome<TChromosome> Mutate(IChromosome<TChromosome> chromosome);
    }
}