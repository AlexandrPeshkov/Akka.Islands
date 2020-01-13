using System;
using System.Collections.Generic;
using System.Linq;
using IslandGenetic.Interfaces;

namespace IslandGenetic.Operators
{
    /// <summary>
    /// Турнирный отбор
    /// </summary>
    internal class TournamentSelectionOperator : SelectionOperator
    {
        public override IEnumerable<IIndividual> Select(IPopulation population, IFitnessFunction fitnessFunction, float selectionPercent = 0.2f)
        {
            if (population.Individuals.Count < 2) throw new ArgumentException("Необходимо минимум две особи в популяции");

            if (selectionPercent <= 0 || selectionPercent >= 1) throw new ArgumentException("Процент выборки не должен быть отрицательным или превышать 1");

            int groupCount = (int)Math.Ceiling(1 / selectionPercent);
            int groupMaxSize = (int)Math.Ceiling((double)population.Individuals.Count / groupCount);

            List<IIndividual> winners = new List<IIndividual>(groupCount);

            for (var i = 0; i < groupCount; i++)
            {
                var group = population.Individuals.Skip(i * groupMaxSize).Take(groupMaxSize);

                IIndividual winner = group.FirstOrDefault(i => fitnessFunction.Value(i) == group.Max(g => fitnessFunction.Value(g)));
                winners.Add(winner);
            }
            return winners;
        }
    }
}