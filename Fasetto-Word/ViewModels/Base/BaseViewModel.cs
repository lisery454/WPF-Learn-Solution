using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Fasetto_Word.Helpers;

namespace Fasetto_Word.ViewModels.Base;

public class BaseViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }

    protected async Task RunCommand(Expression<Func<bool>> updatingFlag, Func<Task> action)
    {
        if (updatingFlag.GetPropertyValue()) return;

        updatingFlag.SetPropertyValue(true);


        try
        {
            await action();
        }
        finally
        {
            updatingFlag.SetPropertyValue(false);
        }
    }
}