// <auto-generated/>

#if Benchmark || all || sha2

using DotVast.Hashing;

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace DotVast.Hashing.NativeCrypto;

public sealed partial class SHA512 : IHasher
{
    private sealed class SHA512Handle : HasherHandle
    {
        protected override void Free() => sha512_free(handle);
    }

    private readonly SHA512Handle _handle = sha512_new();

    public int HashLengthInBytes => 64;

    public void Reset() => sha512_reset(_handle);

    public void Append(ReadOnlySpan<byte> source) => sha512_update(_handle, source, source.Length);

    public byte[] Finalize()
    {
        var ret = new byte[64];
        sha512_finalize(_handle, ret, 64);
        return ret;
    }

    [LibraryImport("native_crypto", EntryPoint = "sha512_new")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SHA512Handle sha512_new();

    [LibraryImport("native_crypto", EntryPoint = "sha512_reset")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void sha512_reset(SHA512Handle hasherHandle);

    [LibraryImport("native_crypto", EntryPoint = "sha512_update")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void sha512_update(SHA512Handle hasherHandle, ReadOnlySpan<byte> input, int size);

    [LibraryImport("native_crypto", EntryPoint = "sha512_finalize")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void sha512_finalize(SHA512Handle hasherHandle, Span<byte> output, int size);

    [LibraryImport("native_crypto", EntryPoint = "sha512_free")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void sha512_free(nint hasherPtr);
}

#endif
