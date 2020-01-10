using System.Collections.Generic;

namespace IslandGenetic.Interfaces
{
    public interface ISelectionOperator<TChromosome>
    {
        public IEnumerable<IIndividual<TChromosome>> Select(IPopulation<TChromosome> population);
    }
}