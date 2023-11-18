using System;
using System.Windows.Input;

namespace Demo1.Directory.ViewModels.Base;

public class RelayCommand : ICommand
{
    private readonly Action _action;

    public bool CanExecute(object? parameter)
    {
        return true;
    }

    public void Execute(object? parameter)
    {
        _action.Invoke();
    }

    public RelayCommand(Action action)
    {
        _action = action;
    }

    public event EventHandler? CanExecuteChanged;
}