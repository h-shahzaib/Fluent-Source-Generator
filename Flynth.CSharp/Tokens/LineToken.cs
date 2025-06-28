using Flynth.CSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flynth.CSharp.Tokens
{
    public class LineToken : Token
    {
        public string Line { get; } = string.Empty;

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
