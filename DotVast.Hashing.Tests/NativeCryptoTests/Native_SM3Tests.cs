namespace DotVast.Hashing.Tests.NativeCryptoTests;

public class Native_SM3Tests() : SM3Tests()
{
    protected override IHasher CreateInstance() => new NativeCrypto.SM3();
}
