using IslandGenetic.Interfaces;
using System.Collections.Generic;

namespace IslandGenetic
{
    public class Population<TChromosome> : IPopulation<TChromosome>
    {
        public IList<IIndividual<TChromosome>> Individuals { get; set; }
    }
}