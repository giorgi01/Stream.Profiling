using System;
using System.Diagnostics;

namespace Stream.Profiling
{

    internal class Program
    {
        static void Main(string[] args)
        {
            var sw = Stopwatch.StartNew();

            var gen = new Generator();
            var fileName = gen.Generate(1_000_000);

            var sorter = new Sorter();
            sorter.Sort(fileName, 100_000);

            sw.Stop();
            Console.WriteLine($"Execution time: {sw.Elapsed}");
        }
    }
}