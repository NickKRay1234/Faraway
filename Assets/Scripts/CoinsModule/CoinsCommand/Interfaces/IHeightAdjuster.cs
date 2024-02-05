using System.Threading;
using System.Threading.Tasks;

public interface IHeightAdjuster
{
    Task AdjustHeightSmoothly(Player player, float targetHeight, float duration, CancellationToken ct);
}