// * Summary *

BenchmarkDotNet=v0.13.2, OS=macOS 13.0.1 (22A400) [Darwin 22.1.0]
Apple M1, 1 CPU, 8 logical and 8 physical cores
.NET SDK=6.0.101
  [Host]     : .NET 6.0.1 (6.0.121.56705), Arm64 RyuJIT AdvSIMD
  DefaultJob : .NET 6.0.1 (6.0.121.56705), Arm64 RyuJIT AdvSIMD


|  Method | Size |     Mean |     Error |    StdDev |
|-------- |----- |---------:|----------:|----------:|
| Kruskal |   50 | 1.021 ms | 0.0017 ms | 0.0015 ms |
| Kruskal |  100 | 7.108 ms | 0.0996 ms | 0.0883 ms |

// * Hints *
Outliers
  KruskalOverall.Kruskal: Default -> 1 outlier  was  removed (1.03 ms)
  KruskalOverall.Kruskal: Default -> 1 outlier  was  removed (7.52 ms)

// * Legends *
  Size   : Value of the 'Size' parameter
  Mean   : Arithmetic mean of all measurements
  Error  : Half of 99.9% confidence interval
  StdDev : Standard deviation of all measurements
  1 ms   : 1 Millisecond (0.001 sec)

// ***** BenchmarkRunner: End *****
Run time: 00:00:24 (24.92 sec), executed benchmarks: 2

Global total time: 00:00:30 (30.93 sec), executed benchmarks: 2
