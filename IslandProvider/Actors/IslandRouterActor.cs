using ActorMessages.Messages;
using Akka.Actor;
using GeneticCore;
using GeneticSharp.Domain.Chromosomes;
using Island.Messages;
using IslandRouter.Messages;
using Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace IslandRouter
{
    public class IslandRouterActor : ReceiveActor
    {
        private const int islandSurveyMilisecondsDelay = 5 * 1000;

        private readonly GeneticAlgoritmConfig _geneticAlgoritmConfig;

        private readonly Timer _timer;

        private readonly Random _random;

        private bool IsGAStarted { get; set; }

        /// <summary>
        /// Очередь на миграцию
        /// </summary>
        private List<IChromosome> Solutions { get; set; }

        /// <summary>
        /// Подключенные острова и последнее известное лучшее решение
        /// </summary>
        private Dictionary<string, IslandValue> Islands { get; set; }

        private struct IslandValue
        {
            public double FitnessValue;

            public IActorRef Island;
        }


        public IslandRouterActor()
        {
            Receive<MigrateSolutionsMessage>(OnMigrateSolutions);
            Receive<StartGAMessage>(OnStartGeneticAlgorithm);
            Receive<GetIslandCountMessage>(OnGetIslandCount);
            Receive<BindIslandMessage>(OnBindIsland);
            Receive<Terminated>(OnTerminated);

           
            _geneticAlgoritmConfig = new GeneticAlgoritmConfig();
            _random = new Random();

            Solutions = new List<IChromosome>();
            Islands = new Dictionary<string, IslandValue>();
            IsGAStarted = false;

            _timer = new Timer(async (x) =>
            {
                await OnTimerElapsed();
            }, null, TimeSpan.Zero, TimeSpan.FromMilliseconds(islandSurveyMilisecondsDelay));

            Console.WriteLine($"Island router start on {Self.Path}");
        }

        #region ReceiveHandlers
        protected override void Unhandled(object message)
        {
            Console.WriteLine(message);
        }

        private void OnTerminated(Terminated terminated)
        {
            Islands.Remove(terminated.ActorRef.Path.ToStringWithoutAddress());
            Console.WriteLine($"Island {terminated.ActorRef.Path.ToStringWithUid()} was disconected");
        }

        /// <summary>
        /// Получено решение для миграции
        /// </summary>
        /// <param name="message"></param>
        private void OnMigrateSolutions(MigrateSolutionsMessage message)
        {
            foreach (var solution in message.Solutions)
            {
                Solutions.Add(solution);
            }
        }

        /// <summary>
        /// Число подключенных островов
        /// </summary>
        /// <param name="islandCountMessage"></param>
        private void OnGetIslandCount(GetIslandCountMessage islandCountMessage)
        {
            Sender.Tell(Islands.Count);
        }

        /// <summary>
        /// Зарегестрировать вычислительный остров
        /// </summary>
        private void OnBindIsland(BindIslandMessage bindIslandMessage)
        {
            Context.Watch(Sender);
            Islands.TryAdd(Sender.Path.ToStringWithoutAddress(), new IslandValue { FitnessValue = 0, Island = Sender });
            Console.ForegroundColor = ConsoleColor.Green;
            System.Console.WriteLine($"Binded Island {Sender.Path.ToStringWithUid()}");
            Console.ForegroundColor = ConsoleColor.White;
        }

        /// <summary>
        /// Запуск вычислений ГА
        /// </summary>
        /// <param name="startGAMessage"></param>
        private void OnStartGeneticAlgorithm(StartGAMessage startGAMessage)
        {
            IsGAStarted = true;
            foreach (var islandStruct in Islands.Values)
            {
                islandStruct.Island.Tell(new SetupGeneticConfigMessage(_geneticAlgoritmConfig));
            }
        }

        #endregion ReceiveHandlers


        #region RouteLogic
        private async Task OnTimerElapsed()
        {
            if (IsGAStarted)
            {
                await RefreshIslandFitnesValues();
                DistributeSolutions();
            }
        }

        /// <summary>
        /// Запросить текущие значения лучшего решения
        /// </summary>
        /// <returns></returns>
        private async Task RefreshIslandFitnesValues()
        {
            foreach (var islandStruct in Islands.ToList())
            {
                double currentFitness = islandStruct.Value.FitnessValue;
                BestFitnessValueResponse bestFitnessValueMessage = await islandStruct.Value.Island.Ask<BestFitnessValueResponse>(new BestFitnessValueRequest());

                if (_geneticAlgoritmConfig.Predicate(bestFitnessValueMessage.FitnessValue, currentFitness))
                {
                    Islands.Remove(islandStruct.Key);
                    Islands.TryAdd(islandStruct.Key, new IslandValue { FitnessValue = bestFitnessValueMessage.FitnessValue, Island = islandStruct.Value.Island });
                }
            }
        }

        /// <summary>
        /// Распределить решения
        /// </summary>
        private void DistributeSolutions()
        {
           while (Solutions.Count >= _geneticAlgoritmConfig.MigrationSize)
            {
                List<IChromosome> solutions = new List<IChromosome>();

                while (solutions.Count < _geneticAlgoritmConfig.MigrationSize)
                {
                    int solutionIndex = _random.Next(0, Solutions.Count - 1);
                    IChromosome chromosome = Solutions[solutionIndex];
                    solutions.Add(chromosome);
                }
                IActorRef island = WorstIsland();
                MigrateSolutionTo(island, solutions);
            }
        }
        
        /// <summary>
        /// Остров с худшим решением
        /// </summary>
        /// <returns></returns>
        private IActorRef WorstIsland()
        {
            if(_geneticAlgoritmConfig.IsMaximization)
            {
                return Islands.Values.FirstOrDefault(i => i.FitnessValue == Islands.Min(x => x.Value.FitnessValue)).Island;
            }
            return Islands.Values.FirstOrDefault(i => i.FitnessValue == Islands.Max(x => x.Value.FitnessValue)).Island;
        }

        /// <summary>
        /// Отправить решение на другой остров
        /// </summary>
        /// <param name="island">Остров</param>
        /// <param name="solutions">Миграция</param>
        private void MigrateSolutionTo(IActorRef island, List<IChromosome> solutions)
        {
            MigrateSolutionsMessage message = new MigrateSolutionsMessage(solutions);
            island.Tell(message);
        }
        #endregion RouteLogic
    }
}