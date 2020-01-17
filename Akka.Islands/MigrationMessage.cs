using GAF;
using System.Collections.Generic;

namespace Island
{
    /// <summary>
    /// Сообщение миграции особей
    /// </summary>
    public class MigrationMessage
    {
        public MigrationMessage(List<Chromosome> solutions) => (Solutions) = (solutions);

        /// <summary>
        /// Особи
        /// </summary>
        public List<Chromosome> Solutions { get; private set; }
    }
}