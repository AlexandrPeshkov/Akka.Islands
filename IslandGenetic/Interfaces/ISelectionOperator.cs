using System.Collections.Generic;

namespace IslandGenetic.Interfaces
{
    public interface ISelectionOperator
    {
        public IEnumerable<IIndividual> Select(IPopulation population, IFitnessFunction fitnessFunction, float selectionPercent);
    }
}