```

BenchmarkDotNet v0.13.12, Windows 11 (10.0.22631.3880/23H2/2023Update/SunValley3)
AMD Ryzen 7 6800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK 9.0.100-preview.6.24328.19
  [Host]     : .NET 8.0.7 (8.0.724.31311), X64 RyuJIT AVX2
  Job-DSSAIY : .NET 8.0.7 (8.0.724.31311), X64 RyuJIT AVX2

Runtime=.NET 8.0  IterationTime=512.0000 ms  

```
| Method              | Length  | Mean        | Ratio | Allocated | Alloc Ratio |
|-------------------- |-------- |------------:|------:|----------:|------------:|
| Native_BLAKE2b512   | 1000000 |   706.24 μs |  0.79 |      89 B |        0.11 |
| HashLib4_BLAKE2b512 | 1000000 |   888.85 μs |  1.00 |     801 B |        1.00 |
|                     |         |             |       |           |             |
| Native_BLAKE2s256   | 1000000 | 1,220.41 μs |  0.82 |      57 B |        0.09 |
| HashLib4_BLAKE2s256 | 1000000 | 1,483.91 μs |  1.00 |     641 B |        1.00 |
|                     |         |             |       |           |             |
| Native_BLAKE3       | 1000000 |   289.28 μs |  0.13 |      56 B |       0.000 |
| HashLib4_BLAKE3     | 1000000 | 2,276.84 μs |  1.00 |  154514 B |       1.000 |
|                     |         |             |       |           |             |
| Native_MD4          | 1000000 |   768.82 μs |  0.96 |      41 B |        0.28 |
| HashLib4_MD4        | 1000000 |   800.57 μs |  1.00 |     145 B |        1.00 |
| DotVast_MD4         | 1000000 |   911.46 μs |  1.14 |      41 B |        0.28 |
|                     |         |             |       |           |             |
| Native_MD5          | 1000000 | 1,329.03 μs |  0.67 |      41 B |        0.28 |
| HashLib4_MD5        | 1000000 | 1,974.42 μs |  1.00 |     145 B |        1.00 |
|                     |         |             |       |           |             |
| Native_RIPEMD160    | 1000000 | 2,572.61 μs |  0.55 |      50 B |        0.29 |
| HashLib4_RIPEMD160  | 1000000 | 4,681.41 μs |  1.00 |     172 B |        1.00 |
|                     |         |             |       |           |             |
| Native_SHA1         | 1000000 |   413.75 μs |  0.18 |      48 B |        0.28 |
| HashLib4_SHA1       | 1000000 | 2,321.20 μs |  1.00 |     170 B |        1.00 |
|                     |         |             |       |           |             |
| Native_SHA512       | 1000000 | 1,332.75 μs |  0.18 |      89 B |        0.20 |
| HashLib4_SHA512     | 1000000 | 7,247.23 μs |  1.00 |     445 B |        1.00 |
|                     |         |             |       |           |             |
| HashLib4_SHA3_512   | 1000000 | 3,686.02 μs |  1.00 |   21219 B |       1.000 |
| Native_SHA3_512     | 1000000 | 3,867.54 μs |  1.05 |      91 B |       0.004 |
|                     |         |             |       |           |             |
| Native_SM3          | 1000000 | 2,372.17 μs |  0.65 |      58 B |        0.98 |
| DotVast_SM3         | 1000000 | 3,659.88 μs |  1.00 |      59 B |        1.00 |
|                     |         |             |       |           |             |
| DotVast_QuickXor    | 1000000 |    28.49 μs |  1.00 |      48 B |        1.00 |
