// <auto-generated/>

#if Benchmark || all || sha3

using DotVast.Hashing;

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace DotVast.Hashing.NativeCrypto;

public sealed partial class SHA3_256 : IHasher
{
    private sealed class SHA3_256Handle : HasherHandle
    {
        protected override void Free() => sha3_256_free(handle);
    }

    private readonly SHA3_256Handle _handle = sha3_256_new();

    public int HashLengthInBytes => 32;

    public void Reset() => sha3_256_reset(_handle);

    public void Append(ReadOnlySpan<byte> source) => sha3_256_update(_handle, source, source.Length);

    public byte[] Finalize()
    {
        var ret = new byte[32];
        sha3_256_finalize(_handle, ret, 32);
        return ret;
    }

    [LibraryImport("native_crypto", EntryPoint = "sha3_256_new")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SHA3_256Handle sha3_256_new();

    [LibraryImport("native_crypto", EntryPoint = "sha3_256_reset")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void sha3_256_reset(SHA3_256Handle hasherHandle);

    [LibraryImport("native_crypto", EntryPoint = "sha3_256_update")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void sha3_256_update(SHA3_256Handle hasherHandle, ReadOnlySpan<byte> input, int size);

    [LibraryImport("native_crypto", EntryPoint = "sha3_256_finalize")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void sha3_256_finalize(SHA3_256Handle hasherHandle, Span<byte> output, int size);

    [LibraryImport("native_crypto", EntryPoint = "sha3_256_free")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void sha3_256_free(nint hasherPtr);
}

#endif