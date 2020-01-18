using Akka.Actor;
using IslandRouter.Messages;
using System;
using System.Threading.Tasks;

namespace IslandRouter.Services
{
    public class IslandRouterService
    {
        private readonly IActorRef _routerActor;

        private const int _maxAskDelayMiliseconds = 20000;

        public IslandRouterService(ActorSystem actorSystem)
        {
            _routerActor = actorSystem.ActorOf<IslandRouterActor>("island-router");
        }

        public void StartGeneticAlgorithm()
        {
            _routerActor.Tell(new StartGAMessage());
        }

        public async Task<int> ActiveIslands()
        {
            return await _routerActor.Ask<int>(new GetIslandCountMessage(), TimeSpan.FromMilliseconds(_maxAskDelayMiliseconds));
        }
    }
}
