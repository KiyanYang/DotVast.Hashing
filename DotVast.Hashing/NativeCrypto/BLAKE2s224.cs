// <auto-generated/>

#if Benchmark || all || blake2

using DotVast.Hashing;

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace DotVast.Hashing.NativeCrypto;

public sealed partial class BLAKE2s224 : IHasher
{
    private sealed class BLAKE2s224Handle : HasherHandle
    {
        protected override void Free() => blake2s224_free(handle);
    }

    private readonly BLAKE2s224Handle _handle = blake2s224_new();

    public int HashLengthInBytes => 28;

    public void Reset() => blake2s224_reset(_handle);

    public void Append(ReadOnlySpan<byte> source) => blake2s224_update(_handle, source, source.Length);

    public byte[] Finalize()
    {
        var ret = new byte[28];
        blake2s224_finalize(_handle, ret, 28);
        return ret;
    }

    [LibraryImport("native_crypto", EntryPoint = "blake2s224_new")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial BLAKE2s224Handle blake2s224_new();

    [LibraryImport("native_crypto", EntryPoint = "blake2s224_reset")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void blake2s224_reset(BLAKE2s224Handle hasherHandle);

    [LibraryImport("native_crypto", EntryPoint = "blake2s224_update")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void blake2s224_update(BLAKE2s224Handle hasherHandle, ReadOnlySpan<byte> input, int size);

    [LibraryImport("native_crypto", EntryPoint = "blake2s224_finalize")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void blake2s224_finalize(BLAKE2s224Handle hasherHandle, Span<byte> output, int size);

    [LibraryImport("native_crypto", EntryPoint = "blake2s224_free")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void blake2s224_free(nint hasherPtr);
}

#endif
