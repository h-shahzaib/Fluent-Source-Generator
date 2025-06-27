using Flynth.SourceBuilder.CSharp;

namespace Demo
{
    internal class Program
    {
        public static void Main()
        {
            var _ = new SourceBuilder();

            _.Line("Asd");
            _.Lines("Asd");
            _.Line("Asd");

            Console.WriteLine(_.ToString());
            Console.WriteLine("------------------------------------------");
        }
    }
}
