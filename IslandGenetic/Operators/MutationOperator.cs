using IslandGenetic.Interfaces;
using System;

namespace IslandGenetic.Operators
{
    public abstract class MutationOperator : IMutationOperator
    {
        protected readonly Random Random;

        public MutationOperator()
        {
            Random = new Random();
        }

        public abstract IIndividual Mutate(IIndividual individual);
    }
}