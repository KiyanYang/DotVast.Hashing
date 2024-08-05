namespace DotVast.Hashing;

public static class Hasher
{
    public static IHasher CreateBLAKE2b160() => new NativeCrypto.BLAKE2b160();
    public static IHasher CreateBLAKE2b256() => new NativeCrypto.BLAKE2b256();
    public static IHasher CreateBLAKE2b384() => new NativeCrypto.BLAKE2b384();
    public static IHasher CreateBLAKE2b512() => new NativeCrypto.BLAKE2b512();
    public static IHasher CreateBLAKE2s128() => new NativeCrypto.BLAKE2s128();
    public static IHasher CreateBLAKE2s160() => new NativeCrypto.BLAKE2s160();
    public static IHasher CreateBLAKE2s224() => new NativeCrypto.BLAKE2s224();
    public static IHasher CreateBLAKE2s256() => new NativeCrypto.BLAKE2s256();
    public static IHasher CreateBLAKE3() => new NativeCrypto.BLAKE3();

    public static IHasher CreateMD4() => new NativeCrypto.MD4();
    public static IHasher CreateMD5() => new NativeCrypto.MD5();

    public static IHasher CreateRIPEMD128() => new NativeCrypto.RIPEMD128();
    public static IHasher CreateRIPEMD160() => new NativeCrypto.RIPEMD160();
    public static IHasher CreateRIPEMD256() => new NativeCrypto.RIPEMD256();
    public static IHasher CreateRIPEMD320() => new NativeCrypto.RIPEMD320();

    public static IHasher CreateSHA1() => new NativeCrypto.SHA1();
    public static IHasher CreateSHA224() => new NativeCrypto.SHA224();
    public static IHasher CreateSHA256() => new NativeCrypto.SHA256();
    public static IHasher CreateSHA384() => new NativeCrypto.SHA384();
    public static IHasher CreateSHA512() => new NativeCrypto.SHA512();
    public static IHasher CreateSHA3_224() => new NativeCrypto.SHA3_224();
    public static IHasher CreateSHA3_256() => new NativeCrypto.SHA3_256();
    public static IHasher CreateSHA3_384() => new NativeCrypto.SHA3_384();
    public static IHasher CreateSHA3_512() => new NativeCrypto.SHA3_512();

    public static IHasher CreateKeccak224() => new NativeCrypto.Keccak224();
    public static IHasher CreateKeccak256() => new NativeCrypto.Keccak256();
    public static IHasher CreateKeccak384() => new NativeCrypto.Keccak384();
    public static IHasher CreateKeccak512() => new NativeCrypto.Keccak512();

    public static IHasher CreateSM3() => new NativeCrypto.SM3();

    public static IHasher CreateQuickXor() => new QuickXor();
}
