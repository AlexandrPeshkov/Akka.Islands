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
                double x = floatingPointChromosome.ToFloatingPoint();
                result = Math.Sin(Math.PI / 180 * x) - 1 / x;
            }
            return result;
        }
    }
}