namespace DotVast.Hashing.Tests.NativeCryptoTests;

public class SM3Test() : IHasherTestDriver(_emptyInputTestCase)
{
    private static readonly TestCase _emptyInputTestCase = new("", "1AB21D8355CFA17F8E61194831E81A8F22BEC8C728FEFB747ED035EB5082AA2B");

    protected override IHasher CreateInstance() => new NativeCrypto.SM3();

    public static readonly TheoryData<TestCase> TestCases =
    [
        new("616263", "66C7F0F462EEEDD9D1F2D46BDC10E4E24167C4875CF2F7A2297DA02B8F4BA8E0"),
        new("61626364616263646162636461626364616263646162636461626364616263646162636461626364616263646162636461626364616263646162636461626364", "DEBE9FF92275B8A138604889C18E5A4D6FDB70E5387E5765293DCBA39C0C5732")
    ];

    [Theory]
    [MemberData(nameof(TestCases))]
    public void InstanceAppend(TestCase testCase) => InstanceAppendDriver(testCase);

    [Theory]
    [MemberData(nameof(TestCases))]
    public void InstanceAppendAndReset(TestCase testCase) => InstanceAppendAndResetDriver(testCase);

    [Theory]
    [MemberData(nameof(TestCases))]
    public void InstanceMultiAppend(TestCase testCase) => InstanceMultiAppendDriver(testCase);

    [Theory]
    [MemberData(nameof(TestCases))]
    public void InstanceVerifyResetState(TestCase testCase) => InstanceVerifyResetStateDriver(testCase);
}
