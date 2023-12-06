using System.Windows;
using System.Windows.Controls;

namespace Fasetto_Word;

public class MonitorPasswordProperty : BaseAttachedProperty<MonitorPasswordProperty, bool>
{
    public override void OnValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
    {
        if (sender is not PasswordBox passwordBox)
            return;

        passwordBox.PasswordChanged -= PasswordBoxPasswordChanged;

        if ((bool)e.NewValue)
        {
            HasTextProperty.SetValue(passwordBox);
            passwordBox.PasswordChanged += PasswordBoxPasswordChanged;
        }

        void PasswordBoxPasswordChanged(object o, RoutedEventArgs args)
        {
            HasTextProperty.SetValue((PasswordBox)o);
        }
    }
}

public class HasTextProperty : BaseAttachedProperty<HasTextProperty, bool>
{
    public static void SetValue(DependencyObject sender)
    {
        SetValue(sender, ((PasswordBox)sender).SecurePassword.Length > 0);
    }

    public override void OnValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
    {
    }
}