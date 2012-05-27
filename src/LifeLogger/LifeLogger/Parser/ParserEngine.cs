namespace LifeLogger.Parser
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class ParserEngine
    {
        public ParserEngine()
        {
            CurrentParserContext = new ParserContext();
        }

        internal ParserContext CurrentParserContext { get; private set; }

        public ParserContext ParseUserInput(String input, ParserContext context = null)
        {
            var parts = input.Split(' ');
            var settings = Program.Settings;

            CurrentParserContext = context ?? CurrentParserContext;

            if (parts.Length == 0)
                return ParserContext.Empty;

            var firstCommand = parts[0];
            if(firstCommand.StartsWith("@"))
            {
                return ParseTime(firstCommand);
            }

            var userAction = settings.GetActionForShortcut(firstCommand);

            if(userAction == null)
            {
                return ParserContext.Empty;
            }

            CurrentParserContext.ContextAction = userAction;

            var inputSansAction = parts.Skip(1).ToArray();

            HandleAction(inputSansAction);
            HandleLocation(inputSansAction);
            HandleTime(inputSansAction);

            var returningContext = CurrentParserContext;
            CurrentParserContext = new ParserContext();
            return returningContext;
        }

        private ParserContext ParseTime(string firstCommand)
        {
            var when = firstCommand.Substring(1);

            switch (when)
            {
                case "today":
                    CurrentParserContext.ContextDate = DateTime.Today.Date;
                    break;
                case "yesterday":
                    CurrentParserContext.ContextDate = DateTime.Today.AddDays(-1).Date;
                    break;
                default:
                    {
                        DateTime contextDate;
                        if (DateTime.TryParse(when, out contextDate))
                            CurrentParserContext.ContextDate = contextDate;
                    }
                    break;
            }

            return CurrentParserContext;
        }

        private ParserContext HandleTime(IEnumerable<string> parts)
        {
            var timePart = parts.FirstOrDefault(p => p.StartsWith("@"));
            if (timePart == null)
                return CurrentParserContext;

            timePart = timePart.Substring(1);

            DateTime time;

            if (!DateTime.TryParse(timePart, out time))
                return CurrentParserContext;

            CurrentParserContext.ContextDate = CurrentParserContext.ContextDate.AddHours(time.Hour);
            CurrentParserContext.ContextDate = CurrentParserContext.ContextDate.AddMinutes(time.Minute);

            return CurrentParserContext;
        }

        private ParserContext HandleLocation(IEnumerable<string> parts)
        {
            var locationParts = parts.SkipWhile(p => !p.StartsWith("@@")).ToString();

            if (String.IsNullOrEmpty(locationParts))
                return CurrentParserContext;

            CurrentParserContext.ContextLocation = String.Join(" ", locationParts.Skip(2)); //Take out the leading @@

            return CurrentParserContext;
        }

        private ParserContext HandleAction(IEnumerable<string> parts)
        {
            var actionParts = parts.TakeWhile(s => !s.StartsWith("@")).TakeWhile(s => !s.Equals("@@"));

            CurrentParserContext.ContextContent = String.Join(" ", actionParts);

            return CurrentParserContext;
        }
    }
}
