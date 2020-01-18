using Akka.Actor;
using GeneticCore;
using GeneticSharp.Domain.Chromosomes;
using Island.Messages;
using IslandRouter.Messages;
using Messages;
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
        public List<IActorRef> Islands { get; private set; }

        public IslandRouterActor()
        {
            Receive<MigrateSolutionsMessage>(OnReceiveMigrateSolutions);
            Receive<StartGAMessage>(StartGeneticAlgorithm);
            Receive<GetIslandCountMessage>(OnGetIslandCount);
            Receive<BindIslandMessage>(OnBindIsland);

            Solutions = new ConcurrentQueue<IChromosome>();
            Islands = new List<IActorRef>();
            System.Console.WriteLine($"Island router start on {Self.Path}"); 
        }

        private void OnReceiveMigrateSolutions(MigrateSolutionsMessage message)
        {
            foreach(var solution in message.Solutions)
            {
                Solutions.Enqueue(solution);
            }
        }

        private void OnGetIslandCount(GetIslandCountMessage islandCountMessage)
        {
            Sender.Tell(Islands.Count);
        }

        protected override void Unhandled(object message)
        {
            System.Console.WriteLine(message);
        }

        private void MigrateSolutionTo(IActorRef island, List<IChromosome> solutions)
        {
            MigrateSolutionsMessage message = new MigrateSolutionsMessage(solutions);
            island.Tell(message);
        }

        /// <summary>
        /// Зарегестрировать вычислительный остров
        /// </summary>
        public void OnBindIsland(BindIslandMessage bindIslandMessage)
        {
            Islands.Add(Sender);
            System.Console.WriteLine($"Binded Island {Sender.Path}");
        }

        public void StartGeneticAlgorithm(StartGAMessage startGAMessage)
        {
            Islands.ForEach(i => i.Tell(new SetupGeneticConfigMessage(new GeneticAlgoritmConfig())));
        }
    }
}