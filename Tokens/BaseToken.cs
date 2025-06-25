using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flynth.Tokens
{
    public abstract class BaseToken : IToken
    {
        public SourceBuilderOptions Options { get; }

        protected BaseToken(SourceBuilderOptions options)
        {
            Options = new SourceBuilderOptions(options);
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
