using Flynth.SQL.Select.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace Flynth.SQL.Select
{
    public class SelectBuilder<T, TResult>
    {
        public SelectToken<T, TResult>? SelectToken { get; set; } = null;
        public FromToken<T>? FromToken { get; set; } = null;
    }
}
