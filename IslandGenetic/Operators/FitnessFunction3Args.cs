using IslandGenetic.Interfaces;
using System;

namespace IslandGenetic.Operators
{
    internal class FitnessFunction3Args : FitnessFunction
    {
        protected override Func<IIndividual, double> Function => (individual) =>
        {
            double x = BitConverter.ToSingle(individual.Genome[0].Value.ToArray());
            double y = BitConverter.ToSingle(individual.Genome[1].Value.ToArray());
            double n = BitConverter.ToSingle(individual.Genome[2].Value.ToArray());

            return Math.Pow(15 * x * y * (1 - x) * (1 - y) * Math.Sin(n * Math.PI * x) * Math.Sin(n * Math.PI * y), 2);
        };

        public override int Size => 3;
    }
}