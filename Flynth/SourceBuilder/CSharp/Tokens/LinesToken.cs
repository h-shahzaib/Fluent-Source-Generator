using Flynth.SourceBuilder.CSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flynth.SourceBuilder.CSharp.Tokens
{
    public class LinesToken : Token
    {
        public string[] Lines { get; set; }

        public LinesToken(Options options, string[] lines) : base(options)
        {
            Lines = lines;
        }

        public override string ToString()
        {
            for (int i = 0; i < Lines.Length; i++)
                Lines[i] = ApplyReplacements(Lines[i]);
            return string.Join(Environment.NewLine, Lines);
        }
    }
}
