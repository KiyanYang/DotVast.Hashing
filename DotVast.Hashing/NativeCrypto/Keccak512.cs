// <auto-generated/>

#if Benchmark || all || sha3

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace DotVast.Hashing.NativeCrypto;

internal sealed partial class Keccak512 : IHasher
{
    private sealed class Keccak512Handle : HasherHandle
    {
        protected override void Free() => keccak512_free(handle);
    }

    private readonly Keccak512Handle _handle = keccak512_new();

    public int HashLengthInBytes => 64;

    public void Reset() => keccak512_reset(_handle);

    public void Append(ReadOnlySpan<byte> source)
    {
        if (!source.IsEmpty)
            keccak512_update(_handle, source, source.Length);
    }

    public byte[] Finalize()
    {
        var ret = new byte[64];
        keccak512_finalize(_handle, ret, 64);
        return ret;
    }

    [LibraryImport("native_crypto", EntryPoint = "keccak512_new")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Keccak512Handle keccak512_new();

    [LibraryImport("native_crypto", EntryPoint = "keccak512_reset")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void keccak512_reset(Keccak512Handle hasherHandle);

    [LibraryImport("native_crypto", EntryPoint = "keccak512_update")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void keccak512_update(Keccak512Handle hasherHandle, ReadOnlySpan<byte> input, int size);

    [LibraryImport("native_crypto", EntryPoint = "keccak512_finalize")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void keccak512_finalize(Keccak512Handle hasherHandle, Span<byte> output, int size);

    [LibraryImport("native_crypto", EntryPoint = "keccak512_free")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void keccak512_free(nint hasherPtr);
}

#endif
