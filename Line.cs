using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stream.Profiling
{
    internal class Line : IComparable<Line>
    {
        public int Number { get; set; }
        public string Word { get; set; }

        public Line(string line)
        {
            var pos = line.IndexOf('.');
            Number = int.Parse(line[..pos]);
            Word = line[(pos + 2)..];
        }

        public string Build() => $"{Number}. {Word}";

        public int CompareTo(Line other)
        {
            var result = Word.CompareTo(other.Word);
            return result != 0 ? result : Number.CompareTo(other.Number);
        }
    }

}
