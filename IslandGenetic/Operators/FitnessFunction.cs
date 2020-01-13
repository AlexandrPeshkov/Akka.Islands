using IslandGenetic.Interfaces;
using System;

namespace IslandGenetic
{
    internal abstract class FitnessFunction : IFitnessFunction
    {
        public abstract int Size { get; }

        protected abstract Func<IIndividual, double> Function { get; }

        public double Value(IIndividual individual)
        {
            return Function(individual);
        }
    }
}