using ActorMessages.Messages;
using Akka.Actor;
using Akka.Configuration;
using GeneticCore;
using GeneticSharp.Domain.Chromosomes;
using Island.Messages;
using Messages;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Island
{
    public class IslandActor : ReceiveActor
    {
        private IActorRef IslandProvider { get; set; }

        private GeneticCoreSerivce _geneticCore { get; set; }

        private readonly Guid _guid;

        public IslandActor(Config config, GeneticCoreSerivce geneticCore)
        {
            Receive<MigrateSolutionsMessage>(OnReceiveMigrateSolutions);
            Receive<SetupGeneticConfigMessage>(StartGeneticAlgorithm);
            Receive<BestFitnessValueRequest>(OnBestFitnessValue);

            _guid = Guid.NewGuid();
            _geneticCore = geneticCore;
            _geneticCore.MigrationReady += OnMigrationReady;
            BindIsland(config);
        }

        public static Props Props(Config config, GeneticCoreSerivce geneticCore)
        {
            return Akka.Actor.Props.Create(() => new IslandActor(config, geneticCore));
        }

        public void BindIsland(Config config)
        {
            string systemName = config.GetString("akka.remote.system-name");
            string hostname = config.GetString("akka.remote.dot-netty.tcp.hostname");
            string port = config.GetString("akka.remote.system-port");

            //var selection = Context.ActorSelection("akka.tcp://actorSystemName@10.0.0.1:2552/user/actorName");
            var hostActorPath = $"akka.tcp://{systemName}@{hostname}:{port}/user/island-router";

            ActorSelection selection = Context.ActorSelection(hostActorPath);
            try
            {
                IslandProvider = selection.ResolveOne(TimeSpan.FromSeconds(3)).Result;

                //IslandProvider.Tell(new Identify(_guid), Self);
                IslandProvider.Tell(new BindIslandMessage(), Self);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Island {Self.Path} binded to host {IslandProvider.Path.ToStringWithoutAddress()}");
                Console.ForegroundColor = ConsoleColor.White;
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Bind error to {hostActorPath} \n {ex.Message}");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        /// <summary>
        /// Сформирована новая популяция для миграции
        /// </summary>
        /// <param name="migration"></param>
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

        private void OnBestFitnessValue(BestFitnessValueRequest request)
        {
            Sender.Tell(new BestFitnessValueResponse(_geneticCore.BestFitnessValue));
        }
    }
}