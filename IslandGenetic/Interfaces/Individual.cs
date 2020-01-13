using System.Collections.Generic;

namespace IslandGenetic.Interfaces
{
    public interface IIndividual
    {
        List<Chromosome> Genome { get; set; }
    }
}