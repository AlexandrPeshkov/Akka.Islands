using IslandGenetic.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace IslandGenetic
{
    public class Population : IPopulation
    {
        public Population(List<Individual> individuals = null)
        {
            Individuals = individuals ?? new List<Individual>();
        }

        public List<Individual> Individuals { get; set; }

        public Individual WorstIndividual(IPopulation population, IFitnessFunction fitnessFunction)
        {
            return Individuals.FirstOrDefault(i => fitnessFunction.Value(i) == Individuals.Min(s => fitnessFunction.Value(s)));
        }

        public Individual BestIndividual(IPopulation population, IFitnessFunction fitnessFunction)
        {
            return Individuals.FirstOrDefault(i => fitnessFunction.Value(i) == Individuals.Max(s => fitnessFunction.Value(s)));
        }
    }
}