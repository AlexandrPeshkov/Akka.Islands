using Akka.Actor;
using GeneticCore;
using GeneticSharp.Domain.Chromosomes;
using IslandRouter.Messages;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace IslandRouter
{
    public class IslandRouterActor : ReceiveActor
    {
        /// <summary>
        /// Очередь на миграцию
        /// </summary>
        private ConcurrentQueue<IChromosome> Solutions { get; set; }

        /// <summary>
        /// Подключенные острова
        /// </summary>
        private List<IActorRef> Islands { get; set; }

        public IslandRouterActor()
        {
            Receive<MigrateSolutionsMessage>(OnReceiveMigrateSolutions);
            Solutions = new ConcurrentQueue<IChromosome>();
            Islands = new List<IActorRef>();
        }

        private void OnReceiveMigrateSolutions(MigrateSolutionsMessage message)
        {
        }

        private void MigrateSolutionTo(IActorRef island, List<IChromosome> solutions)
        {
            MigrateSolutionsMessage message = new MigrateSolutionsMessage(solutions);
            island.Tell(message);
        }
    }
}