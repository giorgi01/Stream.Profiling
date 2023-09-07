namespace Stream.Profiling
{
    internal struct Line : IComparable<Line>
    {
        private readonly int _pos;
        private readonly string _line;

        public int Number { get; set; }
        public ReadOnlySpan<char> Word => _line.AsSpan(_pos + 2);

        public Line(string line)
        {
            _pos = line.IndexOf('.');
            _line = line;
            Number = int.Parse(line.AsSpan(0, _pos));
        }

        public string Build() => _line;

        public int CompareTo(Line other)
        {
            var result = Word.CompareTo(other.Word, StringComparison.Ordinal);
            return result != 0 ? result : Number.CompareTo(other.Number);
        }
    }

}
