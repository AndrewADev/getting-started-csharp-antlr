using Antlr4.Runtime;
using System;
using System.Text;

namespace GettingStartedCSharpAntlr
{
    class Program
    {
        public static void Main(string[] args)
        {
            string input = "";
            StringBuilder text = new StringBuilder();
            Console.WriteLine("Input your chat message");

            try
            {
                while ((input = Console.ReadLine()) != "\u0004")
                {
                    text.AppendLine(input);
                }

                AntlrInputStream inputStream = new AntlrInputStream(text.ToString());

                SpeakLexer speakLexer = new SpeakLexer(inputStream);
                CommonTokenStream commonTokenStream = new CommonTokenStream(speakLexer);

                SpeakParser speakParser = new SpeakParser(commonTokenStream);

                SpeakParser.ChatContext chatContext = speakParser.chat();
                SpeakVisitor visitor = new SpeakVisitor();
                visitor.Visit(chatContext);
                foreach (var line in visitor.Lines)
                {
                    Console.WriteLine($"{line.Person} said {line.Text}");
                }
            } catch(Exception ex)
            {
                Console.WriteLine($"Exception: {ex}");
            }
        }
    }
}
