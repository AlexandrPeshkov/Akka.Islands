namespace IslandGenetic.Interfaces
{
    /// <summary>
    /// Хромосома
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    public interface IChromosome<TValue>
    {
        TValue Value { get; set; }
    }
}