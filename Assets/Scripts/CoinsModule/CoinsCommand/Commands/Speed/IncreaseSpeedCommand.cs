using System;
using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using DefaultNamespace.Command.Commands;
using UnityEngine;

public class IncreaseSpeedCommand : AbstractCommand
{
    public IncreaseSpeedCommand(CommandContext context) : base(context) { }

    protected async Task SpeedUp(CancellationToken ct)
    {
        float startSpeed = _context.Settings.StartSpeed;
        float maxSpeed = _context.Data.MaxSpeed;
        float timeToReachMaxSpeed = _context.Duration / 2;

        await ChangeSpeedOverTime(startSpeed, maxSpeed, timeToReachMaxSpeed, ct);
        if (ct.IsCancellationRequested) return;

        await UniTask.Delay(TimeSpan.FromSeconds(timeToReachMaxSpeed), cancellationToken: ct);
        await ChangeSpeedOverTime(maxSpeed, startSpeed, timeToReachMaxSpeed, ct);
    }

    private async Task ChangeSpeedOverTime(float fromSpeed, float toSpeed, float duration, CancellationToken ct)
    {
        float elapsedTime = 0;

        while (elapsedTime < duration)
        {
            if (ct.IsCancellationRequested) 
                break;

            elapsedTime += Time.deltaTime;
            float progress = elapsedTime / duration;
            _context.Settings.ForwardSpeed = Mathf.Lerp(fromSpeed, toSpeed, progress);
            await UniTask.Yield(PlayerLoopTiming.Update, ct);
        }

        if (!ct.IsCancellationRequested)
            _context.Settings.ForwardSpeed = toSpeed;
    }

    public override async void Execute(Player player)
    {
        base.Execute(player);
        try
        {
            await SpeedUp(_cancellationTokenSource.Token);
        }
        catch (Exception ex)
        {
#if UNITY_EDITOR
            Debug.LogError($"Error executing IncreaseSpeedCommand: {ex.Message}");
#endif
        }
    }
}