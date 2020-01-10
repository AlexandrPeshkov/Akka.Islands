using System;

namespace IslandGenetic.Operators
{
    internal class FitnessFunction3Args : FitnessFunction<double, double, double, double>
    {
        protected override Func<double, double, double, double> Function =>
            (x, y, n) => Math.Pow(15 * x * y * (1 - x) * (1 - y) * Math.Sin(n * Math.PI * x) * Math.Sin(n * Math.PI * y), 2);
    }
}