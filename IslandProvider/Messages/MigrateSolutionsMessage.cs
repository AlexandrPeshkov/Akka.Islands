using GeneticSharp.Domain.Chromosomes;
using System.Collections.Generic;

namespace IslandRouter.Messages
{
    public class MigrateSolutionsMessage
    {
        public List<IChromosome> Solutions { get; private set; }

        public MigrateSolutionsMessage(List<IChromosome> solutions) => (Solutions) = (solutions);
    }
}