// <auto-generated/>

#if Benchmark || all || ripemd

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace DotVast.Hashing.NativeCrypto;

internal sealed partial class RIPEMD256 : IHasher
{
    private sealed class RIPEMD256Handle : HasherHandle
    {
        protected override void Free() => ripemd256_free(handle);
    }

    private readonly RIPEMD256Handle _handle = ripemd256_new();

    public int HashLengthInBytes => 32;

    public void Reset() => ripemd256_reset(_handle);

    public void Append(ReadOnlySpan<byte> source)
    {
        if (!source.IsEmpty)
            ripemd256_update(_handle, source, source.Length);
    }

    public byte[] Finalize()
    {
        var ret = new byte[32];
        ripemd256_finalize(_handle, ret, 32);
        return ret;
    }

    [LibraryImport("native_crypto", EntryPoint = "ripemd256_new")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial RIPEMD256Handle ripemd256_new();

    [LibraryImport("native_crypto", EntryPoint = "ripemd256_reset")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void ripemd256_reset(RIPEMD256Handle hasherHandle);

    [LibraryImport("native_crypto", EntryPoint = "ripemd256_update")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void ripemd256_update(RIPEMD256Handle hasherHandle, ReadOnlySpan<byte> input, int size);

    [LibraryImport("native_crypto", EntryPoint = "ripemd256_finalize")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void ripemd256_finalize(RIPEMD256Handle hasherHandle, Span<byte> output, int size);

    [LibraryImport("native_crypto", EntryPoint = "ripemd256_free")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void ripemd256_free(nint hasherPtr);
}

#endif
