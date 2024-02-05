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
        await ChangeSpeedOverTime(_startSpeed, _maxSpeed, _timeToReachMaxSpeed, ct);
        if (ct.IsCancellationRequested) return;

        // Ожидание перед возвращением скорости к начальному значению.
        await UniTask.Delay(TimeSpan.FromSeconds(_timeToReachMaxSpeed), cancellationToken: ct);
        
        // Плавное возвращение скорости к начальному значению.
        await ChangeSpeedOverTime(_maxSpeed, _startSpeed, _timeToReachMaxSpeed, ct);
    }

    /// Overriding the Execute method to trigger the speedup.
    public override async void Execute(Player player)
    {
        base.Execute(player);
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