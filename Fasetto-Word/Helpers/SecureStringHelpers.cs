using System;
using System.Runtime.InteropServices;
using System.Security;

namespace Fasetto_Word.Helpers;

public static class SecureStringHelpers
{
    public static string? Unsecure(this SecureString secureString)
    {
        var unmanagedString = IntPtr.Zero;

        try
        {
            unmanagedString = Marshal.SecureStringToGlobalAllocUnicode(secureString);
            return Marshal.PtrToStringUni(unmanagedString);
        }
        finally
        {
            Marshal.ZeroFreeGlobalAllocUnicode(unmanagedString);
        }
    }
}