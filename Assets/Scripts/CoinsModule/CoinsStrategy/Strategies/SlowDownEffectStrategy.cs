using ColorBump.Manager.CoinsModule.CoinsCommand.Data;
using DefaultNamespace.Strategy;

namespace Factories
{
    public class SlowDownEffectStrategy : AbstractStrategy
    {
        public SlowDownEffectStrategy(PlayerSettings playerSettings, CommandsData data) =>
            _commands.AddRange(new ICommand[] { new DecreaseSpeedCommand(playerSettings,data, 10f) });
    }
}