``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19042.1083 (20H2/October2020Update)
Intel Core i7-7820HQ CPU 2.90GHz (Kaby Lake), 1 CPU, 8 logical and 4 physical cores
  [Host]               : .NET Framework 4.8 (4.8.4360.0), X64 RyuJIT
  .NET Framework 4.7.2 : .NET Framework 4.8 (4.8.4360.0), X64 RyuJIT

Job=.NET Framework 4.7.2  Runtime=.NET Framework 4.7.2  

```
|                                  Method |         assemblyName |      Mean |    Error |    StdDev |    Median | Ratio | RatioSD | Rank | Allocated |
|---------------------------------------- |--------------------- |----------:|---------:|----------:|----------:|------:|--------:|-----:|----------:|
|                       StartWith_Ordinal | Kobo.(...)erred [24] |  14.06 ns | 0.316 ns |  0.758 ns |  13.93 ns |  0.13 |    0.01 |    1 |         - |
|             StartWith_OrdinalIgnoreCase | Kobo.(...)erred [24] |  31.85 ns | 0.615 ns |  1.663 ns |  31.50 ns |  0.29 |    0.02 |    2 |         - |
|                        IsPrefix_Ordinal | Kobo.(...)erred [24] |  34.04 ns | 0.765 ns |  2.144 ns |  33.50 ns |  0.29 |    0.02 |    3 |         - |
|              IsPrefix_OrdinalIgnoreCase | Kobo.(...)erred [24] |  49.53 ns | 1.026 ns |  2.295 ns |  48.73 ns |  0.44 |    0.03 |    4 |         - |
|                     StartWith_Invariant | Kobo.(...)erred [24] |  99.37 ns | 1.869 ns |  1.835 ns |  99.18 ns |  0.88 |    0.02 |    5 |         - |
|                                IsPrefix | Kobo.(...)erred [24] | 109.82 ns | 1.795 ns |  2.458 ns | 109.40 ns |  0.98 |    0.03 |    6 |         - |
|    StartWith_InvariantCultureIgnoreCase | Kobo.(...)erred [24] | 111.65 ns | 2.345 ns |  6.689 ns | 110.56 ns |  0.97 |    0.05 |    6 |         - |
|                               StartWith | Kobo.(...)erred [24] | 112.37 ns | 2.259 ns |  3.015 ns | 111.33 ns |  1.00 |    0.00 |    6 |         - |
| StartWith_IgnoreCaseTrue_CurrentCulture | Kobo.(...)erred [24] | 119.30 ns | 2.402 ns |  4.798 ns | 117.54 ns |  1.08 |    0.06 |    7 |         - |
|                     IsPrefix_IgnoreCase | Kobo.(...)erred [24] | 119.81 ns | 2.790 ns |  8.051 ns | 116.64 ns |  1.11 |    0.09 |    7 |         - |
|                                         |                      |           |          |           |           |       |         |      |           |
|                       StartWith_Ordinal | Loyal(...)Cares [29] |  38.91 ns | 0.909 ns |  2.623 ns |  38.28 ns |  0.12 |    0.01 |    1 |         - |
|                        IsPrefix_Ordinal | Loyal(...)Cares [29] |  57.52 ns | 1.184 ns |  1.809 ns |  57.04 ns |  0.18 |    0.01 |    2 |         - |
|             StartWith_OrdinalIgnoreCase | Loyal(...)Cares [29] |  83.70 ns | 1.514 ns |  2.571 ns |  83.62 ns |  0.26 |    0.01 |    3 |         - |
|              IsPrefix_OrdinalIgnoreCase | Loyal(...)Cares [29] | 112.63 ns | 2.261 ns |  4.018 ns | 110.99 ns |  0.34 |    0.02 |    4 |         - |
|                                IsPrefix | Loyal(...)Cares [29] | 282.70 ns | 5.663 ns |  9.462 ns | 279.42 ns |  0.87 |    0.05 |    5 |         - |
|                     IsPrefix_IgnoreCase | Loyal(...)Cares [29] | 298.05 ns | 5.822 ns |  5.446 ns | 296.85 ns |  0.92 |    0.03 |    6 |         - |
|                               StartWith | Loyal(...)Cares [29] | 325.47 ns | 6.254 ns | 16.143 ns | 321.66 ns |  1.00 |    0.00 |    7 |         - |
|                     StartWith_Invariant | Loyal(...)Cares [29] | 340.87 ns | 6.798 ns | 11.359 ns | 337.98 ns |  1.05 |    0.06 |    8 |         - |
| StartWith_IgnoreCaseTrue_CurrentCulture | Loyal(...)Cares [29] | 345.18 ns | 8.966 ns | 26.012 ns | 336.54 ns |  1.06 |    0.09 |    8 |         - |
|    StartWith_InvariantCultureIgnoreCase | Loyal(...)Cares [29] | 357.34 ns | 7.033 ns | 11.357 ns | 354.03 ns |  1.09 |    0.07 |    9 |         - |
