﻿using Flynth.CSharp.Tokens;
using Flynth.CSharp.Tokens.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Flynth.CSharp
{
    public class SourceBuilder
    {
        private readonly List<Token> m_Tokens = [];

        public bool IsRootBuilder { get; set; } = true;

        public Options Options
        {
            get
            {
                if (m_Options == null)
                    Options = Options.Default;
                return m_Options!;
            }
            set => m_Options = new Options(value);
        }
        private Options? m_Options;

        public void Token(Token token)
        {
            m_Tokens.Add(token);
        }

        public void Line(string line = "")
        {
            m_Tokens.Add(new LineToken(Options, line));
        }

        public void Lines(params string[] lines)
        {
            m_Tokens.Add(new LinesToken(Options, lines));
        }

        public void Using(string statement)
        {
            m_Tokens.Add(new LineToken(Options, $"using {statement};"));
        }

        public void Namespace(string @namespace, Action<SourceBuilder> source_builder_act)
        {
            var source_builder = new SourceBuilder();
            source_builder.Options = Options;
            source_builder.IsRootBuilder = false;
            source_builder_act.Invoke(source_builder);
            m_Tokens.Add(new BlockNode(Options, $"namespace {@namespace}", source_builder));
        }

        public void Class(string modifiers, string class_name, Action<SourceBuilder> source_builder_act)
        {
            var source_builder = new SourceBuilder();
            source_builder.Options = Options;
            source_builder.IsRootBuilder = false;
            source_builder_act.Invoke(source_builder);
            m_Tokens.Add(new BlockNode(Options, $"{(!string.IsNullOrWhiteSpace(modifiers) ? $"{modifiers} " : string.Empty)}class {class_name}", source_builder));
        }

        public void Method(string modifiers, string return_type, string method_name, string parameters, Action<SourceBuilder> source_builder_act)
        {
            var source_builder = new SourceBuilder();
            source_builder.Options = Options;
            source_builder.IsRootBuilder = false;
            source_builder_act.Invoke(source_builder);
            m_Tokens.Add(new BlockNode(Options, $"{(!string.IsNullOrWhiteSpace(modifiers) ? $"{modifiers} " : string.Empty)}{return_type} {method_name}({parameters})", source_builder));
        }

        public IfElseNode If(string condition, Action<SourceBuilder> source_builder_act)
        {
            if (string.IsNullOrWhiteSpace(condition))
                throw new ArgumentException("Condition for `If` statement cannot be null, empty or whitespace.", nameof(condition));

            var source_builder = new SourceBuilder();
            source_builder.Options = Options;
            source_builder.IsRootBuilder = false;
            source_builder_act.Invoke(source_builder);

            var node = new IfElseNode(Options, condition, source_builder);
            m_Tokens.Add(node);
            return node;
        }

        public override string ToString()
        {
            var new_line = Environment.NewLine;

            var string_builder = new StringBuilder();

            for (int i = 0; i < m_Tokens.Count; i++)
            {
                var token = m_Tokens[i];
                var next_token = m_Tokens.ElementAtOrDefault(i + 1);

                var next_token_not_empty =
                    next_token is LineToken next_line_token
                        && !string.IsNullOrWhiteSpace(next_line_token.Line) ||
                    next_token is LinesToken next_lines_token
                        && next_lines_token.Lines.Length > 0
                        && !string.IsNullOrWhiteSpace(next_lines_token.Lines[0]);

                var tabs = string.Empty;
                if (!IsRootBuilder && token.Options.NumberOfSpacesInOneTab > 0)
                    tabs = new string(' ', token.Options.NumberOfSpacesInOneTab);

                if (i != 0)
                    string_builder.AppendLine();
                string_builder.Append(tabs + token.ToString().Replace(new_line, $"{new_line}{tabs}"));

                if (token is NodeToken)
                {
                    if (i != m_Tokens.Count - 1 && (next_token is NodeToken || next_token_not_empty))
                        string_builder.AppendLine();
                }
                else if (token is LineToken line_token)
                {
                    if (i != m_Tokens.Count - 1 && !string.IsNullOrWhiteSpace(line_token.Line) && next_token is not LineToken && (next_token is NodeToken || next_token_not_empty))
                        string_builder.AppendLine();
                }
                else if (token is LinesToken lines_token)
                {
                    if (i != m_Tokens.Count - 1 && lines_token.Lines.Length > 0 && !string.IsNullOrWhiteSpace(lines_token.Lines[lines_token.Lines.Length - 1]) && (next_token is NodeToken || next_token_not_empty))
                        string_builder.AppendLine();
                }
                else
                {
                    throw new InvalidOperationException(
                        $"Unsupported token type '{token.GetType().Name}' encountered in SourceBuilder."
                    );
                }
            }

            return string_builder.ToString();
        }
    }
}
