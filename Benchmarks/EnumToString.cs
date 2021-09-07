using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;

namespace Benchmarks
{
    [MemoryDiagnoser]
    [Orderer(SummaryOrderPolicy.FastestToSlowest)]
    [RankColumn]
    public class EnumToString
    {
        public enum Status
        {
            NotStarted,
            Done
        }
        
        /// <summary>
        /// Native ToString() does binary search, so expecting this to be slower then compiled nameof() 
        /// </summary>
        [Benchmark(Baseline = true)]
        public string NativeEnumToString()
        {
            return Status.Done.ToString();
        }
        
        [Benchmark]
        public string NameOfEnumToString()
        {
            return nameof(Status.Done);
        }
    }
}