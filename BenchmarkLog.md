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


|             Method | Size |      Mean |     Error |    StdDev |    Median |      Gen0 |      Gen1 |     Gen2 | Allocated |
|------------------- |----- |----------:|----------:|----------:|----------:|----------:|----------:|---------:|----------:|
|      KruskalRandom |  100 |  8.735 ms | 0.0160 ms | 0.0142 ms |  8.734 ms | 1484.3750 |  656.2500 | 328.1250 |   4.16 MB |
| KruskalPassingBias |  100 | 12.243 ms | 0.1349 ms | 0.1262 ms | 12.227 ms | 1531.2500 |  718.7500 | 359.3750 |   4.31 MB |
|      KruskalRandom |  200 | 44.201 ms | 1.0401 ms | 3.0669 ms | 45.206 ms | 3666.6667 | 1916.6667 | 916.6667 |  16.69 MB |
| KruskalPassingBias |  200 | 57.480 ms | 0.2017 ms | 0.1887 ms | 57.512 ms | 3666.6667 | 1888.8889 | 888.8889 |   17.3 MB |


|                Method | Size |     Mean |     Error |    StdDev |     Gen0 |     Gen1 |    Gen2 |  Allocated |
|---------------------- |----- |---------:|----------:|----------:|---------:|---------:|--------:|-----------:|
| RecursiveBacktracking |  100 | 1.859 ms | 0.0050 ms | 0.0044 ms | 199.2188 |  48.8281 |       - |  469.26 KB |
| RecursiveBacktracking |  200 | 8.858 ms | 0.1269 ms | 0.1187 ms | 546.8750 | 250.0000 | 93.7500 | 1875.97 KB |
