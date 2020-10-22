using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace NewShoreTest.Exceptions
{
    [Serializable()]
    public class SearchException : SystemException
    {
        public SearchException()
        {
        }

        public SearchException(string message) : base(message)
        {
            
        }

        public SearchException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected SearchException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
