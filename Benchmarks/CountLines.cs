using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;

namespace Benchmarks
{
    [MemoryDiagnoser]
    [Orderer(SummaryOrderPolicy.FastestToSlowest)]
    [RankColumn]
    public class CountLines
    {
        private Process _p;
        const string unixPath = "/mnt/c/Projects/_Playground/Benchmark/Benchmarks/bin/Release/net5.0/TestData";
        
        public static IEnumerable<FileInfo> Files()
        {
            yield return new FileInfo("./TestData/first714584.txt");
            yield return new FileInfo("./TestData/first1410082.txt");
        }
        
        [GlobalSetup(Targets = new [] {nameof(NativeBashCountLines), nameof(NativeBashCountLinesOutputToFile)})]
        public void GlobalSetup()
        {
            _p = ShellHelper.CreateBashProcess();
        }

        [GlobalCleanup(Targets = new [] {nameof(NativeBashCountLines), nameof(NativeBashCountLinesOutputToFile)})]
        public void GlobalCleanup()
        {
            _p.Close();
        }

        /// <summary>
        /// Native "wc -l" bash command  
        /// </summary>
        [Benchmark(Baseline = true)]
        [ArgumentsSource(nameof(Files))]
        public void NativeBashCountLines(FileInfo file)
        {
            _p.Start();
            _p.StandardInput.WriteLine($"wc -l {unixPath}/{file.Name}");
            //_p.StandardInput.Flush();
            //_p.StandardInput.Close();
            _p.StandardInput.DisposeAsync();
            _p.WaitForExit();
        }
        
        [Benchmark]
        [ArgumentsSource(nameof(Files))]
        public void NativeBashCountLinesOutputToFile(FileInfo file)
        {
            _p.Start();
            _p.StandardInput.WriteLine($"wc -l {unixPath}/{file.Name} >> out.txt");
            _p.StandardInput.DisposeAsync();
            _p.WaitForExit();
        }

        [Benchmark]
        [ArgumentsSource(nameof(Files))]
        public void CSharpCodeCountLines(FileInfo file)
        {
            var fileInfo = new FileInfo(file.FullName);
            using var stream = fileInfo.OpenRead();
            stream.CountLines();
        }
        
        [Benchmark]
        [ArgumentsSource(nameof(Files))]
        public void CSharpCodeCountLinesSimple(FileInfo file)
        {
            File.ReadLines(file.FullName).Count();
        }
    }

    public static class ShellHelper
    {
        public static Process CreateBashProcess()
        {
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = @"cmd.exe",
                    Arguments = $"/c wsl.exe -d Ubuntu-20.04",
                    RedirectStandardOutput = true,
                    RedirectStandardInput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = false
                }
            };

            return process;
        }
    }

    public static class StreamExtensions
    {
        private const char CR = '\r';
        private const char LF = '\n';
        private const char NULL = (char)0;

        public static long CountLines(this Stream stream, Encoding encoding = default)
        {
            /*if (stream == null || stream.Length == 0 || stream == Stream.Null)
                Console.WriteLine("Stream is empty");*/
                
            var lineCount = 0L;
            var byteBuffer = new byte[1024 * 1024];
            var detectedEOL = NULL;
            var currentChar = NULL;
            int bytesRead;

            if (encoding is null || Equals(encoding, Encoding.ASCII) || Equals(encoding, Encoding.UTF8))
            {
                while ((bytesRead = stream.Read(byteBuffer, 0, byteBuffer.Length)) > 0)
                {
                    for (var i = 0; i < bytesRead; i++)
                    {
                        currentChar = (char)byteBuffer[i];

                        if (detectedEOL != NULL)
                        {
                            if (currentChar == detectedEOL)
                            {
                                lineCount++;
                            }
                        }
                        else if (currentChar == LF || currentChar == CR)
                        {
                            detectedEOL = currentChar;
                            lineCount++;
                        }
                    }
                }
            } 
            else
            {
                var charBuffer = new char[byteBuffer.Length];

                while ((bytesRead = stream.Read(byteBuffer, 0, byteBuffer.Length)) > 0)
                {
                    var charCount = encoding.GetChars(byteBuffer, 0, bytesRead, charBuffer, 0);

                    for (var i = 0; i < charCount; i++)
                    {
                        currentChar = charBuffer[i];

                        if (detectedEOL != NULL)
                        {
                            if (currentChar == detectedEOL)
                            {
                                lineCount++;
                            }
                        }
                        else if (currentChar == LF || currentChar == CR)
                        {
                            detectedEOL = currentChar;
                            lineCount++;
                        }
                    }
                }
            }

            if (currentChar != LF && currentChar != CR && currentChar != NULL)
            {
                lineCount++;
            }

            return lineCount;
        }
        
    }
    
}