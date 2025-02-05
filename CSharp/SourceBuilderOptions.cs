using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace FluentSourceGenerator.CSharp
{
    [Serializable]
    internal class SourceBuilderOptions
    {
        private readonly Dictionary<char, char> CharacterReplacements = new Dictionary<char, char>();

        public SourceBuilderOptions()
        {
            NumberOfSpacesInOneTab = 4;
            NewLine = Environment.NewLine;
            ///
            RegisterCharReplacement('`', '"');
        }

        public int NumberOfSpacesInOneTab { get; set; }
        public string NewLine { get; set; }

        public void RegisterCharReplacement(char original_char, char replace_with)
        {
            if (original_char == ' ')
                throw new NotSupportedException("Cannot add replacement for `space` character.");

            if (CharacterReplacements.ContainsKey(original_char))
                CharacterReplacements[original_char] = replace_with;
            else
                CharacterReplacements.Add(original_char, replace_with);
        }

        public void RemoveCharReplacement(char original_char)
        {
            if (CharacterReplacements.ContainsKey(original_char))
                CharacterReplacements.Remove(original_char);
        }

        public ReadOnlyDictionary<char, char> GetCharReplacements()
        {
            return new ReadOnlyDictionary<char, char>(CharacterReplacements);
        }

        public static SourceBuilderOptions Default => new SourceBuilderOptions();
    }
}
