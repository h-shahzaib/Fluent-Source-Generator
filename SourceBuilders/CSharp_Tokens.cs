using Flynth.Core;
using Flynth.Tokens;
using System;
using System.Text;

namespace Flynth.SourceBuilders
{
    public static partial class SourceBuilder
    {
        public partial class CSharp
        {
            private class BlockToken : BaseToken
            {
                public string Header { get; set; }
                public CSharp SourceBuilder { get; set; }

                public BlockToken(SourceBuilderOptions options, string header, CSharp source_builder) : base(options)
                {
                    Header = header;
                    SourceBuilder = source_builder;
                }

                public override string ToString()
                {
                    var output = new StringBuilder();

                    var new_line = Environment.NewLine;
                    output.Append(ApplyReplacements(Header));
                    output.Append($"{new_line}{{");
                    output.Append($"{new_line}{SourceBuilder}");
                    output.Append($"{new_line}}}");

                    return output.ToString();
                }
            }
        }
    }
}
