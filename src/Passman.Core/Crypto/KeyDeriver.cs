using System.Security.Cryptography;

namespace Passman.Core.Crypto;

public static class KeyDeriver
{
    public static readonly int KeySize = 32;

    // The number of times something is hashed to get to the result
    // An attacker would have to do the same computation for each guess
    // This only slows down computation and makes each guess more expensive
    private const int Iterations = 100_000; 

    public static byte[] DeriveKey(string password, byte[] salt)
    {
        // Password-Based Key Derivation Function 2
        using var pbkdf2 = new Rfc2898DeriveBytes(
            password: password,
            salt: salt,
            iterations: Iterations,
            hashAlgorithm: HashAlgorithmName.SHA256
        );

        return pbkdf2.GetBytes(KeySize);
    }
}