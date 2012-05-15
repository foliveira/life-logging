using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LifeLogger.Parser;

namespace LifeLogger.Parser
{
    public class ParserEngine
    {
        private ParserContext _parserContext;

        public ParserEngine()
        {
            _parserContext = new ParserContext();
        }

        public ParserContext ParseUserInput(String input, ParserContext context = null)
        {
            var parts = input.Split(' ');
            var settings = Program.Settings;

            _parserContext = context ?? _parserContext;

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

            _parserContext.ContextAction = userAction;

            var inputSansAction = parts.Skip(1).ToArray();

            HandleAction(inputSansAction);
            HandleLocation(inputSansAction);
            HandleTime(inputSansAction);

            var returningContext = _parserContext;
            _parserContext = new ParserContext();
            return returningContext;
        }

        private ParserContext ParseTime(string firstCommand)
        {
            var when = firstCommand.Substring(1);

            switch (when)
            {
                case "today":
                    _parserContext.ContextDate = DateTime.Today.Date;
                    break;
                case "yesterday":
                    _parserContext.ContextDate = DateTime.Today.AddDays(-1).Date;
                    break;
                default:
                    {
                        DateTime contextDate;
                        if (DateTime.TryParse(when, out contextDate))
                            _parserContext.ContextDate = contextDate;
                    }
                    break;
            }

            return _parserContext;
        }

        private ParserContext HandleTime(IEnumerable<string> parts)
        {
            var timePart = parts.FirstOrDefault(p => p.StartsWith("@"));
            if (timePart == null)
                return _parserContext;

            timePart = timePart.Substring(1);

            DateTime time;

            if (!DateTime.TryParse(timePart, out time))
                return _parserContext;

            _parserContext.ContextDate = _parserContext.ContextDate.AddHours(time.Hour);
            _parserContext.ContextDate = _parserContext.ContextDate.AddMinutes(time.Minute);

            return _parserContext;
        }

        private ParserContext HandleLocation(IEnumerable<string> parts)
        {
            var locationParts = parts.SkipWhile(s => !s.Equals("at")).Skip(1);

            _parserContext.ContextLocation = String.Join(" ", locationParts);

            return _parserContext;
        }

        private ParserContext HandleAction(IEnumerable<string> parts)
        {
            var actionParts = parts.TakeWhile(s => !s.StartsWith("@")).TakeWhile(s => !s.Equals("at"));

            _parserContext.ContextContent = String.Join(" ", actionParts);

            return _parserContext;
        }
    }
}
