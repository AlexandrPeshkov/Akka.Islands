using Akka.Actor;
using Island.Messages;

namespace Island
{
    public class IslandActor : ReceiveActor
    {
        public IslandActor()
        {
            Receive<MigrateSolutionsMessage>(OnReceiveMigrateSolutions);
        }

        /// <summary>
        /// Принять популяцию
        /// </summary>
        /// <param name="message"></param>
        private void OnReceiveMigrateSolutions(MigrateSolutionsMessage message)
        {
        }
    }
}