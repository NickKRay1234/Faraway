using ColorBump.Manager.CoinsModule.CoinsCommand.Data;

public class CommandContext
{
    public PlayerSettings Settings { get; }
    public CommandsData Data { get; }
    public float Duration { get; }

    public CommandContext(PlayerSettings settings, CommandsData data, float duration)
    {
        Settings = settings;
        Data = data;
        Duration = duration;
    }
}