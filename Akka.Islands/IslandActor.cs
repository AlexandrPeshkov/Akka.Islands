using Akka.Actor;
using Akka.Event;

namespace Island
{
    /// <summary>
    /// Вычислительный остров
    /// </summary>
    public class IslandActor : ReceiveActor
    {
        private readonly ILoggingAdapter log = Context.GetLogger();

        public IslandActor()
        {
            Receive<MigrationMessage>(message =>
            {
                message.Solutions *
            });
        }
    }
}