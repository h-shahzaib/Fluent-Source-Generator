using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace FluentSourceGenerator
{
    public static class Extensions
    {
        public static string ApplyReplacements(this string str, IDictionary<char, char> replacements)
        {
            if (string.IsNullOrWhiteSpace(str))
                return str;

            var output = str;

            foreach (var replacement in replacements)
            {
                output = output.Replace(replacement.Key, replacement.Value);
            }

            return output;
        }

        public static T Clone<T>(this T source)
        {
            if (!typeof(T).IsSerializable)
            {
                throw new ArgumentException("The type must be serializable.", nameof(source));
            }

            if (source == null)
                return default;

            using (var stream = new MemoryStream())
            {
                IFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, source);
                stream.Seek(0, SeekOrigin.Begin);
                return (T)formatter.Deserialize(stream);
            }
        }
    }
}
