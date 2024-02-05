using System;
using System.Threading;
using System.Threading.Tasks;
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
        ChangeSpeedOverTime(_startSpeed, _minSpeed, _durationHalf, ct);
        if (ct.IsCancellationRequested) return;
        // Waiting before restoring the initial speed.
        await UniTask.Delay(TimeSpan.FromSeconds(_durationHalf), cancellationToken: ct);
        if (ct.IsCancellationRequested) return;
        // Smoothly restore the speed to the initial value.
        ChangeSpeedOverTime(_minSpeed, _context.Settings.StartSpeed, _durationHalf, ct);
    }

    /// Overriding the Execute method to trigger speed reduction.
    public override async Task Execute(Player player)
    {
        ResetCancellationToken();
        _player = player ?? throw new ArgumentNullException(nameof(player));
        try
        {
            await DecreaseSpeed(_cancellationTokenSource.Token);
        }
        catch (Exception ex)
        {
#if UNITY_EDITOR
            Debug.LogError($"Error executing DecreaseSpeedCommand: {ex.Message}");
#endif
        }
    }
}