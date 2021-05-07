using Antlr4.Runtime.Misc;
using System.Collections.Generic;
using static GettingStartedCSharpAntlr.SpeakParser;

namespace GettingStartedCSharpAntlr
{
    public class SpeakVisitor : SpeakBaseVisitor<object>
    {
        public List<SpeakLine> Lines = new List<SpeakLine>();

        public override object VisitLine([NotNull] LineContext context)
        {
            NameContext name = context.name();
            OpinionContext opinion = context.opinion();

            SpeakLine line = new()
            {
                Person = name.GetText(),
                Text = opinion.GetText().Trim('"')
            };
            Lines.Add(line);
            // Done for unit testing (per tutorial)
            return line;
        }
    }
}
