using System.Security.Cryptography;
using System.Text;

namespace WriterPlatformWeb.Helpers;

public class SHA256Manager
{
    public static string GenerateSaltedHash(string password, string salt)
    {
        var passwordBytes = Encoding.Unicode.GetBytes(password);
        var saltBytes = Encoding.Unicode.GetBytes(salt);

        var algorithm = SHA256.Create();

        var passwordWithSaltBytes = new byte[passwordBytes.Length + saltBytes.Length];
        passwordBytes.CopyTo(passwordWithSaltBytes, 0);
        saltBytes.CopyTo(passwordWithSaltBytes, passwordBytes.Length);

        return Convert.ToBase64String(algorithm.ComputeHash(passwordWithSaltBytes));
    }

    public static bool PasswordMath(string password, string salt, string hash)
    {
        return hash == GenerateSaltedHash(password, salt);
    }
}