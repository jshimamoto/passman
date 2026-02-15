using System.Security.Cryptography;

namespace Passman.Core.Crypto;

public static class SaltGenerator
{
    public const int SaltSize = 16;

    public static byte[] Generate()
    {
        byte[] salt = new byte[SaltSize];
        RandomNumberGenerator.Fill(salt);
        return salt;
    }
}