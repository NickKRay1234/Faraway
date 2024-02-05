using System;
using System.Threading;

namespace DefaultNamespace.Command.Commands
{
    public abstract class AbstractCommand : ICommand, IDisposable
    {
        /// Cancellation token source to control asynchronous operations.
        protected CancellationTokenSource _cancellationTokenSource = new();
        
        /// Command context containing the settings and data required for execution.
        protected readonly CommandContext _context;
        
        /// The player on whom the command is executed.
        protected Player _player;
        
        protected const int DIVIDER = 2;

        protected AbstractCommand(CommandContext context) => 
            _context = context ?? throw new ArgumentNullException(nameof(context));
        
        public virtual void Execute(Player player) =>
            _player = player ?? throw new ArgumentNullException(nameof(player));

        
        /// Cancels the current asynchronous operation and recreates the source of the cancellation token.
        public void Cancel()
        {
            _cancellationTokenSource.Cancel();
            _cancellationTokenSource.Dispose();
            _cancellationTokenSource = new CancellationTokenSource();
        }

        /// Releases the resources used by the source of the cancellation token.
        public void Dispose()
        {
            _cancellationTokenSource.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}