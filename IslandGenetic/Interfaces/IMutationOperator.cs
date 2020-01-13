namespace IslandGenetic.Interfaces
{
    /// <summary>
    /// Оператор мутации
    /// </summary>
    /// <typeparam name="TChromosome"></typeparam>
    public interface IMutationOperator
    {
        public IIndividual Mutate(IIndividual individual);
    }
}