using Flynth.CSharp.Tokens;
using System;
using System.Text;

namespace Flynth.CSharp.Tokens.Nodes
{
    public class BlockNode(Options options, string header, SourceBuilder source_builder) : NodeToken(options)
    {
        public string Header { get; } = header;
        public SourceBuilder SourceBuilder { get; } = source_builder;

        public override string ToString()
        {
            var new_line = Environment.NewLine;

            var output = new StringBuilder();
            output.Append(ApplyReplacements(Header));
            output.Append($"{new_line}{{");
            output.Append($"{new_line}{SourceBuilder}");
            output.Append($"{new_line}}}");

            return output.ToString();
        }
    }
}
