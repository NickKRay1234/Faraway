using System.Collections.Generic;
using UnityEngine;

// Возможно в будущем будет монетка которая заставляет делать несколько движений назад. 
// Пока что не реализовано. Но смысл надеюсь понятен. 

[HelpURL("https://unity.com/how-to/use-command-pattern-flexible-and-extensible-game-systems")]
public sealed class CommandInvoker
{
    private Stack<ICommand> _commandsHistory = new();
    private ICommand _currentCommand;

    public void ExecuteCommand(ICommand newCommand, Player player)
    {
        _currentCommand?.Cancel();

        _currentCommand = newCommand;
        //_currentCommand.Execute(player);

        _commandsHistory.Push(newCommand);
    }

    public void CancelCurrentCommand()
    {
        _currentCommand?.Cancel();
        _currentCommand = null;
    }

    public ICommand GetLastCommand() =>
        _commandsHistory.Count > 0 ? _commandsHistory.Peek() : null;

    public IEnumerable<ICommand> GetCommandsHistory() =>
        _commandsHistory;
}