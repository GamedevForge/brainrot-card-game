using System;

namespace Project.Core.Sevices.StateMachine
{
    public interface ITransition
    {
        Type To { get; }
        bool CanTranslate(IState fromState);
    }
}