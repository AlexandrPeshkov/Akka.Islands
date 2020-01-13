using IslandGenetic.Interfaces;
using System.Collections.Generic;

namespace IslandGenetic
{
    /// <summary>
    /// Особь
    /// </summary>
    public class Individual : IIndividual
    {
        public List<Chromosome> Genome { get; set; }

        public Individual(List<Chromosome> genome = null)
        {
            Genome = genome ?? new List<Chromosome>();
        }
    }
}