using System.Security.Cryptography;

namespace Passman.Core.Crypto;

public static class AesGcmService
{
    // Initialization Vector - random number used once for each encryption operation
    public const int NonceSize = 12;

    // Small piece of data (usually 16 bytes) that proves: 
    // The ciphertext hasn’t been modified and the correct key was used
    // This prevents someone from altering a text and saving that as your new data
    public const int TagSize = 16;

    public static (byte[] nonce, byte[] ciphertext, byte[] tag) Encrypt(
        byte[] plaintext, 
        byte[] key
    )
    {
        byte[] nonce = new byte[NonceSize];
        RandomNumberGenerator.Fill(nonce);

        byte[] ciphertext = new byte[plaintext.Length];
        byte[] tag = new byte[TagSize];

        using var aes = new AesGcm(key, tagSizeInBytes: TagSize);
        aes.Encrypt(nonce, plaintext, ciphertext, tag);

        return (nonce, ciphertext, tag);
    }

    public static byte[] Decrypt(byte[] ciphertext, byte[] key, byte[] nonce, byte[] tag)
    {
        byte[] plaintext = new byte[ciphertext.Length];
        using var aes = new AesGcm(key, tagSizeInBytes: TagSize);
        aes.Decrypt(nonce, ciphertext, tag, plaintext);
        return plaintext;
    }
}