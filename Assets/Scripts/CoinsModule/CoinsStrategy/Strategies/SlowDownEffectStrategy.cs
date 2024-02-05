using DefaultNamespace.Strategy;

namespace Strategies
{
    public sealed class SlowDownEffectStrategy : AbstractStrategy
    {
        public SlowDownEffectStrategy(CommandContext context)
            : base(new ICommand[] { new DecreaseSpeedCommand(context) }) { }
    }
}