// <auto-generated/>

#if Benchmark || all || sha3

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace DotVast.Hashing.NativeCrypto;

internal sealed partial class Keccak256 : IHasher
{
    private sealed class Keccak256Handle : HasherHandle
    {
        protected override void Free() => keccak256_free(handle);
    }

    private readonly Keccak256Handle _handle = keccak256_new();

    public int HashLengthInBytes => 32;

    public void Reset() => keccak256_reset(_handle);

    public void Append(ReadOnlySpan<byte> source)
    {
        if (!source.IsEmpty)
            keccak256_update(_handle, source, source.Length);
    }

    public byte[] Finalize()
    {
        var ret = new byte[32];
        keccak256_finalize(_handle, ret, 32);
        return ret;
    }

    [LibraryImport("native_crypto", EntryPoint = "keccak256_new")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Keccak256Handle keccak256_new();

    [LibraryImport("native_crypto", EntryPoint = "keccak256_reset")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void keccak256_reset(Keccak256Handle hasherHandle);

    [LibraryImport("native_crypto", EntryPoint = "keccak256_update")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void keccak256_update(Keccak256Handle hasherHandle, ReadOnlySpan<byte> input, int size);

    [LibraryImport("native_crypto", EntryPoint = "keccak256_finalize")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void keccak256_finalize(Keccak256Handle hasherHandle, Span<byte> output, int size);

    [LibraryImport("native_crypto", EntryPoint = "keccak256_free")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void keccak256_free(nint hasherPtr);
}

#endif
