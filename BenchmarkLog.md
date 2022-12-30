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


|             Method | Size |      Mean |     Error |    StdDev |    Median |      Gen0 |      Gen1 |      Gen2 | Allocated |
|------------------- |----- |----------:|----------:|----------:|----------:|----------:|----------:|----------:|----------:|
|      KruskalRandom |  100 |  6.369 ms | 0.0861 ms | 0.0806 ms |  6.361 ms | 1648.4375 |  804.6875 |  398.4375 |   4.39 MB |
| KruskalPassingBias |  100 |  8.601 ms | 0.1694 ms | 0.2536 ms |  8.688 ms | 1375.0000 |  640.6250 |  312.5000 |   4.54 MB |
|      KruskalRandom |  200 | 30.380 ms | 0.6042 ms | 1.5047 ms | 29.813 ms | 3656.2500 | 1875.0000 |  875.0000 |  17.61 MB |
| KruskalPassingBias |  200 | 41.774 ms | 0.8347 ms | 1.4173 ms | 41.420 ms | 3307.6923 | 1692.3077 | 1000.0000 |  18.21 MB |


|                Method | Size |     Mean |     Error |    StdDev |     Gen0 |     Gen1 |    Gen2 |  Allocated |
|---------------------- |----- |---------:|----------:|----------:|---------:|---------:|--------:|-----------:|
| RecursiveBacktracking |  100 | 1.859 ms | 0.0050 ms | 0.0044 ms | 199.2188 |  48.8281 |       - |  469.26 KB |
| RecursiveBacktracking |  200 | 8.858 ms | 0.1269 ms | 0.1187 ms | 546.8750 | 250.0000 | 93.7500 | 1875.97 KB |
