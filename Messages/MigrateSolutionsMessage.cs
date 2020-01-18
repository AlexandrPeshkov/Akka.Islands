using System.Collections.Generic;

namespace Island.Messages
{
    public class MigrateSolutionsMessage
    {
        public IEnumerable<IChromosome> Solutions { get; private set; }

        public MigrateSolutionsMessage(IEnumerable<IChromosome> solutions) => (Solutions) = (solutions);
    }
}