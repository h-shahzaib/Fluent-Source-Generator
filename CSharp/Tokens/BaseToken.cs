using FluentSourceGenerator.CSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentSourceGenerator.CSharp.Tokens
{
    internal abstract class BaseToken : IToken
    {
        public SourceBuilderOptions Options { get; }

        protected BaseToken(SourceBuilderOptions options)
        {
            Options = options.Clone();
        }

        public abstract override string ToString();
    }
}
