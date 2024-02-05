using System;
using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using DefaultNamespace.Command.Commands;
using UnityEngine;

public class IncreaseSpeedCommand : SpeedParentCommand
{
    public IncreaseSpeedCommand(CommandContext context) : base(context)
    {
        _startSpeed = _context.Settings.StartSpeed;
        _maxSpeed = _context.Data.MaxSpeed;
        _timeToReachMaxSpeed = _context.Duration / DIVIDER;
    }

    protected async Task SpeedUp(CancellationToken ct)
    {
        await ChangeSpeedOverTime(_startSpeed, _maxSpeed, _timeToReachMaxSpeed, ct);
        if (ct.IsCancellationRequested) return;

        await UniTask.Delay(TimeSpan.FromSeconds(_timeToReachMaxSpeed), cancellationToken: ct);
        await ChangeSpeedOverTime(_maxSpeed, _startSpeed, _timeToReachMaxSpeed, ct);
    }

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