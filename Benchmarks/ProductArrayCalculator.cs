using System.Collections.Generic;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;

namespace Benchmarks
{
    [MemoryDiagnoser]
    [Orderer(SummaryOrderPolicy.FastestToSlowest)]
    [RankColumn]
    public class ProductArrayCalculator
    {
        public static IEnumerable<int[]> Arrays()
        {
            yield return new[] { 2, 4, 8, 16 };
            yield return new[] { 2, 4, 8, 16, 6, 10, 144, 10, 4, 7, 2, 56, 245, 23, 54, 85, 12, 6898, 2567, 25, 4, 1, 5798,  6, 10, 144, 10, 4, 7 };
        }

        [Benchmark(Baseline = true)]
        [ArgumentsSource(nameof(Arrays))]
        public int[] CalculateProductArray_Basic(int[] inputArray)
        {
            var retVal = new int[inputArray.Length];
            for (int i = 0; i < inputArray.Length; i++)
            {
                var value = 0;

                for (int j = 0; j < inputArray.Length; j++)
                {
                    if (j==i)
                        continue;

                    value = value == 0 ? inputArray[j] : value *= inputArray[j];
                }

                retVal[i] = value;
            }
            
            return retVal;
        }

        [Benchmark]
        [ArgumentsSource(nameof(Arrays))]
        public int[] CalculateProductArray_Improved(int[] inputArray)
        {
            int cumulativeProduct = 1;
            int[] leftProduct = new int[inputArray.Length];

            for(int i = 0; i < inputArray.Length; i++)
            {
                cumulativeProduct = cumulativeProduct * inputArray[i];
                leftProduct[i] = cumulativeProduct;
            }

            cumulativeProduct = 1;
            int[] rightProduct = new int[inputArray.Length];

            for(int i = inputArray.Length - 1; i >= 0; i--)
            {
                cumulativeProduct = cumulativeProduct * inputArray[i];
                rightProduct[i] = cumulativeProduct;
            }

            int[] outputArray = new int[inputArray.Length];
            for(int i = 0; i < outputArray.Length; i++)
            {
                if(i == 0)
                    outputArray[i] = rightProduct[i + 1];
                else if(i == inputArray.Length - 1)
                    outputArray[i] = leftProduct[inputArray.Length - 2];
                else
                    outputArray[i] = leftProduct[i - 1] * rightProduct[i + 1];
            }

            return outputArray;
        }
    }
}