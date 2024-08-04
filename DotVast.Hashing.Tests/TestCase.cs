namespace DotVast.Hashing.Tests;

public class TestCase
{
    private readonly byte[] _input;
    private readonly byte[] _output;

    public ReadOnlySpan<byte> Input => _input;
    public ReadOnlySpan<byte> Output => _output;

    private string? _outputHashString;
    public string OutputHashString => _outputHashString ??= ToHashString(_output);

    public TestCase(byte[] input, byte[] output)
    {
        _input = input;
        _output = output;
    }

    public TestCase(byte[] input, string outputHashString)
    {
        _input = input;
        _output = FromHashString(outputHashString);
    }

    public TestCase(string input, byte[] output) : this(Convert.FromHexString(input), output) { }

    public TestCase(string input, string output) : this(Convert.FromHexString(input), output) { }

    public void VerifyResult(ReadOnlySpan<byte> result)
    {
        Assert.Equal(OutputHashString, ToHashString(result));
    }

    /// <summary>
    /// 将字节序列形式的哈希值变为字符串形式。默认采用 HexUpper 格式。
    /// </summary>
    /// <param name="bytes">哈希值（字节序列形式）。</param>
    /// <returns>哈希值（字符串形式）。</returns>
    public virtual string ToHashString(ReadOnlySpan<byte> bytes) => Convert.ToHexString(bytes);

    /// <summary>
    /// 将字符串形式的哈希值变为字节序列形式。默认采用 HexUpper 格式化。
    /// </summary>
    /// <param name="hash">哈希值（字符串形式）。</param>
    /// <returns>哈希值（字节序列形式）。</returns>
    public virtual byte[] FromHashString(string hash) => Convert.FromHexString(hash);
}
