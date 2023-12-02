using System.Security;

namespace Fasetto_Word;

public interface IHavePassword
{
    public SecureString SecurePassword { get; }
}