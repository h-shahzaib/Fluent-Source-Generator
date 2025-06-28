using System;
using System.Collections.Generic;
using System.Text;

namespace Flynth.CSharp.Tokens.Nodes
{
    public class IfElseNode : NodeToken
    {
        private readonly Options m_Options;

        public IfElseNode(Options options, string condition, SourceBuilder source_builder) : base(options)
        {
            if (string.IsNullOrWhiteSpace(condition))
                throw new ArgumentException("Condition for `If` statement cannot be null, empty or whitespace.", nameof(condition));

            m_Options = options;
            Blocks.Add(new(options, $"if ({condition})", source_builder));
        }

        public List<BlockNode> Blocks { get; } = [];

        public IfElseNode ElseIf(string condition, Action<SourceBuilder> source_builder_act)
        {
            if (string.IsNullOrWhiteSpace(condition))
                throw new ArgumentException("Condition for `If` statement cannot be null, empty or whitespace.", nameof(condition));

            var source_builder = new SourceBuilder();
            source_builder.Options = Options;
            source_builder.IsRootBuilder = false;
            source_builder_act.Invoke(source_builder);
            Blocks.Add(new(m_Options, $"else if ({condition ?? string.Empty})", source_builder));

            return this;
        }

        public void Else(Action<SourceBuilder> source_builder_act)
        {
            var source_builder = new SourceBuilder();
            source_builder.Options = Options;
            source_builder.IsRootBuilder = false;
            source_builder_act.Invoke(source_builder);
            Blocks.Add(new(m_Options, $"else", source_builder));
        }

        public override string ToString()
        {
            var output = new StringBuilder();

            var new_line = Environment.NewLine;
            for (int i = 0; i < Blocks.Count; i++)
            {
                var block = Blocks[i];
                
                output.Append(ApplyReplacements(block.Header));
                output.Append($"{new_line}{{");
                output.Append($"{new_line}{block.SourceBuilder}");
                output.Append($"{new_line}}}");

                if (i + 1 < Blocks.Count)
                    output.AppendLine();
            }

            return output.ToString();
        }
    }
}
