using System;
using System.Collections.Generic;
using System.Globalization;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Order;

namespace MultiFrameworkBenchmarks
{
    public class StringComparisonMethods
    {
        public static bool IsKoboAssembly(string assemblyName)
        {
            return assemblyName.StartsWith("Kobo.") ||
                   assemblyName.StartsWith("Library.") ||
                   assemblyName.StartsWith("Loyalty.");
        }

        public static bool IsKoboAssembly_IgnoreCaseTrue_CurrentCulture(string assemblyName)
        {
            return assemblyName.StartsWith("Kobo.", true, CultureInfo.CurrentCulture) ||
                   assemblyName.StartsWith("Library.", true, CultureInfo.CurrentCulture) ||
                   assemblyName.StartsWith("Loyalty.", true, CultureInfo.CurrentCulture);
        }

        public static bool IsKoboAssembly_OrdinalIgnoreCase(string assemblyName)
        {
            return assemblyName.StartsWith("Kobo.", StringComparison.OrdinalIgnoreCase) ||
                   assemblyName.StartsWith("Library.", StringComparison.OrdinalIgnoreCase) ||
                   assemblyName.StartsWith("Loyalty.", StringComparison.OrdinalIgnoreCase);
        }

        public static bool IsKoboAssembly_Ordinal(string assemblyName)
        {
            return assemblyName.StartsWith("Kobo.", StringComparison.Ordinal) ||
                   assemblyName.StartsWith("Library.", StringComparison.Ordinal) ||
                   assemblyName.StartsWith("Loyalty.", StringComparison.Ordinal);
        }

        public static bool IsKoboAssembly_InvariantCultureIgnoreCase(string assemblyName)
        {
            return assemblyName.StartsWith("Kobo.", StringComparison.InvariantCultureIgnoreCase) ||
                   assemblyName.StartsWith("Library.", StringComparison.InvariantCultureIgnoreCase) ||
                   assemblyName.StartsWith("Loyalty.", StringComparison.InvariantCultureIgnoreCase);
        }

        public static bool IsKoboAssembly_Invariant(string assemblyName)
        {
            return assemblyName.StartsWith("Kobo.", StringComparison.InvariantCulture) ||
                   assemblyName.StartsWith("Library.", StringComparison.InvariantCulture) ||
                   assemblyName.StartsWith("Loyalty.", StringComparison.InvariantCulture);
        }

        public static bool IsKoboAssemblyIsPrefix(string assemblyName)
        {
            var ci = CultureInfo.CurrentCulture.CompareInfo;

            return ci.IsPrefix(assemblyName, "Kobo.") ||
                   ci.IsPrefix(assemblyName, "Library.") ||
                   ci.IsPrefix(assemblyName, "Loyalty.");
        }

        public static bool IsKoboAssemblyIsPrefix_Ordinal(string assemblyName)
        {
            var ci = CultureInfo.CurrentCulture.CompareInfo;

            return ci.IsPrefix(assemblyName, "Kobo.", CompareOptions.Ordinal) ||
                   ci.IsPrefix(assemblyName, "Library.", CompareOptions.Ordinal) ||
                   ci.IsPrefix(assemblyName, "Loyalty.", CompareOptions.Ordinal);
        }

        public static bool IsKoboAssemblyIsPrefix_OrdinalIgnoreCase(string assemblyName)
        {
            var ci = CultureInfo.CurrentCulture.CompareInfo;

            return ci.IsPrefix(assemblyName, "Kobo.", CompareOptions.OrdinalIgnoreCase) ||
                   ci.IsPrefix(assemblyName, "Library.", CompareOptions.OrdinalIgnoreCase) ||
                   ci.IsPrefix(assemblyName, "Loyalty.", CompareOptions.OrdinalIgnoreCase);
        }
        
        public static bool IsKoboAssemblyIsPrefix_IgnoreCase(string assemblyName)
        {
            var ci = CultureInfo.CurrentCulture.CompareInfo;

            return ci.IsPrefix(assemblyName, "Kobo.", CompareOptions.IgnoreCase) ||
                   ci.IsPrefix(assemblyName, "Library.", CompareOptions.IgnoreCase) ||
                   ci.IsPrefix(assemblyName, "Loyalty.", CompareOptions.IgnoreCase);
        }
    }

    [SimpleJob(RuntimeMoniker.Net472)]
    [MemoryDiagnoser]
    [Orderer(SummaryOrderPolicy.FastestToSlowest)]
    [RankColumn]
    public class StartWithOrIsPrefix
    {
        public static IEnumerable<string> Strings()
        {
            yield return "Kobo.Purchasing.Deferred";
            yield return "Loyalty.Whatever.No.One.Cares";
        }
        
        [Benchmark(Baseline = true)]
        [ArgumentsSource(nameof(Strings))]
        public bool StartWith(string assemblyName)
        {
            return StringComparisonMethods.IsKoboAssembly(assemblyName);
        }
        
        [Benchmark]
        [ArgumentsSource(nameof(Strings))]
        public bool StartWith_IgnoreCaseTrue_CurrentCulture(string assemblyName)
        {
            return StringComparisonMethods.IsKoboAssembly_IgnoreCaseTrue_CurrentCulture(assemblyName);
        }
        
        [Benchmark]
        [ArgumentsSource(nameof(Strings))]
        public bool StartWith_OrdinalIgnoreCase(string assemblyName)
        {
            return StringComparisonMethods.IsKoboAssembly_OrdinalIgnoreCase(assemblyName);
        }
        
        [Benchmark]
        [ArgumentsSource(nameof(Strings))]
        public bool StartWith_Ordinal(string assemblyName)
        {
            return StringComparisonMethods.IsKoboAssembly_Ordinal(assemblyName);
        }
        
        [Benchmark]
        [ArgumentsSource(nameof(Strings))]
        public bool StartWith_InvariantCultureIgnoreCase(string assemblyName)
        {
            return StringComparisonMethods.IsKoboAssembly_InvariantCultureIgnoreCase(assemblyName);
        }
        
        [Benchmark]
        [ArgumentsSource(nameof(Strings))]
        public bool StartWith_Invariant(string assemblyName)
        {
            return StringComparisonMethods.IsKoboAssembly_Invariant(assemblyName);
        }
        
        [Benchmark]
        [ArgumentsSource(nameof(Strings))]
        public bool IsPrefix(string assemblyName)
        {
            return StringComparisonMethods.IsKoboAssemblyIsPrefix(assemblyName);
        }
        
        [Benchmark]
        [ArgumentsSource(nameof(Strings))]
        public bool IsPrefix_Ordinal(string assemblyName)
        {
            return StringComparisonMethods.IsKoboAssemblyIsPrefix_Ordinal(assemblyName);
        }
        
        [Benchmark]
        [ArgumentsSource(nameof(Strings))]
        public bool IsPrefix_OrdinalIgnoreCase(string assemblyName)
        {
            return StringComparisonMethods.IsKoboAssemblyIsPrefix_OrdinalIgnoreCase(assemblyName);
        }
        
        [Benchmark]
        [ArgumentsSource(nameof(Strings))]
        public bool IsPrefix_IgnoreCase(string assemblyName)
        {
            return StringComparisonMethods.IsKoboAssemblyIsPrefix_IgnoreCase(assemblyName);
        }
    }
}