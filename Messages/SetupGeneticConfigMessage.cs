using GeneticCore;

namespace Island.Messages
{
    public class SetupGeneticConfigMessage
    {
        public GeneticAlgoritmConfig GeneticAlgoritmConfig { get; private set; }

        public SetupGeneticConfigMessage(GeneticAlgoritmConfig config)
        {
            GeneticAlgoritmConfig = config;
        }
    }
}
