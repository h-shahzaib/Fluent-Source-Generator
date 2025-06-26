namespace Flynth.SourceBuilder.General.Tokens
{
    public class LineToken : Token
    {
        public string Line { get; set; }

        public LineToken(Options options) : base(options)
        {
        }

        public LineToken(Options options, string line) : base(options)
        {
            Line = line;
        }

        public override string ToString()
        {
            return ApplyReplacements(Line);
        }
    }
}
