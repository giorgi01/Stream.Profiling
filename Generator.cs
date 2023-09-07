using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stream.Profiling
{
    internal sealed class Generator
    {
        private readonly Random _random = new();
        private readonly string[] _words;

        public Generator()
        {
            _words = Enumerable.Range(0, 10000)
                .Select(
                    _ =>
                    {
                        var range = Enumerable.Range(0, _random.Next(20, 100));
                        var chars = range.Select(_ => (char)_random.Next('A', 'Z')).ToArray();
                        return new string(chars);
                    }).ToArray();
        }

        public string Generate(int linesCount)
        {
            var fileName = "L" + linesCount + ".txt";
            using var writer = new StreamWriter(fileName);
            for (var i = 0; i < linesCount; i++)
            {
                writer.WriteLine(GenerateNumber() + "." + GenerateString());
            }

            return fileName;
        }

        private string GenerateString() => _words[_random.Next(0, _words.Length)];

        private string GenerateNumber() => _random.Next(0, 10000).ToString();
    }

}
