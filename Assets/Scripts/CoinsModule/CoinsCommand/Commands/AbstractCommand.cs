using System.Threading;
using ColorBump.Manager.CoinsModule.CoinsCommand.Data;

namespace DefaultNamespace.Command.Commands
{
    public abstract class AbstractCommand : ICommand
    {
        protected readonly CancellationTokenSource _cancellationTokenSource = new();
        protected readonly PlayerSettings _settings;
        protected readonly CommandsData _commandsData;
        protected float _duration;
        protected Player _player;
        
        protected AbstractCommand(PlayerSettings settings, CommandsData commandsData, float duration = 10f)
        {
            _duration = duration;
            _settings = settings;
            _commandsData = commandsData;
        }

        public virtual void Execute(Player player)
        {
            _player = player;
        }

        public void Cancel() => _cancellationTokenSource.Cancel();
    }
}