using System;
using System.Threading;
using ColorBump.Manager.CoinsModule.CoinsCommand.Data;
using Cysharp.Threading.Tasks;
using DefaultNamespace.Command.Commands;
using UnityEngine;

public sealed class ScaleCommand : AbstractCommand
{
    public ScaleCommand(PlayerSettings settings, CommandsData data, float duration = 10f) : base(settings, data,
        duration) => _duration = duration;

    private async UniTask AdjustScale(CancellationToken ct)
    {
        await ChangeScaleSmoothly(_player.transform, _commandsData.MaxScale, _duration / 2, ct);
        if (ct.IsCancellationRequested) return;
        await UniTask.Delay(TimeSpan.FromSeconds(_duration / 2), cancellationToken: ct); 
        await ChangeScaleSmoothly(_player.transform, _commandsData.NormalScale, _duration / 2, ct);
    }
    
    private async UniTask ChangeScaleSmoothly(Transform target, Vector3 newScale, float duration, CancellationToken ct)
    {
        Vector3 startScale = target.localScale;
        float elapsedTime = 0;

        while (elapsedTime < duration)
        {
            if (ct.IsCancellationRequested) break;

            elapsedTime += Time.deltaTime;
            float progress = elapsedTime / duration;
            target.localScale = Vector3.Lerp(startScale, newScale, progress);
            await UniTask.Yield(PlayerLoopTiming.Update, ct);
        }

        if (ct.IsCancellationRequested) return;
        target.localScale = newScale;
    }

    public override void Execute(Player player)
    {
        base.Execute(player);
        AdjustScale(_cancellationTokenSource.Token).Forget();
    }
}