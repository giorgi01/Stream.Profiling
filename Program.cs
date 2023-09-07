using System;

namespace Stream.Profiling
{

    internal class Program
    {
        static void Main(string[] args)
        {
            var gen = new Generator();
            gen.Generate(1000000);

            var sorter = new Sorter();
            sorter.Sort("L1000000.txt");
        }
    }
}