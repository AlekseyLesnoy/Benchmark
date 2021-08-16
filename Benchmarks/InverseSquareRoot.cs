using System;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Diagnostics.Windows.Configs;
using BenchmarkDotNet.Order;

namespace Benchmarks
{
    /// <summary>
    /// Fast Inverse Square Root — A Quake III Algorithm:
    /// https://www.youtube.com/watch?v=p8u_k2LIZyo
    /// </summary>
    [NativeMemoryProfiler]
    [MemoryDiagnoser]
    [Orderer(SummaryOrderPolicy.FastestToSlowest)]
    [RankColumn]
    [RPlotExporter]
    public class InverseSquareRoot
    {
        public static IEnumerable<float> Numbers()
        {
            //yield return 1.5f; //toggle comment to have more variables in test (this makes everything runs twice)
            yield return 0.5f;
        }

        /// <summary>
        ///     expected to be the slowest
        /// </summary>
        [Benchmark(Baseline = true)]
        [ArgumentsSource(nameof(Numbers))]
        public float RegularInverseSquareRoot(float x)
        {
            var invSqrt = (float) (1/Math.Sqrt(x));
            return invSqrt;
        }

        /// <summary>
        ///     Expected to be faster the default implementation - RegularInverseSquareRoot
        /// </summary>
        [Benchmark]
        [ArgumentsSource(nameof(Numbers))]
        public float FastInverseSquareRoot(float x)
        {
            float xhalf = 0.5f * x;
            int i = BitConverter.SingleToInt32Bits(x);
            i = 0x5f3759df - (i >> 1);
            x = BitConverter.Int32BitsToSingle(i);
            x = x * (1.5f - xhalf * x * x);
            return x;
        }
        
        /// <summary>
        ///     Expected to be slower then FastInverseSquareRoot since .net core is more perf. optimized 
        /// </summary>
        [Benchmark]
        [ArgumentsSource(nameof(Numbers))]
        public float PreNetCore_FastInverseSquareRoot(float x)
        {
            float xhalf = 0.5f * x;
            int i = BitConverter.ToInt32(BitConverter.GetBytes(x), 0);
            i = 0x5f3759df - (i >> 1);
            x = BitConverter.ToSingle(BitConverter.GetBytes(i), 0);
            x = x * (1.5f - xhalf * x * x);
            return x;
        }
        
        /*/// <summary>
        ///     Expected to be more accurate but same speed as FastInverseSquareRoot, since 0x5f375a86 is a revised mo 
        /// </summary>
        [Benchmark]
        [ArgumentsSource(nameof(Numbers))]
        public float UpdatedFastInverseSquareRoot(float x)
        {
            float xhalf = 0.5f * x;
            int i = BitConverter.SingleToInt32Bits(x);
            i = 0x5f375a86 - (i >> 1);
            x = BitConverter.Int32BitsToSingle(i);
            x = x * (1.5f - xhalf * x * x);
            return x;
        }*/
    }
}
