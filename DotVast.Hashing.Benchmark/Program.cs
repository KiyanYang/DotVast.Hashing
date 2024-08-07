using System.Collections.Immutable;

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Environments;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Order;
using BenchmarkDotNet.Reports;
using BenchmarkDotNet.Running;

using DotVast.Hashing;

using HashLib4Crypto = HashLib4CSharp.Base.HashFactory.Crypto;
using NativeCrypto = DotVast.Hashing.NativeCrypto;

#if DEBUG
var b = new Benchy
{
    Length = 100_0000
};

b.GlobalSetup();
var r1 = b.Native_BLAKE3();
var r2 = b.HashLib4_BLAKE3();

Console.WriteLine(Convert.ToHexString(r1));
Console.WriteLine(Convert.ToHexString(r2));
Console.WriteLine(Convert.ToHexString(r1) == Convert.ToHexString(r2));
#else
BenchmarkRunner.Run<Benchy>();
#endif

[Config(typeof(Config))]
[MemoryDiagnoser(false)]
[AsciiDocExporter]
public class Benchy
{
    private sealed class Config : ManualConfig
    {
        public Config()
        {
            WithOptions(ConfigOptions.DontOverwriteResults);

            Orderer = new HasherOrderer();

            var job = new Job(Job.Default)
            {
                Run =
                {
                    IterationTime = Perfolizer.Horology.TimeInterval.Millisecond * 512,
                },
            };

            AddJob(job.WithRuntime(CoreRuntime.Core90));

            HideColumns(BenchmarkDotNet.Columns.Column.Error, BenchmarkDotNet.Columns.Column.StdDev);
        }

        private sealed class HasherOrderer : IOrderer
        {
            public IEnumerable<BenchmarkCase> GetExecutionOrder(ImmutableArray<BenchmarkCase> benchmarksCase, IEnumerable<BenchmarkLogicalGroupRule>? order = null) =>
                benchmarksCase;

            public IEnumerable<BenchmarkCase> GetSummaryOrder(ImmutableArray<BenchmarkCase> benchmarksCases, Summary summary)
            {
                return benchmarksCases.GroupBy(bc => GetLogicalGroupKey(default, bc))
                    .SelectMany(g => g.OrderBy(bc => summary[bc]!.ResultStatistics?.Mean));
            }

            public string? GetHighlightGroupKey(BenchmarkCase benchmarkCase) => null;

            public string? GetLogicalGroupKey(ImmutableArray<BenchmarkCase> allBenchmarksCases, BenchmarkCase benchmarkCase)
            {
                var index = benchmarkCase.DisplayInfo.IndexOf('_');
                return index >= 0
                    ? benchmarkCase.DisplayInfo[index..].ToLowerInvariant()
                    : benchmarkCase.DisplayInfo.ToLowerInvariant();
            }

            public IEnumerable<IGrouping<string, BenchmarkCase>> GetLogicalGroupOrder(IEnumerable<IGrouping<string, BenchmarkCase>> logicalGroups, IEnumerable<BenchmarkLogicalGroupRule>? order = null)
            {
                return logicalGroups.OrderBy(g => g.Key);
            }

            public bool SeparateLogicalGroups => true;
        }
    }

    private readonly IHasher _native_blake2b512 = new NativeCrypto.BLAKE2b512();
    private readonly IHasher _hashlib4_blake2b512 = HashLib4Crypto.CreateBlake2B_512().ToIHasher();

    private readonly IHasher _native_blake2s256 = new NativeCrypto.BLAKE2s256();
    private readonly IHasher _hashlib4_blake2s256 = HashLib4Crypto.CreateBlake2S_256().ToIHasher();

    private readonly IHasher _native_blake3 = new NativeCrypto.BLAKE3();
    private readonly IHasher _hashlib4_blake3 = HashLib4Crypto.CreateBlake3_256().ToIHasher();

    private readonly IHasher _native_md4 = new NativeCrypto.MD4();
    private readonly IHasher _hashlib4_md4 = HashLib4Crypto.CreateMD4().ToIHasher();
    private readonly IHasher _dotvast_md4 = new MD4();

    private readonly IHasher _native_md5 = new NativeCrypto.MD5();
    private readonly IHasher _hashlib4_md5 = HashLib4Crypto.CreateMD5().ToIHasher();

    private readonly IHasher _native_ripemd160 = new NativeCrypto.RIPEMD160();
    private readonly IHasher _hashlib4_ripemd160 = HashLib4Crypto.CreateRIPEMD160().ToIHasher();

