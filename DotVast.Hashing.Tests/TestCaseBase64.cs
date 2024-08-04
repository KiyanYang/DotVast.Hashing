namespace DotVast.Hashing.Tests;

public class TestCaseBase64(string input, string output) : TestCase(input, output)
{
    public override byte[] FromHashString(string hash) => Convert.FromBase64String(hash);

    public override string ToHashString(ReadOnlySpan<byte> bytes) => Convert.ToBase64String(bytes);
}
