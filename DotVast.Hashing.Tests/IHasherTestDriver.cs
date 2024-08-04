namespace DotVast.Hashing.Tests;

public abstract class IHasherTestDriver(IHasherTestDriver.TestCase emptyInputTestCase)
{
    private readonly int _hashLengthInBytes = emptyInputTestCase.Output.Length;
    private readonly TestCase _emptyInputTestCase = emptyInputTestCase;

    protected abstract IHasher CreateInstance();

    [Fact]
    public void VerifyHashLengthInBytes()
    {
        var hasher = CreateInstance();
        Assert.Equal(_hashLengthInBytes, hasher.HashLengthInBytes);
        Assert.Equal(_hashLengthInBytes, hasher.Finalize().Length);
    }

    protected void InstanceAppendDriver(TestCase testCase)
    {
        var hasher = CreateInstance();
        hasher.Append(testCase.Input);
        var output = hasher.Finalize();

        testCase.VerifyResult(output);
    }

    protected void InstanceAppendAndResetDriver(TestCase testCase)
    {
        var hasher = CreateInstance();
        hasher.Append(testCase.Input);
        var output = hasher.FinalizeAndReset();

        testCase.VerifyResult(output);
    }

    protected void InstanceMultiAppendDriver(TestCase testCase)
    {
        var source = testCase.Input;
        int div3 = source.Length / 3;
        var hasher = CreateInstance();
        hasher.Append(source[..div3]);
        source = source[div3..];
        hasher.Append(source[..div3]);
        source = source[div3..];
        hasher.Append(source);
        var output = hasher.Finalize();

        testCase.VerifyResult(output);
    }

    [Fact]
    protected void InstanceVerifyEmpty()
    {
        var hasher = CreateInstance();
        var output = hasher.Finalize();
        VerifyEmptyResult(output);

        hasher.Reset();
        output = hasher.Finalize();
        VerifyEmptyResult(output);

        hasher.Reset();
        output = hasher.FinalizeAndReset();
        VerifyEmptyResult(output);

        output = hasher.FinalizeAndReset();
        VerifyEmptyResult(output);

        hasher.Append([]);
        output = hasher.FinalizeAndReset();
        VerifyEmptyResult(output);
    }

    protected void InstanceVerifyResetStateDriver(TestCase testCase)
    {
        var hasher = CreateInstance();

        hasher.Append(testCase.Input);
        hasher.Reset();
        var output = hasher.Finalize();
        VerifyEmptyResult(output);

        hasher.Reset();
        output = hasher.Finalize();
        VerifyEmptyResult(output);

        hasher.Reset();
        hasher.Append(testCase.Input);
        output = hasher.FinalizeAndReset();
        testCase.VerifyResult(output);

        output = hasher.Finalize();
        VerifyEmptyResult(output);
    }

    private void VerifyEmptyResult(ReadOnlySpan<byte> result)
    {
        Assert.Equal(_emptyInputTestCase.OutputHashString, _emptyInputTestCase.ToHashString(result));
    }

    public class TestCase
    {
        private readonly byte[] _input;
        private readonly byte[] _output;

        public ReadOnlySpan<byte> Input => _input;
        public ReadOnlySpan<byte> Output => _output;

        public string OutputHashString { get; }

        public TestCase(byte[] input, byte[] output)
        {
            _input = input;
            _output = output;
            OutputHashString = ToHashString(output);
        }

        public TestCase(byte[] input, string outputHashString)
        {
            _input = input;
            _output = FromHashString(outputHashString);
            OutputHashString = outputHashString;
        }

        public TestCase(string input, byte[] output) : this(Convert.FromHexString(input), output) { }

        public TestCase(string input, string output) : this(Convert.FromHexString(input), output) { }

        public void VerifyResult(ReadOnlySpan<byte> result)
        {
            Assert.Equal(OutputHashString, ToHashString(result));
        }

        /// <summary>
        /// ���ֽ�������ʽ�Ĺ�ϣֵ��Ϊ�ַ�����ʽ��Ĭ�ϲ��� HexUpper ��ʽ��
        /// </summary>
        /// <param name="bytes">��ϣֵ���ֽ�������ʽ����</param>
        /// <returns>��ϣֵ���ַ�����ʽ����</returns>
        public virtual string ToHashString(ReadOnlySpan<byte> bytes) => Convert.ToHexString(bytes);

        /// <summary>
        /// ���ַ�����ʽ�Ĺ�ϣֵ��Ϊ�ֽ�������ʽ��Ĭ�ϲ��� HexUpper ��ʽ����
        /// </summary>
        /// <param name="hash">��ϣֵ���ַ�����ʽ����</param>
        /// <returns>��ϣֵ���ֽ�������ʽ����</returns>
        public virtual byte[] FromHashString(string hash) => Convert.FromHexString(hash);
    }
}
