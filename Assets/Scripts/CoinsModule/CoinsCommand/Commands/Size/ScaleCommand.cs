using System;
using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using DefaultNamespace.Command.Commands;
using UnityEngine;

/// Specific implementation of the command to change the player's scale.
public sealed class ScaleCommand : AbstractCommand
{
    public ScaleCommand(CommandContext context) : base(context)
    {
    }
    
    /// Asynchronous method for smoothly adjusting the player's scale.
    private async UniTask AdjustScale(CancellationToken ct)
    {
        await ChangeScaleSmoothly(_player.transform, _context.Data.MaxScale, _context.Duration / DIVIDER, ct);
        if (ct.IsCancellationRequested) return;
        await UniTask.Delay(TimeSpan.FromSeconds(_context.Duration / DIVIDER), cancellationToken: ct);
        await ChangeScaleSmoothly(_player.transform, _context.Data.NormalScale, _context.Duration / DIVIDER, ct);
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

    /// Override the Execute method to run the scale adjustment.
    public override async Task Execute(Player player)
    {
        if (player == null) throw new ArgumentNullException(nameof(player));
        Cancel();
        await AdjustScale(_cancellationTokenSource.Token);
    }
}