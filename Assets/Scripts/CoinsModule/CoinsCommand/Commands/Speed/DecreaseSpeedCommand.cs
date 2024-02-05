using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using DefaultNamespace.Command.Commands;
using UnityEngine;

/// Specific implementation of the command to reduce the player's speed.
public class DecreaseSpeedCommand : SpeedParentCommand
{
    public DecreaseSpeedCommand(CommandContext context) : base(context)
    {
         _startSpeed = _context.Settings.ForwardSpeed;
         _minSpeed = _context.Data.MinSpeed;
         _durationHalf = _context.Duration / DIVIDER;
    }

    /// Asynchronous method for smooth speed reduction.
    protected async UniTask DecreaseSpeed(CancellationToken ct)
    {
        // Smoothly reduce the speed to the minimum value.
        await ChangeSpeedOverTime(_startSpeed, _minSpeed, _durationHalf, ct);
       
        if (ct.IsCancellationRequested) 
            return;
        
        // Waiting before restoring the initial speed.
        await UniTask.Delay(TimeSpan.FromSeconds(_durationHalf), cancellationToken: ct);
        
        // Smoothly restore the speed to the initial value.
        await ChangeSpeedOverTime(_minSpeed, _context.Settings.StartSpeed, _durationHalf, ct);
    }

    /// Overriding the Execute method to trigger speed reduction.
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