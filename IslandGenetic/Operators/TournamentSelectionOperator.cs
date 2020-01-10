using System;
using System.Collections.Generic;
using IslandGenetic.Interfaces;

namespace IslandGenetic.Operators
{
    internal class TournamentSelectionOperator : SelectionOperator<double>
    {
        public override IEnumerable<IIndividual<double>> Select(IPopulation<double> population)
        {
            if (population.Individuals.Count < 2) throw new ArgumentException("Необходимо минимум две особи в популяции");
        }
    }
}