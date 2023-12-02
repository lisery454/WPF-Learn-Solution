using System.Security;

namespace Fasetto_Word;

public partial class LoginPage : BasePage<LoginPageViewModel>, IHavePassword
{
    public LoginPage()
    {
        InitializeComponent();
    }

    public SecureString SecurePassword => PasswordBox.SecurePassword;
}