// <auto-generated/>

#if Benchmark || all || sha3

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace DotVast.Hashing.NativeCrypto;

internal sealed partial class Keccak384 : IHasher
{
    private sealed class Keccak384Handle : HasherHandle
    {
        protected override void Free() => keccak384_free(handle);
    }

    private readonly Keccak384Handle _handle = keccak384_new();

    public int HashLengthInBytes => 48;

    public void Reset() => keccak384_reset(_handle);

    public void Append(ReadOnlySpan<byte> source)
    {
        if (!source.IsEmpty)
            keccak384_update(_handle, source, source.Length);
    }

    public byte[] Finalize()
    {
        var ret = new byte[48];
        keccak384_finalize(_handle, ret, 48);
        return ret;
    }

    [LibraryImport("native_crypto", EntryPoint = "keccak384_new")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Keccak384Handle keccak384_new();

    [LibraryImport("native_crypto", EntryPoint = "keccak384_reset")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void keccak384_reset(Keccak384Handle hasherHandle);

    [LibraryImport("native_crypto", EntryPoint = "keccak384_update")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void keccak384_update(Keccak384Handle hasherHandle, ReadOnlySpan<byte> input, int size);

    [LibraryImport("native_crypto", EntryPoint = "keccak384_finalize")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void keccak384_finalize(Keccak384Handle hasherHandle, Span<byte> output, int size);

    [LibraryImport("native_crypto", EntryPoint = "keccak384_free")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void keccak384_free(nint hasherPtr);
}

#endif
