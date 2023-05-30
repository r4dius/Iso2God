using System;
using System.IO;

namespace EnterpriseDT.Util.Debug;

public class StandardOutputAppender : Appender
{
    private StreamWriter log;

    public StandardOutputAppender()
    {
        log = new StreamWriter(Console.OpenStandardOutput());
    }

    public virtual void Log(string msg)
    {
        log.WriteLine(msg);
        log.Flush();
    }

    public virtual void Log(Exception t)
    {
        log.WriteLine(((object)t).GetType().FullName + ": " + t.Message);
        log.WriteLine(t.StackTrace.ToString());
        log.Flush();
    }

    public virtual void Close()
    {
        log.Flush();
    }
}
