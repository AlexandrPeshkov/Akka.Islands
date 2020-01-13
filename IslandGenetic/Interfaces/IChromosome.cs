using System.Collections.Generic;

namespace IslandGenetic.Interfaces
{
    /// <summary>
    /// Хромосома
    /// </summary>
    public interface IChromosome
    {
        List<byte> Value { get; set; }
    }
}