using IslandGenetic.Interfaces;
using System.Collections.Generic;

namespace IslandGenetic
{
    /// <summary>
    /// Особь
    /// </summary>
    public class Individual<TChromosome> : IIndividual<TChromosome>
    {
        public IList<IChromosome<TChromosome>> Genome { get; set; }
    }
}