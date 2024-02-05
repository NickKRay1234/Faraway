using System;
using System.Threading;
using ColorBump.Manager.CoinsModule.CoinsCommand.Data;
using Cysharp.Threading.Tasks;
using DefaultNamespace.Command.Commands;
using UnityEngine;

public class DecreaseSpeedCommand : AbstractCommand
{
    public DecreaseSpeedCommand(PlayerSettings settings, CommandsData data, float duration = 10f) : base(settings, data,
        duration)
    {
    }

    protected async UniTask DecreaseSpeed(CancellationToken ct)
    {
        float startSpeed = _settings.ForwardSpeed;
        float minSpeed = _commandsData.MinSpeed;
        float durationHalf = _duration / 2;
        
        await ChangeSpeedOverTime(startSpeed, minSpeed, durationHalf, ct);
        if (ct.IsCancellationRequested) return;
        
        await UniTask.Delay(TimeSpan.FromSeconds(durationHalf), cancellationToken: ct);
        
        await ChangeSpeedOverTime(minSpeed, _settings.StartSpeed, durationHalf, ct);
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
        DecreaseSpeed(_cancellationTokenSource.Token).Forget();
    }
}