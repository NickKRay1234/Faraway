using System;
using System.Threading;
using ColorBump.Manager.CoinsModule.CoinsCommand.Data;
using Cysharp.Threading.Tasks;
using DefaultNamespace.Command.Commands;
using UnityEngine;

public class IncreaseSpeedCommand : AbstractCommand
{
    public IncreaseSpeedCommand(PlayerSettings settings, CommandsData data, float duration = 10f) : base(settings, data, duration)
    {
    }

    protected async UniTask SpeedUp(CancellationToken ct)
    {
        float startSpeed = _settings.StartSpeed;
        float maxSpeed = _commandsData.MaxSpeed;
        float timeToReachMaxSpeed = _duration / 2;
        
        await ChangeSpeedOverTime(startSpeed, maxSpeed, timeToReachMaxSpeed, ct);
        if (ct.IsCancellationRequested) return;
        
        await UniTask.Delay(TimeSpan.FromSeconds(timeToReachMaxSpeed), cancellationToken: ct);
        await ChangeSpeedOverTime(maxSpeed, startSpeed, timeToReachMaxSpeed, ct);
    }

    private async UniTask ChangeSpeedOverTime(float fromSpeed, float toSpeed, float duration, CancellationToken ct)
    {
        float elapsedTime = 0;

        while (elapsedTime < duration)
        {
            if (ct.IsCancellationRequested) break;

            elapsedTime += Time.deltaTime;
            float progress = elapsedTime / duration;
            _settings.ForwardSpeed = Mathf.Lerp(fromSpeed, toSpeed, progress);
            await UniTask.Yield(PlayerLoopTiming.Update, ct);
        }

        _settings.ForwardSpeed = toSpeed;
    }

    public override void Execute(Player player)
    {
        base.Execute(player);
        SpeedUp(_cancellationTokenSource.Token).Forget();
    }
}