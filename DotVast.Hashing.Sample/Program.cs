using DotVast.Hashing.NativeCrypto;

namespace DotVast.Hashing.Sample;

internal static class Extensions
{
    internal static void Print(this byte[] data)
    {
        Console.WriteLine(Convert.ToHexString(data));
    }
}

internal class Program
{
    static void Main(string[] args)
    {
        var data = new byte[1000];
        var random = new Random(9876);
        random.NextBytes(data);

        var md4 = new BLAKE3();
        md4.Append(data);
        md4.Finalize().Print();
    }
}
