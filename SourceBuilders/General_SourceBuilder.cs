using Flynth;
using Flynth.Core;
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
                var string_builder = new StringBuilder();

                for (int i = 0; i < m_Tokens.Count; i++)
                {
                    var token = m_Tokens[i];

                    if (token is LinesToken)
                    {
                        if (i != 0)
                            string_builder.AppendLine();
                        string_builder.Append(token.ToString());
                        if (i != m_Tokens.Count - 1)
                            string_builder.AppendLine();
                    }
                    else
                    {
                        if (i != 0)
                            string_builder.AppendLine();
                        string_builder.Append(token.ToString());
                        if (i != m_Tokens.Count - 1 && !(m_Tokens.ElementAtOrDefault(i + 1) is LineToken))
                            string_builder.AppendLine();
                    }
                }

                return string_builder.ToString();
            }
        }
    }
}
