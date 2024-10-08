// <auto-generated/>

#if Benchmark || all || blake2

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace DotVast.Hashing.NativeCrypto;

internal sealed partial class BLAKE2b512 : IHasher
{
    private sealed class BLAKE2b512Handle : HasherHandle
    {
        protected override void Free() => blake2b512_free(handle);
    }

    private readonly BLAKE2b512Handle _handle = blake2b512_new();

    public int HashLengthInBytes => 64;

    public void Reset() => blake2b512_reset(_handle);

    public void Append(ReadOnlySpan<byte> source)
    {
        if (!source.IsEmpty)
            blake2b512_update(_handle, source, source.Length);
    }

    public byte[] Finalize()
    {
        var ret = new byte[64];
        blake2b512_finalize(_handle, ret, 64);
        return ret;
    }

    [LibraryImport("native_crypto", EntryPoint = "blake2b512_new")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial BLAKE2b512Handle blake2b512_new();

    [LibraryImport("native_crypto", EntryPoint = "blake2b512_reset")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void blake2b512_reset(BLAKE2b512Handle hasherHandle);

    [LibraryImport("native_crypto", EntryPoint = "blake2b512_update")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void blake2b512_update(BLAKE2b512Handle hasherHandle, ReadOnlySpan<byte> input, int size);

    [LibraryImport("native_crypto", EntryPoint = "blake2b512_finalize")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void blake2b512_finalize(BLAKE2b512Handle hasherHandle, Span<byte> output, int size);

    [LibraryImport("native_crypto", EntryPoint = "blake2b512_free")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void blake2b512_free(nint hasherPtr);
}

#endif
