using Flynth.CSharp;
using System;
using System.Text;

namespace Flynth.CSharp.Tokens
{
    public class BlockToken : Token
    {
        public string Header { get; set; }
        public SourceBuilder SourceBuilder { get; set; }

        public BlockToken(Options options, string header, SourceBuilder source_builder) : base(options)
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
