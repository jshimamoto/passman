using System.Reflection.Metadata;
using System.Text;
using Passman.Core.Crypto;

namespace Passman.Core.Storage;

public static class DatbaseFileService
{
    private static readonly byte[] Magic = Encoding.ASCII.GetBytes("PMDB");
    private const byte Version = 1;

    public static void SaveFile(string path, string masterPassword, byte[] plaintext)
    {
        byte[] salt = SaltGenerator.Generate();
        byte[] key = KeyDeriver.DeriveKey(masterPassword, salt);

        var (nonce, ciphertext, tag) = AesGcmService.Encrypt(plaintext, key);

        using var fs = File.Create(path);
        using var bw = new BinaryWriter(fs);

        bw.Write(Magic);
        bw.Write(Version);

        bw.Write((byte)salt.Length);
        bw.Write(salt);

        bw.Write(nonce);
        bw.Write(tag);
        bw.Write(ciphertext);
    }

    public static byte[] LoadFile(string path, string masterPassword)
    {
        using var fs = File.OpenRead(path);
        using var br = new BinaryReader(fs);

        var magic = br.ReadBytes(4);
        if (!magic.SequenceEqual(Magic))
            throw new Exception("Invalid database file");

        var version = br.ReadByte();
        if (version != Version)
            throw new Exception("Unsupported database version");

        int saltLen = br.ReadByte();
        byte[] salt = br.ReadBytes(saltLen);

        byte[] key = KeyDeriver.DeriveKey(masterPassword, salt);

        byte[] nonce = br.ReadBytes(AesGcmService.NonceSize);
        byte[] tag = br.ReadBytes(AesGcmService.TagSize);

        byte[] ciphertext = br.ReadBytes((int)(fs.Length - fs.Position));

        return AesGcmService.Decrypt(ciphertext, key, nonce, tag);
    }
}