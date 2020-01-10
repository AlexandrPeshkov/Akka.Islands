using IslandGenetic.Interfaces;
using System;

namespace IslandGenetic
{
    internal abstract class FitnessFunction<TResult, T0> : IFitnessFunction<TResult, T0>
    {
        protected abstract Func<T0, TResult> Function { get; }

        public TResult Value(T0 arg0) => Function(arg0);
    }

    internal abstract class FitnessFunction<TResult, T0, T1> : IFitnessFunction<TResult, T0, T1>
    {
        protected abstract Func<T0, T1, TResult> Function { get; }

        public TResult Value(T0 arg0, T1 arg1) => Function(arg0, arg1);
    }

    internal abstract class FitnessFunction<TResult, T0, T1, T2> : IFitnessFunction<TResult, T0, T1, T2>
    {
        protected abstract Func<T0, T1, T2, TResult> Function { get; }

        public TResult Value(T0 arg0, T1 arg1, T2 arg2) => Function(arg0, arg1, arg2);
    }

    internal abstract class FitnessFunction<TResult, T0, T1, T2, T3> : IFitnessFunction<TResult, T0, T1, T2, T3>
    {
        protected abstract Func<T0, T1, T2, T3, TResult> Function { get; }

        public TResult Value(T0 arg0, T1 arg1, T2 arg2, T3 arg3) => Function(arg0, arg1, arg2, arg3);
    }
}