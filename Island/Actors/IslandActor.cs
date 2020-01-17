using Akka.Actor;
using GeneticCore;
using GeneticSharp.Domain.Chromosomes;
using Island.Messages;
using System.Collections.Generic;
using System.Linq;

namespace Island
{
    public class IslandActor : ReceiveActor
    {
        private readonly GeneticCoreSerivce _geneticCore;

        private IActorRef IslandProvider { get; set; }

        public IslandActor(GeneticCoreSerivce geneticCore)
        {
            Receive<MigrateSolutionsMessage>(OnReceiveMigrateSolutions);

            _geneticCore = geneticCore;
            _geneticCore.MigrationReady += OnMigrationReady;
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
    }
}