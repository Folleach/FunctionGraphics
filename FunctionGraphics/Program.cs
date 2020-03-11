using Microsoft.Xna.Framework;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionGraphics
{
    class Program
    {
        static class Parser<T>
        {
            public delegate bool ParseDelegate(string text, out T value);

            public static T Read(string text, ParseDelegate func)
            {
                while (true)
                {
                    Console.Write($"{text}: ");
                    if (func(Console.ReadLine(), out T value))
                        return value;
                }
            }
        }

        static Window window;
        const string Line = "————————————————————————————————";
        

        static void Main(string[] args)
        {
            window = new Window();
            Task.Run(ConsoleHandler);
            window.Run();
        }

        private static void ConsoleHandler()
        {
            while (true)
            {
                string cmd = Console.ReadLine();
                if (cmd == "draw func")
                {
                    Console.Write("Coordinate system (dekart or polar) d/p?: ");
                    string funcType = Console.ReadLine();
                    if (funcType != "d" && funcType != "p")
                    {
                        Console.WriteLine("Invalid coordinate system");
                        continue;
                    }
                    float step = Parser<float>.Read("Step", float.TryParse);
                    float minBound = Parser<float>.Read("Min bound", float.TryParse);
                    float maxBound = Parser<float>.Read("Max bound", float.TryParse);
                    Console.WriteLine("Begin function");
                    Console.WriteLine("Available parameters: x");
                    Console.WriteLine(Line);
                    string line;
                    StringBuilder code = new StringBuilder();
                    while ((line = Console.ReadLine()).Length != 0)
                        code.Append(line);
                    Console.WriteLine(Line);
                    Console.WriteLine("End function");
                    var function = Compiler.GetDelegate(code.ToString());
                    if (function == null)
                    {
                        Console.WriteLine("Error");
                        continue;
                    }
                    int index = 0;
                    if (funcType == "d")
                        index = window.Draw2DGraphDekart(function, step, minBound, maxBound, Color.Orange);
                    if (funcType == "p")
                        index = window.Draw2DGraphPolar(function, step, minBound, maxBound, Color.Orange);
                    Console.WriteLine("Function drawn. Index is " + index);
                }
                if (cmd == "remove")
                {
                    int index = Parser<int>.Read("Index", int.TryParse);
                    window.RemoveGraph(index);
                }
                if (cmd == "clear")
                {
                    Console.Clear();
                    continue;
                }
                Console.WriteLine();
            }
        }
    }
}
