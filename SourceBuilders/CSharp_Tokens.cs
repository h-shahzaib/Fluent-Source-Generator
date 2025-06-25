using Flynth.Core;
using Flynth.Tokens;
using System;

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

                public BlockToken(SourceBuilderOptions options, CSharp source_builder) : base(options)
                {
                    SourceBuilder = source_builder;
                }

                public BlockToken(SourceBuilderOptions options, string header, CSharp source_builder) : base(options)
                {
                    Header = header;
                    SourceBuilder = source_builder;
                }

                public override string ToString()
                {
                    var new_line = Environment.NewLine;

                    var tabs = string.Empty;
                    if (Options.NumberOfSpacesInOneTab > 0)
                        tabs = new string(' ', Options.NumberOfSpacesInOneTab);

                    var output = string.Empty;

                    var header = ApplyReplacements(Header);
                    if (!string.IsNullOrWhiteSpace(header))
                        output += header;

                    output += $"{new_line}{{";
                    output += $"{new_line}{tabs}{SourceBuilder.ToString().Replace($"{new_line}", $"{new_line}{tabs}")}";
                    output += $"{new_line}}}";

                    return output;
                }
            }
        }
    }
}
