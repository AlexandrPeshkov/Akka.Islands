using System.Collections.Generic;

namespace IslandGenetic.Interfaces
{
    /// <summary>
    /// Популяция
    /// </summary>
    public interface IPopulation
    {
        public List<Individual> Individuals { get; set; }

        public Individual WorstIndividual(IPopulation population, IFitnessFunction fitnessFunction);

        public Individual BestIndividual(IPopulation population, IFitnessFunction fitnessFunction);
    }
}