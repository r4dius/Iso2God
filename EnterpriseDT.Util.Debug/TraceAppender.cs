#define TRACE
using System;
using System.Diagnostics;

namespace EnterpriseDT.Util.Debug;

public class TraceAppender : Appender
{
    public virtual void Log(string msg)
    {
        Trace.WriteLine(msg);
    }

    public virtual void Log(Exception t)
    {
        Trace.WriteLine(((object)t).GetType().FullName + ": " + t.Message);
        Trace.WriteLine(t.StackTrace.ToString());
    }

    public virtual void Close()
    {
        Trace.Close();
    }
}
