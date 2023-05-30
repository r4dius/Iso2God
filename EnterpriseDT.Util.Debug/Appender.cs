using System;

namespace EnterpriseDT.Util.Debug;

public interface Appender
{
    void Close();

    void Log(string msg);

    void Log(Exception t);
}
