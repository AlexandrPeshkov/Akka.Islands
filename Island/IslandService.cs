using Akka.Actor;
using Akka.Configuration;
using GeneticCore;

namespace Island
{
    public class IslandService
    {
        private readonly IActorRef _islandActor;

        public IslandService(ActorSystem actorSystem, GeneticCoreSerivce geneticCoreSerivce, Config config)
        {
            _islandActor = actorSystem.ActorOf(IslandActor.Props(config, geneticCoreSerivce));
        }
    }
}
