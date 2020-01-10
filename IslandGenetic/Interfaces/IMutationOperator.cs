namespace IslandGenetic.Interfaces
{
    /// <summary>
    /// Оператор мутации
    /// </summary>
    /// <typeparam name="TChromosome"></typeparam>
    public interface IMutationOperator<TChromosome>
    {
        public IChromosome<TChromosome> Mutate(IChromosome<TChromosome> chromosome);
    }
}