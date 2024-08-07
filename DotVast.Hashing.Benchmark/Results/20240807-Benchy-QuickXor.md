```

BenchmarkDotNet v0.13.12, Windows 11 (10.0.22631.3880/23H2/2023Update/SunValley3)
AMD Ryzen 7 6800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK 9.0.100-preview.6.24328.19
  [Host]     : .NET 8.0.7 (8.0.724.31311), X64 RyuJIT AVX2
  Job-FMMXXJ : .NET 9.0.0 (9.0.24.32707), X64 RyuJIT AVX2

Runtime=.NET 9.0  IterationTime=512.0000 ms

```
| Method               | Length  | Mean     | Ratio | Allocated | Alloc Ratio |
|--------------------- |-------- |---------:|------:|----------:|------------:|
| DotVast_QuickXor     | 1000000 | 27.55 μs |  1.00 |      48 B |        1.00 |
| DotVast_QuickXor_New | 1000000 | 22.22 μs |  0.81 |      48 B |        1.00 |
