using IslandGenetic.Interfaces;

namespace IslandGenetic
{
    public abstract class AbstractGeneticService<TChromosome, TResult> : IGeneticService<TChromosome, TResult>
    {
        public IFitnessFunction<TChromosome, TChromosome> FitnessFunction { get; set; }

        public IPopulation<TChromosome> Population { get; set; }

        #region Operators

        public IMutationOperator<TChromosome> MutationOperator { get; set; }

        #endregion Operators

        #region Methods

        public abstract IIndividual<TChromosome> WorstIndividual(IPopulation<TChromosome> population);

        public abstract IIndividual<TChromosome> BestIndividual(IPopulation<TChromosome> population);

        #endregion Methods

        #region Algorithm

        protected abstract IPopulation<TChromosome> InitPopulation(int size);

        #endregion Algorithm
    }
}