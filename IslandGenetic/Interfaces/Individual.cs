using System.Collections.Generic;

namespace IslandGenetic.Interfaces
{
    public interface IIndividual<TChromosome>
    {
        public IList<IChromosome<TChromosome>> Genome { get; set; }
    }
}