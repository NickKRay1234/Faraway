using System;
using System.Threading;
using ColorBump.Manager.CoinsModule.CoinsCommand.Data;
using Cysharp.Threading.Tasks;
using DefaultNamespace.Command.Commands;
using UnityEngine;

public class FlyCommand : AbstractCommand
{
    public FlyCommand(PlayerSettings settings, CommandsData data, float duration = 10f) : base(settings, data, duration)
    {
    }

    protected async UniTask ChangeHeight(CancellationToken ct)
    {
        try
        {
            _player.Rigidbody.useGravity = false;
            await AdjustHeightSmoothly(_player.transform.position.y, _commandsData.MaxHeight, ct);
            await UniTask.Delay((int)(_duration * 1000), cancellationToken: ct);
            await AdjustHeightSmoothly(_player.transform.position.y, _commandsData.NormalHeight, ct);
        }
        catch (OperationCanceledException)
        {
            _player.Rigidbody.useGravity = true;
        }
    }

    private async UniTask AdjustHeightSmoothly(float startHeight, float endHeight, CancellationToken ct)
    {
        float elapsedTime = 0;
        float duration = _duration / 8;

        while (elapsedTime < duration)
        {
            if (ct.IsCancellationRequested) break;

            elapsedTime += Time.deltaTime;
            float progress = elapsedTime / duration;
            float newY = Mathf.Lerp(startHeight, endHeight, progress);
            _player.transform.position = new Vector3(_player.transform.position.x, newY, _player.transform.position.z); 

            await UniTask.Yield(PlayerLoopTiming.Update, ct);
        }
    }

    public override void Execute(Player player)
    {
        base.Execute(player);
        ChangeHeight(_cancellationTokenSource.Token).Forget();
    }
}