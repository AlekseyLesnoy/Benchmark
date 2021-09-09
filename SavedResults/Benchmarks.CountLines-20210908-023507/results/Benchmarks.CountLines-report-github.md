``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19042.1083 (20H2/October2020Update)
Intel Core i7-7820HQ CPU 2.90GHz (Kaby Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK=5.0.301
  [Host]     : .NET 5.0.7 (5.0.721.25508), X64 RyuJIT
  DefaultJob : .NET 5.0.7 (5.0.721.25508), X64 RyuJIT


```
|                           Method |                 file |      Mean |     Error |    StdDev | Ratio | RatioSD | Rank |      Gen 0 |    Gen 1 |    Gen 2 |  Allocated |
|--------------------------------- |--------------------- |----------:|----------:|----------:|------:|--------:|-----:|-----------:|---------:|---------:|-----------:|
|             CSharpCodeCountLines | ./Tes(...)2.txt [27] | 108.98 ms |  2.358 ms |  6.877 ms |  0.55 |    0.04 |    1 |   200.0000 | 200.0000 | 200.0000 |   1,024 KB |
|             NativeBashCountLines | ./Tes(...)2.txt [27] | 199.86 ms |  3.996 ms |  7.308 ms |  1.00 |    0.00 |    2 |          - |        - |        - |     143 KB |
|       CSharpCodeCountLinesSimple | ./Tes(...)2.txt [27] | 236.49 ms |  3.305 ms |  2.760 ms |  1.19 |    0.04 |    3 | 41333.3333 |        - |        - | 169,951 KB |
| NativeBashCountLinesOutputToFile | ./Tes(...)2.txt [27] | 779.65 ms | 15.588 ms | 37.943 ms |  3.88 |    0.22 |    4 |          - |        - |        - |     143 KB |
|                                  |                      |           |           |           |       |         |      |            |          |          |            |
|             CSharpCodeCountLines | ./Tes(...)4.txt [26] |  56.22 ms |  1.201 ms |  3.464 ms |  0.28 |    0.02 |    1 |   300.0000 | 300.0000 | 300.0000 |   1,024 KB |
|       CSharpCodeCountLinesSimple | ./Tes(...)4.txt [26] | 124.61 ms |  2.490 ms |  5.413 ms |  0.63 |    0.03 |    2 | 21000.0000 |        - |        - |  86,129 KB |
|             NativeBashCountLines | ./Tes(...)4.txt [26] | 197.28 ms |  3.849 ms |  4.118 ms |  1.00 |    0.00 |    3 |          - |        - |        - |     145 KB |
| NativeBashCountLinesOutputToFile | ./Tes(...)4.txt [26] | 474.67 ms |  9.466 ms | 22.125 ms |  2.42 |    0.10 |    4 |          - |        - |        - |     143 KB |
