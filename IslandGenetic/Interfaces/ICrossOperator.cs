namespace IslandGenetic.Interfaces
{
    /// <summary>
    /// Оператор скрещивания
    /// </summary>
    public interface ICrossOperator
    {
        Individual Cross(Individual parent1, Individual parent2);
    }
}