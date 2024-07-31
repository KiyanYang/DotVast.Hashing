# dotnet pack -p:NativeLibPack=true
$DllName = 'native_crypto'
$HasherInfoList = @(
    # ClassName, Feature, FnPrefix, HashLengthInBytes
    @('BLAKE2b160', 'blake2', 'blake2b160', 20),
    @('BLAKE2b256', 'blake2', 'blake2b256', 32),
    @('BLAKE2b384', 'blake2', 'blake2b384', 48),
    @('BLAKE2b512', 'blake2', 'blake2b512', 64),
    @('BLAKE2s128', 'blake2', 'blake2s128', 16),
    @('BLAKE2s160', 'blake2', 'blake2s160', 20),
    @('BLAKE2s224', 'blake2', 'blake2s224', 28),
    @('BLAKE2s256', 'blake2', 'blake2s256', 32),
    @('BLAKE3', 'blake3', 'blake3', 32),
    @('MD4', 'md4', 'md4', 16),
    @('MD5', 'md5', 'md5', 16),
    @('RIPEMD128', 'ripemd', 'ripemd128', 16),
    @('RIPEMD160', 'ripemd', 'ripemd160', 20),
    @('RIPEMD256', 'ripemd', 'ripemd256', 32),
    @('RIPEMD320', 'ripemd', 'ripemd320', 40),
    @('SHA1', 'sha1', 'sha1', 20),
    @('SHA224', 'sha2', 'sha224', 28),
    @('SHA256', 'sha2', 'sha256', 32),
    @('SHA384', 'sha2', 'sha384', 48),
    @('SHA512', 'sha2', 'sha512', 64),
    @('SHA3_224', 'sha3', 'sha3_224', 28),
    @('SHA3_256', 'sha3', 'sha3_256', 32),
    @('SHA3_384', 'sha3', 'sha3_384', 48),
    @('SHA3_512', 'sha3', 'sha3_512', 64),
    @('Keccak224', 'sha3', 'keccak224', 28),
    @('Keccak256', 'sha3', 'Keccak256', 32),
    @('Keccak384', 'sha3', 'Keccak384', 48),
    @('Keccak512', 'sha3', 'Keccak512', 64),
    @('SM3', 'sm3', 'sm3', 32)
)

foreach ($hasherInfo in $hasherInfoList) {
    $className = $hasherInfo[0]
    $handleClassName = $className + 'Handle'
    $feature = $hasherInfo[1]
    $fnPrefix = $hasherInfo[2]
    $fnNew = $fnPrefix + '_new'
    $fnReset = $fnPrefix + '_reset'
    $fnUpdate = $fnPrefix + '_update'
    $fnFinalize = $fnPrefix + '_finalize'
    $fnFree = $fnPrefix + '_free'
    $hashLength = $hasherInfo[3]

    $generated = @"
// <auto-generated/>

#if Benchmark || all || $feature

using DotVast.Hashing;

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace DotVast.Hashing.NativeCrypto;

public sealed partial class $className : IHasher
{
    private sealed class $handleClassName : HasherHandle
    {
        protected override void Free() => $fnFree(handle);
    }

    private readonly $handleClassName _handle = $fnNew();

    public int HashLengthInBytes => $hashLength;

    public void Reset() => $fnReset(_handle);

    public void Append(ReadOnlySpan<byte> source) => $fnUpdate(_handle, source, source.Length);

    public byte[] Finalize()
    {
        var ret = new byte[$hashLength];
        $fnFinalize(_handle, ret, $hashLength);
        return ret;
    }

    [LibraryImport("$DllName", EntryPoint = "$fnNew")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial $handleClassName $fnNew();

    [LibraryImport("$DllName", EntryPoint = "$fnReset")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void $fnReset($handleClassName hasherHandle);

    [LibraryImport("$DllName", EntryPoint = "$fnUpdate")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void $fnUpdate($handleClassName hasherHandle, ReadOnlySpan<byte> input, int size);

    [LibraryImport("$DllName", EntryPoint = "$fnFinalize")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void $fnFinalize($handleClassName hasherHandle, Span<byte> output, int size);

    [LibraryImport("$DllName", EntryPoint = "$fnFree")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void $fnFree(nint hasherPtr);
}

#endif
"@

    $generated | Out-File -FilePath "./NativeCrypto/$className.cs"
}