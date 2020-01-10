namespace IslandGenetic.Interfaces
{
    public interface IFitnessFunction
    {
    }

    /// <summary>
    /// Фитнесс функция
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <typeparam name="T0"></typeparam>
    public interface IFitnessFunction<TResult, T0>
    {
        TResult Value(T0 arg0);
    }

    /// <summary>
    /// Фитнесс функция
    /// </summary>
    public interface IFitnessFunction<TResult, T0, T1>
    {
        TResult Value(T0 arg0, T1 arg1);
    }

    /// <summary>
    /// Фитнесс функция
    /// </summary>
    public interface IFitnessFunction<TResult, T0, T1, T2>
    {
        TResult Value(T0 arg0, T1 arg1, T2 arg2);
    }

    /// <summary>
    /// Фитнесс функция
    /// </summary>
    public interface IFitnessFunction<TResult, T0, T1, T2, T3>
    {
        TResult Value(T0 arg0, T1 arg1, T2 arg2, T3 arg3);
    }
}