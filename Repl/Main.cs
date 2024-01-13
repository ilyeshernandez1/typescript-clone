using System;

namespace PureType
{
    class Repl
    {
        public static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("PureType REPL (Ctrl+C to exit)");
            Console.ResetColor();

            while (true)
            {
                Console.Write("> ");
                var line = Console.ReadLine();

                if (line == null)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("You did not enter anything!");
                    Console.ResetColor();
                    continue;
                }

                Lexer lexer = new Lexer(line);

                try
                {
                    var tokens = lexer.Tokenize();

                    foreach (var token in tokens)
                    {
                        Console.WriteLine(token.ToString());
                    }
                }
                catch (UnexpectedCharacterException ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Error: {ex.Message}");
                    Console.ResetColor();
                }

                if (line == "exit")
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Bye!");
                    Console.ResetColor();
                    break;
                }
            }
        }
    }
}
