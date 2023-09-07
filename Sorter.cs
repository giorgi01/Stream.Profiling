using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stream.Profiling
{
    internal class Sorter
    {
        private class LineState
        {
            public required StreamReader Reader { get; init; }
            public required Line Line { get; set; }
        }

        public void Sort(string fileName, int singleFileLinesCount)
        {
            var files = SplitFile(fileName, singleFileLinesCount);
            SortLines(files);
            SortFiles(files);
        }

        private void SortFiles(string[] files)
        {
            var readers = files.Select(x => new StreamReader(x));
            try
            {
                var lines = readers.Select(x => new LineState
                {
                    Line = new Line(x.ReadLine()),
                    Reader = x
                }).ToList();

                using var writer = new StreamWriter("result.txt");
                while (lines.Count > 0)
                {
                    var current = lines.OrderBy(x => x.Line).First();
                    writer.WriteLine(current.Line.Build());

                    if (current.Reader.EndOfStream)
                    {
                        lines.Remove(current);
                        continue;
                    }

                    current.Line = new Line(current.Reader.ReadLine());
                }
            }
            finally
            {
                foreach (var r in readers)
                {
                    r.Dispose();
                }
            }
        }

        private void SortLines(string[] files)
        {
            foreach (var file in files)
            {
                var sortedLines = File.ReadAllLines(file)
                    .Select(x => new Line(x))
                    .OrderBy(x => x);
                File.WriteAllLines(file, sortedLines.Select(x => x.Build()));
            }
        }

        private string[] SplitFile(string fileName, int singleFileLinesCount)
        {
            var list = new List<string>();
            var partNumber = 0;
            using var reader = new StreamReader(fileName);

            while (!reader.EndOfStream)
            {
                partNumber++;
                var partFileName = partNumber + ".txt";
                list.Add(partFileName);

                using var writer = new StreamWriter(partFileName);
                for (var i = 0; i < singleFileLinesCount; i++)
                {
                    if (reader.EndOfStream) break;
                    writer.WriteLine(reader.ReadLine());
                }
            }

            return list.ToArray();
        }
    }

}
