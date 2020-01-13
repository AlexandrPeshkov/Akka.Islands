namespace IslandGenetic.Interfaces
{
    /// <summary>
    /// Фитнесс функция
    /// </summary>
    public interface IFitnessFunction
    {
        /// <summary>
        /// Значение
        /// </summary>
        /// <param name="individual">Особь</param>
        /// <returns></returns>
        double Value(IIndividual individual);

        /// <summary>
        /// Число аргументов
        /// </summary>
        int Size { get; }
    }
}