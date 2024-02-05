﻿using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using DefaultNamespace.Command.Commands;
using UnityEngine;

public sealed class ScaleCommand : AbstractCommand
{
    public ScaleCommand(CommandContext context) : base(context)
    {
    }

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

    public override void Execute(Player player)
    {
        base.Execute(player);
        AdjustScale(_cancellationTokenSource.Token).Forget();
    }
}