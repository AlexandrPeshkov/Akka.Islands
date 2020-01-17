using IslandGenetic.Interfaces;
using System;

namespace IslandGenetic
{
    internal abstract class FitnessFunction : IFitnessFunction
    {
        public abstract double MinLimit { get; }
        public abstract double MaxLimit { get; }

        public abstract bool isMaximization { get; }

        protected abstract Func<IIndividual, double> Function { get; }

        public double Value(IIndividual individual)
        {
            return Function(individual);
        }
    }
}