using IslandGenetic.Interfaces;
using System.Collections.Generic;

namespace IslandGenetic.Operators
{
    internal abstract class SelectionOperator<TChromosome> : ISelectionOperator<TChromosome>
    {
        public abstract IEnumerable<IIndividual<TChromosome>> Select(IPopulation<TChromosome> population);
    }
}