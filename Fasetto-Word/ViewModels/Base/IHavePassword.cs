using System.Security;

namespace Fasetto_Word.ViewModels.Base;

public interface IHavePassword
{
    public SecureString SecurePassword { get; }
}