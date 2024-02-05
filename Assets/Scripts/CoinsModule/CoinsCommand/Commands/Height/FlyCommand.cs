using System;
using Cysharp.Threading.Tasks;
using DefaultNamespace.Command.Commands;

/// Specific implementation of the command that allows the player to fly.
public sealed class FlyCommand : AbstractCommand
{
    private readonly IHeightAdjuster _heightAdjuster;

    public FlyCommand(CommandContext context, IHeightAdjuster heightAdjuster) : base(context) =>
        _heightAdjuster = heightAdjuster;

    /// Overriding the Execute method to enable flight.
    public override async void Execute(Player player)
    {
        if (player == null) throw new ArgumentNullException(nameof(player));

        base.Execute(player);
        player.Rigidbody.useGravity = false;

        try
        {
            // Smoothly raise the player to the maximum height.
            await _heightAdjuster.AdjustHeightSmoothly(_player, _context.Data.MaxHeight, _context.Duration, _cancellationTokenSource.Token);
            
            // Waiting before returning to normal altitude.
            await UniTask.Delay((int)(_context.Duration * 1000), cancellationToken: _cancellationTokenSource.Token);
            
            // Smoothly lower the player back to normal height.
            await _heightAdjuster.AdjustHeightSmoothly(_player, _context.Data.NormalHeight, _context.Duration, _cancellationTokenSource.Token);
        }
        finally
        {
            player.Rigidbody.useGravity = true;
        }
    }
}