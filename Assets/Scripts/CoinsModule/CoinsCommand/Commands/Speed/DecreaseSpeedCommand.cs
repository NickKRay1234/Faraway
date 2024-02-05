using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using DefaultNamespace.Command.Commands;
using UnityEngine;

public class DecreaseSpeedCommand : SpeedParentCommand
{
    public DecreaseSpeedCommand(CommandContext context) : base(context)
    {
         _startSpeed = _context.Settings.ForwardSpeed;
         _minSpeed = _context.Data.MinSpeed;
         _durationHalf = _context.Duration / DIVIDER;
    }

    protected async UniTask DecreaseSpeed(CancellationToken ct)
    {
        
        await ChangeSpeedOverTime(_startSpeed, _minSpeed, _durationHalf, ct);
       
        if (ct.IsCancellationRequested) 
            return;
        
        await UniTask.Delay(TimeSpan.FromSeconds(_durationHalf), cancellationToken: ct);
        await ChangeSpeedOverTime(_minSpeed, _context.Settings.StartSpeed, _durationHalf, ct);
    }

    public override void Execute(Player player)
    {
        base.Execute(player);
        try
        {
            DecreaseSpeed(_cancellationTokenSource.Token).Forget();
        }
        catch (Exception ex)
        {
#if UNITY_EDITOR
            Debug.LogError($"Error executing DecreaseSpeedCommand: {ex.Message}");
#endif
        }
    }
}