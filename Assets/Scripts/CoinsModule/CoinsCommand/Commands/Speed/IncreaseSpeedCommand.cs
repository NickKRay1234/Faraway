using System;
using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using DefaultNamespace.Command.Commands;
using UnityEngine;

/// Specific implementation of the command to increase the player's speed.
public class IncreaseSpeedCommand : SpeedParentCommand
{
    public IncreaseSpeedCommand(CommandContext context) : base(context)
    {
        _startSpeed = _context.Settings.StartSpeed;
        _maxSpeed = _context.Data.MaxSpeed;
        _timeToReachMaxSpeed = _context.Duration / DIVIDER;
    }

    /// Asynchronous method for smooth speed increase.
    protected async Task SpeedUp(CancellationToken ct)
    {
        // Smoothly increase the speed to the maximum value.
        ChangeSpeedOverTime(_startSpeed, _maxSpeed, _timeToReachMaxSpeed, ct);
        if (ct.IsCancellationRequested) return;
        
        await UniTask.Delay(TimeSpan.FromSeconds(_timeToReachMaxSpeed), cancellationToken: ct);
        if (ct.IsCancellationRequested) return;
        
        ChangeSpeedOverTime(_maxSpeed, _startSpeed, _timeToReachMaxSpeed, ct);
    }

    /// Overriding the Execute method to trigger the speedup.
    public override async Task Execute(Player player)
    {
        ResetCancellationToken();
        _player = player ?? throw new ArgumentNullException(nameof(player));
        try
        {
            await SpeedUp(_cancellationTokenSource.Token);
        }
        catch (Exception ex)
        {
#if UNITY_EDITOR
            Debug.LogError($"Error executing IncreaseSpeedCommand: {ex.Message}");
#endif
        }
    }
}