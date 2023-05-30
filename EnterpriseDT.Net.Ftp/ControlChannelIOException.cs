using System.IO;

namespace EnterpriseDT.Net.Ftp;

public class ControlChannelIOException : IOException
{
    public ControlChannelIOException()
    {
    }

    public ControlChannelIOException(string message)
        : base(message)
    {
    }
}
