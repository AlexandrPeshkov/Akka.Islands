using System.Collections.Generic;

namespace IslandGenetic.Interfaces
{
    /// <summary>
    /// Популяция
    /// </summary>
    /// <typeparam name="TChromosome"></typeparam>
    public interface IPopulation<TChromosome>
    {
        public IList<IIndividual<TChromosome>> Individuals { get; set; }
    }
}