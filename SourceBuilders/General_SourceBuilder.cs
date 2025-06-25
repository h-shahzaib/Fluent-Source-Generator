using Flynth.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Flynth.SourceBuilders
{
    public static partial class SourceBuilder
    {
        public class General
        {
            private readonly int m_IndentLevel = 0;

            private readonly List<IToken> m_Tokens = new List<IToken>();

            public SourceBuilderOptions Options
            {
                get
                {
                    if (m_Options == null)
                        Options = SourceBuilderOptions.Default;
                    return m_Options;
                }
                set => m_Options = new SourceBuilderOptions(value);
            }
            private SourceBuilderOptions m_Options;

            public void Token(IToken token)
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

            public override string ToString()
            {
                var new_line = Environment.NewLine;

                var string_builder = new StringBuilder();

                for (int i = 0; i < m_Tokens.Count; i++)
                {
                    var token = m_Tokens[i];
                    var next_token = m_Tokens.ElementAtOrDefault(i + 1);

                    var next_token_not_empty =
                        (next_token is LineToken next_line_token
                            && !string.IsNullOrWhiteSpace(next_line_token.Line)) ||
                        (next_token is LinesToken next_lines_token
                            && next_lines_token.Lines.Length > 0
                            && !string.IsNullOrWhiteSpace(next_lines_token.Lines[0])
                        );

                    var tabs = string.Empty;
                    var token_options = (token as BaseToken).Options;
                    if (m_IndentLevel != 0 && token_options.NumberOfSpacesInOneTab > 0)
                        tabs = new string(' ', token_options.NumberOfSpacesInOneTab);

                    if (i != 0)
                        string_builder.AppendLine();
                    string_builder.Append(tabs + token.ToString().Replace(new_line, $"{new_line}{tabs}"));

                    if (token is LineToken line_token)
                    {
                        if (i != m_Tokens.Count - 1 && !string.IsNullOrWhiteSpace(line_token.Line) && !(next_token is LineToken) && next_token_not_empty)
                            string_builder.AppendLine();
                    }
                    else if (token is LinesToken lines_token)
                    {
                        if (i != m_Tokens.Count - 1 && lines_token.Lines.Length > 0 && !string.IsNullOrWhiteSpace(lines_token.Lines[lines_token.Lines.Length - 1]) && next_token_not_empty)
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
}
