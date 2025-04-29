using FluentSourceGenerator.CSharp;
using FluentSourceGenerator.Options;
using FluentSourceGenerator.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentSourceGenerator.General
{
    public class SourceBuilder
    {
        private readonly List<IToken> m_Tokens = new List<IToken>();

        public SourceBuilderOptions ChildOptions
        {
            get
            {
                if (m_ChildOptions == null)
                    ChildOptions = SourceBuilderOptions.Default;
                return m_ChildOptions;
            }
            set => m_ChildOptions = value.Clone();
        }
        private SourceBuilderOptions m_ChildOptions;

        public void Token(IToken token)
        {
            m_Tokens.Add(token);
        }

        public void Line(string line = "")
        {
            m_Tokens.Add(new LineToken(ChildOptions, line));
        }

        public void Lines(params string[] lines)
        {
            m_Tokens.Add(new LinesToken(ChildOptions, lines));
        }

        public override string ToString()
        {
            var string_builder = new StringBuilder();
            for (int i = 0; i < m_Tokens.Count; i++)
            {
                var token = m_Tokens[i];

                if (token is BlockToken || token is LinesToken)
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
