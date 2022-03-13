using System.Runtime.Serialization;

namespace SE;

/// <summary>
/// Base exception type for those are thrown by Abp system for Abp specific exceptions.
/// </summary>
public class SeException : Exception
{
    public SeException()
    {

    }

    public SeException(string message)
        : base(message)
    {

    }

    public SeException(string message, Exception innerException)
        : base(message, innerException)
    {

    }

    public SeException(SerializationInfo serializationInfo, StreamingContext context)
        : base(serializationInfo, context)
    {

    }
}
