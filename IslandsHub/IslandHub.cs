using Akka.Actor;
using GAF;
using Island;
using System.Collections.Generic;

namespace IslandsHub
{
    public class IslandHub : ReceiveActor
    {
        private List<IActorRef> Islands { get; set; }

        private readonly ActorSystem _actorSystem;

        private readonly string IndentifyId = "IslandRoot";

        private Queue<MigrationMessage> Migrations { get; set; }

        public IslandHub()
        {
            _actorSystem = ActorSystem.Create("IslandGeneticAlgorithm.ActorSystem");

            Receive<ActorIdentity>(AddIsland);
        }

        public void AddIsland(ActorIdentity island)
        {
            //_actorSystem.ActorOf<IslandActor>($"$$Island {hostName}");
            Context.Watch(island);

            var selection = Context.ActorSelection("/user/another");
            selection.Tell(new Identify(identifyId), Self);
        }

        public void RunEvolution()
        {
        }

        public void ShowIslandBestSolutions()
        {
        }

        private void SendSolution(MigrationMessage migrationMessage, IActorRef actor)
        {
            actor.Tell(migrationMessage);
        }

        private void ReceiveMigration(MigrationMessage migrationMessage)
        {
            Migrations.Enqueue(migrationMessage);
        }
    }
}