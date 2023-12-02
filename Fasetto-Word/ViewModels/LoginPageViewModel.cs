using System;
using System.Security;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Fasetto_Word;

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