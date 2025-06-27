using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Flynth.SQL.Select.Tokens
{
    public class FromToken<T>
    {
        public FromToken(string table_name)
        {
            TableName = table_name;
        }

        public string TableName { get; }
    }
}
