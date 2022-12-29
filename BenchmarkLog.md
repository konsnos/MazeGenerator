// * Summary *

BenchmarkDotNet=v0.13.2, OS=macOS 13.1 (22C65) [Darwin 22.2.0]
Apple M1, 1 CPU, 8 logical and 8 physical cores
.NET SDK=6.0.101
  [Host]     : .NET 6.0.1 (6.0.121.56705), Arm64 RyuJIT AdvSIMD
  DefaultJob : .NET 6.0.1 (6.0.121.56705), Arm64 RyuJIT AdvSIMD


|      Method | Size |       Mean |     Error |    StdDev |     Gen0 |  Allocated |
|------------ |----- |-----------:|----------:|----------:|---------:|-----------:|
| HuntAndKill |  100 |   9.209 ms | 0.0244 ms | 0.0229 ms | 281.2500 |  586.69 KB |
| HuntAndKill |  200 | 127.032 ms | 1.8947 ms | 1.5821 ms | 750.0000 | 2343.94 KB |



// * Summary *

BenchmarkDotNet=v0.13.2, OS=macOS 13.1 (22C65) [Darwin 22.2.0]
Apple M1, 1 CPU, 8 logical and 8 physical cores
.NET SDK=6.0.101
  [Host]     : .NET 6.0.1 (6.0.121.56705), Arm64 RyuJIT AdvSIMD
  DefaultJob : .NET 6.0.1 (6.0.121.56705), Arm64 RyuJIT AdvSIMD


|             Method | Size |     Mean |    Error |   StdDev |      Gen0 |      Gen1 |     Gen2 | Allocated |
|------------------- |----- |---------:|---------:|---------:|----------:|----------:|---------:|----------:|
|      KruskalRandom |  100 | 11.43 ms | 0.095 ms | 0.084 ms | 1546.8750 |  703.1250 | 343.7500 |   4.85 MB |
| KruskalPassingBias |  100 | 14.55 ms | 0.142 ms | 0.118 ms | 1531.2500 |  734.3750 | 359.3750 |      5 MB |
|      KruskalRandom |  200 | 53.48 ms | 0.710 ms | 0.729 ms | 4300.0000 | 2000.0000 | 800.0000 |  19.44 MB |
| KruskalPassingBias |  200 | 71.98 ms | 1.433 ms | 3.971 ms | 4000.0000 | 1750.0000 | 750.0000 |  20.04 MB |



// * Summary *

BenchmarkDotNet=v0.13.2, OS=macOS 13.1 (22C65) [Darwin 22.2.0]
Apple M1, 1 CPU, 8 logical and 8 physical cores
.NET SDK=6.0.101
  [Host]     : .NET 6.0.1 (6.0.121.56705), Arm64 RyuJIT AdvSIMD
  DefaultJob : .NET 6.0.1 (6.0.121.56705), Arm64 RyuJIT AdvSIMD


|                Method | Size |     Mean |     Error |    StdDev |     Gen0 |     Gen1 |    Gen2 |  Allocated |
|---------------------- |----- |---------:|----------:|----------:|---------:|---------:|--------:|-----------:|
| RecursiveBacktracking |  100 | 1.859 ms | 0.0050 ms | 0.0044 ms | 199.2188 |  48.8281 |       - |  469.26 KB |
| RecursiveBacktracking |  200 | 8.858 ms | 0.1269 ms | 0.1187 ms | 546.8750 | 250.0000 | 93.7500 | 1875.97 KB |
