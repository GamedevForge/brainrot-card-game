using System;

namespace Project.Core.Sevices.StateMachine
{
    public class Transition<TFrom, TTo> : ITransition
        where TFrom : IState
        where TTo : IState
    {
        public Type To { get; }

        public Transition() =>
            To = typeof(TTo);

        public bool CanTranslate(IState fromState) =>
            fromState is TFrom;
    }
}