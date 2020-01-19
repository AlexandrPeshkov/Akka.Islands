using GeneticSharp.Domain.Randomizations;
using System;
using Xunit;

namespace GeneticCore.Tests
{
    public class GeneticCoreServiceTests
    {
        [Fact]
        public void StartGA()
        {
            IRandomization randomization = new FastRandomRandomization();
            GeneticCoreSerivce geneticCoreSerivce = new GeneticCoreSerivce(randomization);
            GeneticAlgoritmConfig config = new GeneticAlgoritmConfig();
            geneticCoreSerivce.InitiGA(config);
        }
    }
}
