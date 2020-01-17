using IslandGenetic.Interfaces;
using System;
using System.Linq;

namespace IslandGenetic.Operators
{
    internal class SchwefelFunction : FitnessFunction
    {
        protected override Func<IIndividual, double> Function => (individual) =>
        {
            int N = individual.Genome.Count;
            double sum = 418.9829f * N;

            for (var i = 0; i < N; i++)
            {
                double value = BitConverter.ToDouble(individual.Genome[i].Value.ToArray());
                sum += value * Math.Sin(Math.Sqrt(Math.Abs(value)));
            }
            return sum;
        };

        public override double MinLimit => -500;

        public override double MaxLimit => 500;

        public override bool isMaximization => false;
    }
}