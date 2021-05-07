using Antlr4.Runtime;
using FluentAssertions;
using Xunit;

namespace GettingStartedCSharpAntlr.Tests
{
    public class SpeakParserShould
    {
        private SpeakParser Setup(string text)
        {
            AntlrInputStream inputStream = new(text);
            SpeakLexer speakLexer = new(inputStream);
            CommonTokenStream commonTokenStream = new(speakLexer);
            return new SpeakParser(commonTokenStream);
        }

        [Fact]
        public void ParseValidLines()
        {
            var parser = Setup("john says \"hello\" \n michael says \"world\" \n");
            SpeakParser.ChatContext context = parser.chat();
            SpeakVisitor visitor = new();
            visitor.Visit(context);
            visitor.Lines.Should().HaveCount(2);
        }

        [Fact]
        public void ParseInvalidLines()
        {
            var parser = Setup("john sayan \"hello\" \n");
            var context = parser.line();

            context.Should().BeOfType(typeof(SpeakParser.LineContext));
            context.name().GetText().Should().Be("john", "it's a valid name token");
            context.SAYS().Should().BeNull("no valid SAYS in text");
            context.GetText().Should().Be("johnsayan\"hello\"\n");
        }
    }
}
