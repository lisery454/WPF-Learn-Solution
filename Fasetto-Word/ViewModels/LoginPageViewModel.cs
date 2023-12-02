using System;
using System.Security;
using System.Threading.Tasks;
using System.Windows.Input;
using Fasetto_Word.Helpers;
using Fasetto_Word.ViewModels.Base;

namespace Fasetto_Word.ViewModels;

public class LoginPageViewModel : BaseViewModel
{
    public string Email { get; set; }

    public ICommand LoginCommand { get; set; }

    public bool IsLoginRunning { get; set; }

    public LoginPageViewModel()
    {
        Email = string.Empty;
        LoginCommand = new RelayParameterizedCommand(Login);
    }

    private async void Login(object? o)
    {
        await RunCommand(() => IsLoginRunning, async () =>
        {
            await Task.Delay(5000);
            var email = Email;
            var password = (o as IHavePassword)?.SecurePassword.Unsecure();
        });
    }
}