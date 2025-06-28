using Flynth.CSharp;

namespace Flynth.Demo
{
    internal class Program
    {
        public static void Main()
        {
            var _ = new SourceBuilder();

            _.Using("System.Text");

            _.Namespace("Shzb.Program", _ =>
            {
                _.Class("public", "Program", _ =>
                {
                    _.Line("Console.WriteLine(`Hello World!`);");
                });

                _.Class("public", "Program", _ =>
                {
                    _.Line("Console.WriteLine(`Hello World!`);");
                    
                    _.If("A > 5", _ =>
                    {
                        _.Line("Console.WriteLine(`Hello World!`);");
                    })
                    .ElseIf("B > 5", _ =>
                    {
                        _.Line("Console.WriteLine(`Hello World!`);");
                    })
                    .Else(_ =>
                    {
                        _.Line("Console.WriteLine(`Hello World!`);");
                    });
                });
            });

            Console.WriteLine(_.ToString());
        }
    }
}
