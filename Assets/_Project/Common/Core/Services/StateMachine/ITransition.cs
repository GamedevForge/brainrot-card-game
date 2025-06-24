using System;

namespace Project.Core.Services.StateMachine
{
    public interface ITransition
    {
        Type To { get; }
        bool CanTranslate(IState fromState);
    }
}