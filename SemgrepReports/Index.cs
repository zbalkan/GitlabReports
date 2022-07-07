using System;
using System.Collections.Generic;

namespace SemgrepReports
{
    internal sealed class Index
    {
        private static readonly Lazy<Index> Lazy = new(() => new Index());

        private readonly List<Tuple<string, int>> _sections;

        internal static Index Instance => Lazy.Value;

        public IReadOnlyList<Tuple<string, int>> Sections => _sections.AsReadOnly();

        public Index()
        {
            _sections = new List<Tuple<string, int>>();
        }

        public void AddSection(string section, int level)
        {
            _sections.Add(new Tuple<string, int>(section, level));
        }
    }
}
