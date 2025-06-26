using Flynth.SourceBuilder.CSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flynth.SourceBuilder.CSharp.Tokens
{
    public abstract class Token
    {
        public Options Options { get; }

        protected Token(Options options)
        {
            Options = new Options(options);
        }

        public abstract override string ToString();

        protected string ApplyReplacements(string str)
        {
            if (string.IsNullOrWhiteSpace(str))
                return str;

            var output = str;

            foreach (var replacement in Options.GetCharReplacements())
            {
                output = output.Replace(replacement.Key, replacement.Value);
            }

            return output;
        }
    }
}