    private readonly IHasher _native_sha1 = new NativeCrypto.SHA1();
    private readonly IHasher _hashlib4_sha1 = HashLib4Crypto.CreateSHA1().ToIHasher();

    private readonly IHasher _native_sha512 = new NativeCrypto.SHA512();
    private readonly IHasher _hashlib4_sha512 = HashLib4Crypto.CreateSHA2_512().ToIHasher();

    private readonly IHasher _native_sha3_512 = new NativeCrypto.SHA3_512();
    private readonly IHasher _hashlib4_sha3_512 = HashLib4Crypto.CreateSHA3_512().ToIHasher();

    private readonly IHasher _native_sm3 = new NativeCrypto.SM3();
    private readonly IHasher _dotvast_sm3 = new SM3();

    private readonly IHasher _dotvast_quickxor = new QuickXor();

    [Params(1_000_000)]
    public int Length { get; set; }

    public const int BufferLength = 1024 * 4;

    public byte[]? _bytes;

    [GlobalSetup]
    public void GlobalSetup()
    {
        _bytes = new byte[Length];
        var random = new Random(1024);
        random.NextBytes(_bytes);
    }

    [Benchmark] public byte[] Native_BLAKE2b512() => Compute(_native_blake2b512);
    [Benchmark(Baseline = true)] public byte[] HashLib4_BLAKE2b512() => Compute(_hashlib4_blake2b512);

    [Benchmark] public byte[] Native_BLAKE2s256() => Compute(_native_blake2s256);
    [Benchmark(Baseline = true)] public byte[] HashLib4_BLAKE2s256() => Compute(_hashlib4_blake2s256);

    [Benchmark] public byte[] Native_BLAKE3() => Compute(_native_blake3);
    [Benchmark(Baseline = true)] public byte[] HashLib4_BLAKE3() => Compute(_hashlib4_blake3);

    [Benchmark] public byte[] Native_MD4() => Compute(_native_md4);
    [Benchmark(Baseline = true)] public byte[] HashLib4_MD4() => Compute(_hashlib4_md4);
    [Benchmark] public byte[] DotVast_MD4() => Compute(_dotvast_md4);

    [Benchmark] public byte[] Native_MD5() => Compute(_native_md5);
    [Benchmark(Baseline = true)] public byte[] HashLib4_MD5() => Compute(_hashlib4_md5);

    [Benchmark] public byte[] Native_RIPEMD160() => Compute(_native_ripemd160);
    [Benchmark(Baseline = true)] public byte[] HashLib4_RIPEMD160() => Compute(_hashlib4_ripemd160);

    [Benchmark] public byte[] Native_SHA1() => Compute(_native_sha1);
    [Benchmark(Baseline = true)] public byte[] HashLib4_SHA1() => Compute(_hashlib4_sha1);

    [Benchmark] public byte[] Native_SHA512() => Compute(_native_sha512);
    [Benchmark(Baseline = true)] public byte[] HashLib4_SHA512() => Compute(_hashlib4_sha512);

    [Benchmark] public byte[] Native_SHA3_512() => Compute(_native_sha3_512);
    [Benchmark(Baseline = true)] public byte[] HashLib4_SHA3_512() => Compute(_hashlib4_sha3_512);

    [Benchmark] public byte[] Native_SM3() => Compute(_native_sm3);
    [Benchmark(Baseline = true)] public byte[] DotVast_SM3() => Compute(_dotvast_sm3);

    [Benchmark(Baseline = true)] public byte[] DotVast_QuickXor() => Compute(_dotvast_quickxor);

    public byte[] Compute(IHasher hasher)
    {
        var bytes = _bytes.AsSpan();
        while (bytes.Length > BufferLength)
        {
            hasher.Append(bytes[..BufferLength]);
            bytes = bytes[BufferLength..];
        }
        hasher.Append(bytes);
        return hasher.FinalizeAndReset();
    }
}

public static class Extensions
{
    public static IHasher ToIHasher(this HashLib4CSharp.Interfaces.IHash hash) =>
        new HashLib4CSharpAdapter(hash);
}

public sealed class HashLib4CSharpAdapter : IHasher
{
    public HashLib4CSharpAdapter(HashLib4CSharp.Interfaces.IHash hasher)
    {
        _hasher = hasher;
        _hasher.Initialize();
    }

    private readonly HashLib4CSharp.Interfaces.IHash _hasher;

    public int HashLengthInBytes => _hasher.HashSize;

    public void Append(ReadOnlySpan<byte> source) => _hasher.TransformByteSpan(source);
    public byte[] Finalize() => _hasher.TransformFinal().GetBytes();
    public void Reset() => _hasher.Initialize();
}
