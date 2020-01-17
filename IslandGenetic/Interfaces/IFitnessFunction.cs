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

        double MinLimit { get; }

        double MaxLimit { get; }

        /// <summary>
        /// Максимизация ЦФ
        /// </summary>
        bool isMaximization { get; }
    }
}