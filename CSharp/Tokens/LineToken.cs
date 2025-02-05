using FluentSourceGenerator.CSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentSourceGenerator.CSharp.Tokens
{
    internal class LineToken : BaseToken
    {
        public string Line { get; set; }

        public LineToken(SourceBuilderOptions options) : base(options)
        {

        }

        public LineToken(SourceBuilderOptions options, string line) : base(options)
        {
            Line = line;
        }

        public override string ToString()
        {
            return Line.ApplyReplacements(Options.GetCharReplacements());
        }
    }
}
