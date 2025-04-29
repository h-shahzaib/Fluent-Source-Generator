using FluentSourceGenerator.CSharp;
using FluentSourceGenerator.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentSourceGenerator.Tokens
{
    public class BlockToken : BaseToken
    {
        public string Header { get; set; }
        public CSharp_SourceBuilder SourceBuilder { get; set; }

        public BlockToken(SourceBuilderOptions options, CSharp_SourceBuilder source_builder) : base(options)
        {
            SourceBuilder = source_builder;
        }

        public BlockToken(SourceBuilderOptions options, string header, CSharp_SourceBuilder source_builder) : base(options)
        {
            Header = header;
            SourceBuilder = source_builder;
        }

        public override string ToString()
        {
            var output = string.Empty;

            var new_line = Options.NewLine;
            var tabs = new string(' ', Options.NumberOfSpacesInOneTab);
            var header = Header.ApplyReplacements(Options.GetCharReplacements());

            if (!string.IsNullOrWhiteSpace(header))
                output += header;
            output += $"{new_line}{{";
            output += $"{new_line}{tabs}{SourceBuilder.ToString().Replace(new_line, $"{new_line}{tabs}")}";
            output += $"{new_line}}}";

            return output;
        }
    }
}
