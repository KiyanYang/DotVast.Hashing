namespace DotVast.Hashing.Tests;

public class MD4Test() : IHasherTestDriver(_emptyInputTestCase)
{
    private static readonly TestCase _emptyInputTestCase = new("", "31D6CFE0D16AE931B73C59D7E0C089C0");

    protected override IHasher CreateInstance() => new MD4();

    public static readonly TheoryData<TestCase> TestCases =
    [
        new("a"u8.ToArray(), "BDE52CB31DE33E46245E05FBDBD6FB24"),
        new("abc"u8.ToArray(), "A448017AAF21D8525FC10AE87AA6729D"),
        new("message digest"u8.ToArray(), "D9130A8164549FE818874806E1C7014B"),
        new("abcdefghijklmnopqrstuvwxyz"u8.ToArray(), "D79E1C308AA5BBCDEEA8ED63DF412DA9"),
        new("ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789"u8.ToArray(), "043F8582F241DB351CE627E153E7F0E4"),
        new("12345678901234567890123456789012345678901234567890123456789012345678901234567890"u8.ToArray(), "E33B4DDC9C38F2199C3E7B164FCC0536"),
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
