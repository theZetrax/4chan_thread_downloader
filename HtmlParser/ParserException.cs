using System;

namespace HtmlParser
{
    [System.Serializable]
    public class ParserException : System.Exception
    {
        public ParserException() { }
        public ParserException(string message) : base(message) { }
        public ParserException(string message, System.Exception inner) : base(message, inner) { }
        protected ParserException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}