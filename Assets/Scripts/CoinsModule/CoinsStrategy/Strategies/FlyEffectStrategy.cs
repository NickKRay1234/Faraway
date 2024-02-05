using DefaultNamespace.Strategy;

namespace Strategies
{
    public sealed class FlyEffectStrategy : AbstractStrategy
    {
        public FlyEffectStrategy(CommandContext context)
            : base(new ICommand[] { new FlyCommand(context, new HeightAdjuster()) }) { }
    }
}