using System.Security;
using Fasetto_Word.ViewModels;
using Fasetto_Word.ViewModels.Base;

namespace Fasetto_Word.Pages;

public partial class LoginPage : BasePage<LoginPageViewModel>, IHavePassword
{
    public LoginPage()
    {
        InitializeComponent();
    }

    public SecureString SecurePassword => PasswordBox.SecurePassword;
}