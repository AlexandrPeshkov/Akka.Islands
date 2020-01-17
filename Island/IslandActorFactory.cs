using Akka.Actor;

namespace IslandRouter.Actors
{
    public class IslandActorFactory
    {
        private readonly ActorSystem _actorSystem;

        public IslandActorFactory(ActorSystem actorSystem)
        {
            _actorSystem = actorSystem;
        }

        public IActorRef CreateIsland()
        {
            return _actorSystem.ActorOf<IslandRouterActor>();
        }
    }
}