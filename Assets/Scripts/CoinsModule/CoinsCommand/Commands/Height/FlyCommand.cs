using System;
using Cysharp.Threading.Tasks;
using DefaultNamespace.Command.Commands;

public sealed class FlyCommand : AbstractCommand
{
    private readonly IHeightAdjuster _heightAdjuster;

    public FlyCommand(CommandContext context, IHeightAdjuster heightAdjuster) : base(context) =>
        _heightAdjuster = heightAdjuster;

    public override async void Execute(Player player)
    {
        if (player == null) throw new ArgumentNullException(nameof(player));

        base.Execute(player);
        player.Rigidbody.useGravity = false;

        try
        {
            await _heightAdjuster.AdjustHeightSmoothly(_player, _context.Data.MaxHeight, _context.Duration, _cancellationTokenSource.Token);
            await UniTask.Delay((int)(_context.Duration * 1000), cancellationToken: _cancellationTokenSource.Token);
            await _heightAdjuster.AdjustHeightSmoothly(_player, _context.Data.NormalHeight, _context.Duration, _cancellationTokenSource.Token);
        }
        finally
        {
            player.Rigidbody.useGravity = true;
        }
    }
}