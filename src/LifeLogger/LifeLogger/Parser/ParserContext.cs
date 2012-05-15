using System;
using System.Text;

namespace LifeLogger.Parser
{
    public class ParserContext
    {
        public static readonly ParserContext Empty = new ParserContext {IsEmpty = true};

        public ParserContext()
        {
            ContextDate = DateTime.Today;
        }

        public Boolean IsEmpty { get; private set; }

        public UserAction ContextAction { get; set; }
        public String ContextContent { get; set; }
        public String ContextLocation { get; set; }
        public DateTime ContextDate { get; set; }

        public override string ToString()
        {
            if (IsEmpty)
                return String.Empty;

            //Create different returns based on the content of each property
            var sb = new StringBuilder();

            sb.AppendFormat("L({0} {1}", ContextAction.ActionName, ContextContent);
            sb.AppendFormat("{0})",
                            String.IsNullOrEmpty(ContextLocation)
                                ? String.Empty
                                : String.Format("[{0}]", ContextLocation));
            sb.AppendFormat("+T({0})",
                            ContextDate.Equals(DateTime.Today)
                                ? DateTime.Now.ToShortTimeString()
                                : ContextDate.ToShortTimeString());

            return sb.ToString();
        }
    }
}
