using System.Runtime.Serialization;

namespace DomainLogic
{
    [Serializable]
    public class DomainException : Exception
    {
        public DomainException() : base() { }

        public DomainException(string message) : base(message) { }

        public DomainException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected DomainException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public static DomainException Default { get; } = new DomainException("Something went wrong in the domain logic");
    }
}
