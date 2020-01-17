using Akka.Actor;
using System;

namespace Island
{
    /// <summary>
    /// Запуск вычислительных узлов
    /// </summary>
    internal class SystemDaemon : ReceiveActor
    {
        protected override void PreStart()
        {
            InitiIslands();
        }

        /// <summary>
        /// Создать вычислительный узел на каждое ядро процессора
        /// </summary>
        private void InitiIslands()
        {
            for (var i = 0; i < Environment.ProcessorCount; i++)
            {
                Context.ActorOf<IslandActor>();
            }
        }
    }
}