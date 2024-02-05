using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using DefaultNamespace.Command.Commands;
using UnityEngine;

public class DecreaseSpeedCommand : AbstractCommand
{
    public DecreaseSpeedCommand(CommandContext context) : base(context)
    {
    }

    protected async UniTask DecreaseSpeed(CancellationToken ct)
    {
        float startSpeed = _context.Settings.ForwardSpeed;
        float minSpeed = _context.Data.MinSpeed;
        float durationHalf = _context.Duration / 2;
        
        await ChangeSpeedOverTime(startSpeed, minSpeed, durationHalf, ct);
        if (ct.IsCancellationRequested) return;
        
        await UniTask.Delay(TimeSpan.FromSeconds(durationHalf), cancellationToken: ct);
        
        await ChangeSpeedOverTime(minSpeed, _context.Settings.StartSpeed, durationHalf, ct);
    }

    private async UniTask ChangeSpeedOverTime(float fromSpeed, float toSpeed, float duration, CancellationToken ct)
    {
        float elapsedTime = 0;

        while (elapsedTime < duration)
        {
            if (ct.IsCancellationRequested) break;

            elapsedTime += Time.deltaTime;
            float progress = elapsedTime / duration;
            _context.Settings.ForwardSpeed = Mathf.Lerp(fromSpeed, toSpeed, progress);
            await UniTask.Yield(PlayerLoopTiming.Update, ct);
        }

        _context.Settings.ForwardSpeed = toSpeed;
    }

    public override void Execute(Player player)
    {
        base.Execute(player);
        DecreaseSpeed(_cancellationTokenSource.Token).Forget();
    }
}