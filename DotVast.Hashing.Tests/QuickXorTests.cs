namespace DotVast.Hashing.Tests;

public class QuickXorTests() : IHasherTestDriver(_emptyInputTestCase)
{
    private static readonly TestCase _emptyInputTestCase = new TestCaseBase64("", "AAAAAAAAAAAAAAAAAAAAAAAAAAA=");

    protected override IHasher CreateInstance() => new QuickXor();

    public static readonly TheoryData<TestCaseBase64> TestCases =
    [
        new("517569636B586F72", "UahDGsawBiy8QQ4ACAAAAAAAAAA="),
        new("517569636B586F7248617368416C676F726974686D", "sKUxUsWt1vy6QQ5IHcMc0BAENpw="),
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