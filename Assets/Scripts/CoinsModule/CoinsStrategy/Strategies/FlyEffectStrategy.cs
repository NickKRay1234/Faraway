using ColorBump.Manager.CoinsModule.CoinsCommand.Data;
using DefaultNamespace.Strategy;

public sealed class FlyEffectStrategy : AbstractStrategy
{
    public FlyEffectStrategy(PlayerSettings playerSettings, CommandsData data)=>
        _commands.AddRange(new ICommand[] { new FlyCommand(playerSettings, data,10) });
}
