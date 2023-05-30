using System.Threading;

namespace EnterpriseDT.Net.Ftp;

internal class FTPSemaphore
{
    private object syncLock = new object();

    private long count = 0L;

    internal FTPSemaphore(int initCount)
    {
        count = initCount;
    }

    internal void WaitOne(int timeoutMillis)
    {
        lock (syncLock)
        {
            while (count == 0)
            {
                Monitor.Wait(syncLock, timeoutMillis);
            }
            count--;
        }
    }

    internal void Release()
    {
        lock (syncLock)
        {
            count++;
            Monitor.Pulse(syncLock);
        }
    }
}
