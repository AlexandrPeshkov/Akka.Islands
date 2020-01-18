using Akka.Actor;
using Akka.Configuration;
using GeneticCore;
using GeneticSharp.Domain.Chromosomes;
using Island.Messages;
using Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Island
{
    public class IslandActor : ReceiveActor
    {
        private IActorRef IslandProvider { get; set; }

        private GeneticCoreSerivce _geneticCore { get; set; }

        public IslandActor(Config config, GeneticCoreSerivce geneticCore)
        {
            Receive<MigrateSolutionsMessage>(OnReceiveMigrateSolutions);
            Receive<SetupGeneticConfigMessage>(StartGeneticAlgorithm);

            _geneticCore = geneticCore;
            BindIsland(config).Wait();
        }

        public static Props Props(Config config, GeneticCoreSerivce geneticCore)
        {
            return Akka.Actor.Props.Create(() => new IslandActor(config, geneticCore));
        }

        public async Task BindIsland(Config config)
        {
            string systemName = config.GetString("akka.remote.system-name");
            string hostname = config.GetString("akka.remote.dot-netty.tcp.hostname");
            string port = config.GetString("akka.remote.system-port");

            //var selection = Context.ActorSelection("akka.tcp://actorSystemName@10.0.0.1:2552/user/actorName");
            var actorPath = $"akka.tcp://{systemName}@{hostname}:{port}/user/island-router";

            ActorSelection selection = Context.ActorSelection(actorPath);
            IslandProvider = await selection.ResolveOne(TimeSpan.FromSeconds(3));

            IslandProvider.Tell(new BindIslandMessage());
            //IslandProvider.Tell("hello");
        }


        private void OnMigrationReady(IEnumerable<IChromosome> migration)
        {
            IslandProvider.Tell(new MigrateSolutionsMessage(migration));
        }

        /// <summary>
        /// Принять популяцию
        /// </summary>
        /// <param name="message"></param>
        private void OnReceiveMigrateSolutions(MigrateSolutionsMessage message)
        {
            _geneticCore.AddIndividuals(message.Solutions.ToList());
        }

        private void StartGeneticAlgorithm(SetupGeneticConfigMessage configMessage)
        {
            _geneticCore.InitiGA(configMessage.GeneticAlgoritmConfig);
        }
    }
}