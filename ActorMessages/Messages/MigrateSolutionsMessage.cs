using GeneticSharp.Domain.Chromosomes;
using System.Collections.Generic;

namespace Island.Messages
{
    public class MigrateSolutionsMessage
    {
        /// <summary>
        /// Лучшие решения
        /// </summary>
        public IEnumerable<IChromosome> Solutions { get; private set; }

        public MigrateSolutionsMessage(IEnumerable<IChromosome> solutions) => (Solutions) = (solutions);
    }
}