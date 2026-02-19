using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;

namespace Passman.Core.Crypto;

public static class PasswordGenerator
{
    private const string Lower = "abcdefghijklmnopqrstuvwxyz";
    private const string Upper = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    private const string Digits = "0123456789";
    private const string Symbols = "!@#$%^&*()-_=+[]{}<>?";
    private const int Length = 24;

    public static string Generate()
    {
        var chars = new List<char>();
        chars.Add(GetRandomChar(Lower));
        chars.Add(GetRandomChar(Upper));
        chars.Add(GetRandomChar(Digits));
        chars.Add(GetRandomChar(Symbols));

        var pool = Lower + Upper + Digits + Symbols;

        while (chars.Count < Length)
        {
            chars.Add(GetRandomChar(pool));
        }

        Shuffle(chars);
        
        return new string(chars.ToArray());
    }

    private static char GetRandomChar(string set)
    {
        int index = RandomNumberGenerator.GetInt32(set.Length);
        return set[index];
    }

    private static void Shuffle(List<char> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int j = RandomNumberGenerator.GetInt32(i + 1);
            (list[i], list[j]) = (list[j], list[i]);
        }
    }
}