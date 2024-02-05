using System.Collections.Generic;
using Factories;

namespace DefaultNamespace.Strategy
{
    public abstract class AbstractStrategy : ICoinEffectStrategy
    {
        protected readonly List<ICommand> _commands = new();

        void ICoinEffectStrategy.ApplyEffect(Player player)
        {
            foreach (var command in _commands)
                command.Execute(player);
        }

        void ICoinEffectStrategy.CancelEffect()
        {
            for (int i = _commands.Count - 1; i >= 0; i--)
                _commands[i].Cancel();
        }
    }
}