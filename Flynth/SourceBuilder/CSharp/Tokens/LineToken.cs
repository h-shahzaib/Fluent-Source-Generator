using Flynth.SourceBuilder.CSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flynth.SourceBuilder.CSharp.Tokens
{
    public class LineToken : Token
    {
        public string Line { get; set; }

        public LineToken(Options options) : base(options)
        {
        }

        public LineToken(Options options, string line) : base(options)
        {
            Line = line;
        }

        public override string ToString()
        {
            return ApplyReplacements(Line);
        }
    }
}
