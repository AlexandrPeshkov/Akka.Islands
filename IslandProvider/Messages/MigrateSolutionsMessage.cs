using GAF;
using System.Collections.Generic;

namespace IslandRouter.Messages
{
    public class MigrateSolutionsMessage
    {
        public List<Chromosome> Solutions { get; private set; }

        public MigrateSolutionsMessage(List<Chromosome> solutions) => (Solutions) = (solutions);
    }
}