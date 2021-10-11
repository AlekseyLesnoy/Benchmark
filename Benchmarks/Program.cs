using BenchmarkDotNet.Running;

namespace Benchmarks
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //var summary = BenchmarkRunner.Run<InverseSquareRoot>();
            //var summary = BenchmarkRunner.Run<EnumToString>();
            //var summary = BenchmarkRunner.Run<CountLines>();
            var summary = BenchmarkRunner.Run<ProductArrayCalculator>();
        }
    }
}