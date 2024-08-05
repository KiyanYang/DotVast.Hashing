// <auto-generated/>

#if Benchmark || all || md4

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace DotVast.Hashing.NativeCrypto;

internal sealed partial class MD4 : IHasher
{
    private sealed class MD4Handle : HasherHandle
    {
        protected override void Free() => md4_free(handle);
    }

    private readonly MD4Handle _handle = md4_new();

    public int HashLengthInBytes => 16;

    public void Reset() => md4_reset(_handle);

    public void Append(ReadOnlySpan<byte> source)
    {
        if (!source.IsEmpty)
            md4_update(_handle, source, source.Length);
    }

    public byte[] Finalize()
    {
        var ret = new byte[16];
        md4_finalize(_handle, ret, 16);
        return ret;
    }

    [LibraryImport("native_crypto", EntryPoint = "md4_new")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial MD4Handle md4_new();

    [LibraryImport("native_crypto", EntryPoint = "md4_reset")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void md4_reset(MD4Handle hasherHandle);

    [LibraryImport("native_crypto", EntryPoint = "md4_update")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void md4_update(MD4Handle hasherHandle, ReadOnlySpan<byte> input, int size);

    [LibraryImport("native_crypto", EntryPoint = "md4_finalize")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void md4_finalize(MD4Handle hasherHandle, Span<byte> output, int size);

    [LibraryImport("native_crypto", EntryPoint = "md4_free")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void md4_free(nint hasherPtr);
}

#endif
