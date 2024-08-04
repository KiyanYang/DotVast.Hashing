using DotVast.Hashing;

var data = new byte[10000];
Random.Shared.NextBytes(data);

// Simple
var blake3 = Hasher.CreateBLAKE3();
blake3.Append(data);
var hash1 = blake3.Finalize();
var hexHash1 = Convert.ToHexString(hash1);
Console.WriteLine($"Hash1(Create New)       : {hexHash1}");

// Reuse
blake3.Reset();
blake3.Append(data);
var hash2 = blake3.Finalize();
var hexHash2 = Convert.ToHexString(hash2);
Console.WriteLine($"Hash2(Reuse Old)        : {hexHash2}");

// Reuse conveniently
blake3.Reset();
blake3.Append(data);
var hash3 = blake3.FinalizeAndReset(); // == { var hash3 = blake3.Finalize(); blake3.Reset(); }
blake3.Append(data);
var hash4 = blake3.Finalize();
var hexHash3 = Convert.ToHexString(hash3);
var hexHash4 = Convert.ToHexString(hash4);
Console.WriteLine($"Hash3(FinalizeAndReset) : {hexHash3}");
Console.WriteLine($"Hash4(FinalizeAndReset) : {hexHash4}");
