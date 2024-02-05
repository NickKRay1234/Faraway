using System;
using System.Threading;

namespace Factories
{
    public abstract class AbstractCommand : ICommand, IDisposable
    {
        protected CancellationTokenSource _cancellationTokenSource = new();
        protected readonly CommandContext _context;
        protected Player _player;
        
        protected const int DIVIDER = 2;

        protected AbstractCommand(CommandContext context) => 
            _context = context ?? throw new ArgumentNullException(nameof(context));
        
        public virtual void Execute(Player player) =>
            _player = player ?? throw new ArgumentNullException(nameof(player));

        public void Cancel()
        {
            _cancellationTokenSource.Cancel();
            _cancellationTokenSource.Dispose();
            _cancellationTokenSource = new CancellationTokenSource();
        }

        public void Dispose()
        {
            _cancellationTokenSource.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}