using Flynth.SourceBuilder.General;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Flynth.SourceBuilder.CSharp
{
    public class Options
    {
        private readonly Dictionary<char, char> m_CharReplacements;

        public int NumberOfSpacesInOneTab { get; set; }

        public Options()
        {
            m_CharReplacements = new Dictionary<char, char>();
            NumberOfSpacesInOneTab = 4;
            RegisterCharReplacement('`', '"');
        }

        public Options(Options options)
        {
            m_CharReplacements = new Dictionary<char, char>(options.m_CharReplacements);
            NumberOfSpacesInOneTab = options.NumberOfSpacesInOneTab;
        }

        public void RegisterCharReplacement(char original_char, char replace_with)
        {
            if (original_char == ' ')
                throw new NotSupportedException("Cannot add replacement for `space` character.");

            m_CharReplacements[original_char] = replace_with;
        }

        public void RemoveCharReplacement(char original_char)
        {
            m_CharReplacements.Remove(original_char);
        }

        public ReadOnlyDictionary<char, char> GetCharReplacements()
        {
            return new ReadOnlyDictionary<char, char>(m_CharReplacements);
        }

        public static Options Default => new Options();
    }
}
