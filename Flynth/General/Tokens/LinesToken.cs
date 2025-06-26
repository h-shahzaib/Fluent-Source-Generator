using Flynth.General;
using System;

namespace Flynth.General.Tokens
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
