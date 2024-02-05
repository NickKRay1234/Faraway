using System.Collections.Generic;
using Factories;

namespace DefaultNamespace.Strategy
{
    /// A strategy base class that defines the common behaviour for all coin effect strategies.
    public abstract class AbstractStrategy : ICoinEffectStrategy
    {
        /// List of commands associated with each strategy.
        protected readonly List<ICommand> _commands;
        
        protected AbstractStrategy(IEnumerable<ICommand> commands) =>
            _commands = new List<ICommand>(commands);

        /// Apply the effect to the player by executing all associated commands.
        void ICoinEffectStrategy.ApplyEffect(Player player)
        {
            foreach (var command in _commands)
                command.Execute(player);
        }

        /// Undo effect, cancelling all associated commands in reverse order.
        void ICoinEffectStrategy.CancelEffect()
        {
            for (int i = _commands.Count - 1; i >= 0; i--)
                _commands[i].Cancel();
        }
    }
}