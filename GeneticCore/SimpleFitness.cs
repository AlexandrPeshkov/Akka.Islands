using GeneticSharp.Domain.Chromosomes;
using GeneticSharp.Domain.Fitnesses;
using System;

namespace GeneticCore
{
    public class SimpleFitness : IFitness
    {
        public double Evaluate(IChromosome chromosome)
        {
            double result = double.NaN;

            if (chromosome is FloatingPointChromosome floatingPointChromosome)
            {
                var values = floatingPointChromosome.ToFloatingPoints();
                var x1 = values[0];
                var y1 = values[1];
                var x2 = values[2];
                var y2 = values[3];

                return Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2));
            }
            return result;
        }

        //public double Evaluate(IChromosome chromosome)
        //{
        //    double result = double.NaN;

        //    if (chromosome is FloatingPointChromosome floatingPointChromosome)
        //    {
        //        var values = floatingPointChromosome.ToFloatingPoints();
        //        var x = values[0];
        //        var y = values[1];
        //        var z = values[2];
        //        var t = values[3];

        //        result =  1 / Math.Abs(x + y + z - t);
        //    }
        //    return result;
        //}
    }
}