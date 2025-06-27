using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Flynth.SQL.Select.Tokens
{
    public class SelectToken<T, TResult>
    {
        public SelectToken(Expression<Func<T, TResult>> expression)
        {
            SelectExpression = expression;
        }

        public Expression<Func<T, TResult>> SelectExpression { get; }
    }
}
