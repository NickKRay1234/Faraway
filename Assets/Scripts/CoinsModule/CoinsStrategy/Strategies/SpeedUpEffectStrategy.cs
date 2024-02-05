using ColorBump.Manager.CoinsModule.CoinsCommand.Data;
using DefaultNamespace.Strategy;

public class SpeedUpEffectStrategy : AbstractStrategy
{
    public SpeedUpEffectStrategy(PlayerSettings playerSettings, CommandsData data) =>
        _commands.AddRange(new ICommand[] { 
            new IncreaseSpeedCommand(playerSettings, data,10)});
}