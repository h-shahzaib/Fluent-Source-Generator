using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Flynth
{
    /// <summary>
    /// Provides configuration options for source code generation via SourceBuilders.
    /// </summary>
    public class SourceBuilderOptions
    {
        private readonly Dictionary<char, char> m_CharReplacements = new Dictionary<char, char>();

        /// <summary>
        /// Gets or sets the number of spaces that represent a single tab.
        /// </summary>
        public int NumberOfSpacesInOneTab { get; set; }

        /// <summary>
        /// Initializes a new instance of <see cref="SourceBuilderOptions"/> with default settings.
        /// </summary>
        public SourceBuilderOptions()
        {
            NumberOfSpacesInOneTab = 4;
            RegisterCharReplacement('`', '"');
        }

        /// <summary>
        /// Initializes a new instance of <see cref="SourceBuilderOptions"/> by copying values from another instance.
        /// </summary>
        /// <param name="options">The instance to copy values from.</param>
        public SourceBuilderOptions(SourceBuilderOptions options)
        {
            m_CharReplacements = new Dictionary<char, char>(options.m_CharReplacements);
            NumberOfSpacesInOneTab = options.NumberOfSpacesInOneTab;
        }

        /// <summary>
        /// Registers a character replacement rule.
        /// </summary>
        /// <param name="original_char">The character to be replaced.</param>
        /// <param name="replace_with">The character to replace with.</param>
        /// <exception cref="NotSupportedException">Thrown if the original character is a space.</exception>
        public void RegisterCharReplacement(char original_char, char replace_with)
        {
            if (original_char == ' ')
                throw new NotSupportedException("Cannot add replacement for `space` character.");

            m_CharReplacements[original_char] = replace_with;
        }

        /// <summary>
        /// Removes a previously registered character replacement rule.
        /// </summary>
        /// <param name="original_char">The character whose replacement should be removed.</param>
        public void RemoveCharReplacement(char original_char)
        {
            m_CharReplacements.Remove(original_char);
        }

        /// <summary>
        /// Returns a read-only dictionary of all registered character replacements.
        /// </summary>
        /// <returns>A read-only view of the character replacement rules.</returns>
        public ReadOnlyDictionary<char, char> GetCharReplacements()
        {
            return new ReadOnlyDictionary<char, char>(m_CharReplacements);
        }

        /// <summary>
        /// Returns a new instance of <see cref="SourceBuilderOptions"/> with default values.
        /// </summary>
        public static SourceBuilderOptions Default => new SourceBuilderOptions();
    }
}
