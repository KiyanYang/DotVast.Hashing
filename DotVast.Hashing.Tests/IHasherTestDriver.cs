namespace DotVast.Hashing.Tests;

public abstract class IHasherTestDriver(TestCase emptyInputTestCase)
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
}
