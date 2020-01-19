using GeneticSharp.Domain.Chromosomes;
using GeneticSharp.Domain.Fitnesses;
using System;
using System.Linq;

namespace GeneticCore
{
    /// <summary>
    /// Функция Швефеля
    /// </summary>
    public class ShwefelFunction : IFitness
    {
        public double Evaluate(IChromosome chromosome)
        {
            double result = -1;
            if (chromosome != null)
            {
                result = 418.9829 * chromosome.Length;
                byte[] genes = chromosome.GetGenes().Select(g => (byte)(int)g.Value).ToArray();
                double x = BitConverter.ToDouble(genes);
                result -= x * Math.Sin(Math.Sqrt(Math.Abs(x)));
            }
            return result;
        }
    }
}
