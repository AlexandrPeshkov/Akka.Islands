using IslandGenetic.Interfaces;
using System.Collections.Generic;

namespace IslandGenetic.Operators
{
    internal abstract class SelectionOperator : ISelectionOperator
    {
        public abstract IEnumerable<IIndividual> Select(IPopulation population, IFitnessFunction fitnessFunction, float selectionPercent);
    }
}