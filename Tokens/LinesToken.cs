using FluentSourceGenerator.CSharp;
using FluentSourceGenerator.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentSourceGenerator.Tokens
{
    public class LinesToken : BaseToken
    {
        public string[] Lines { get; set; }

        public LinesToken(SourceBuilderOptions options, string[] lines) : base(options)
        {
            Lines = lines;
        }

        public override string ToString()
        {
            for (int i = 0; i < Lines.Length; i++)
                Lines[i] = Lines[i].ApplyReplacements(Options.GetCharReplacements());
            return string.Join(Options.NewLine, Lines);
        }
    }
}
