using FluentSourceGenerator.CSharp;
using FluentSourceGenerator.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentSourceGenerator.Tokens
{
    public abstract class BaseToken : IToken
    {
        public SourceBuilderOptions Options { get; }

        protected BaseToken(SourceBuilderOptions options)
        {
            Options = options.Clone();
        }

        public abstract override string ToString();
    }
}
