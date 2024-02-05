using DefaultNamespace.Strategy;

namespace Strategies
{
    public class SpeedUpEffectStrategy : AbstractStrategy
    {
        public SpeedUpEffectStrategy(CommandContext context)
            : base(new ICommand[] { new IncreaseSpeedCommand(context) }) { }
    }
}