using System;
using System.Windows.Input;

namespace Fasetto_Word;

public class RelayParameterizedCommand : ICommand
{
    private readonly Action<object?> _action;

    public bool CanExecute(object? parameter)
    {
        return true;
    }

    public void Execute(object? parameter)
    {
        _action.Invoke(parameter);
    }

    public RelayParameterizedCommand(Action<object?> action)
    {
        _action = action;
    }

    public event EventHandler? CanExecuteChanged;
}