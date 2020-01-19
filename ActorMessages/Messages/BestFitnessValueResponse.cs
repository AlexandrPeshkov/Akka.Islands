namespace ActorMessages.Messages
{
    public class BestFitnessValueResponse
    {
        public double FitnessValue { get; private set; }

        public BestFitnessValueResponse(double value) => (FitnessValue) = (value);
    }
}
