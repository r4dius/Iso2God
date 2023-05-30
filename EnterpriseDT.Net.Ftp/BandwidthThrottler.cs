using EnterpriseDT.Util.Debug;
using System;
using System.Threading;

namespace EnterpriseDT.Net.Ftp;

public class BandwidthThrottler
{
    private Logger log = Logger.GetLogger("BandwidthThrottler");

    private DateTime lastTime = DateTime.MinValue;

    private long lastBytes = 0L;

    private int thresholdBytesPerSec = -1;

    public int Threshold
    {
        get
        {
            return thresholdBytesPerSec;
        }
        set
        {
            thresholdBytesPerSec = value;
        }
    }

    public BandwidthThrottler(int thresholdBytesPerSec)
    {
        this.thresholdBytesPerSec = thresholdBytesPerSec;
    }

    public void ThrottleTransfer(long bytesSoFar)
    {
        DateTime now = DateTime.Now;
        long num = bytesSoFar - lastBytes;
        long num2 = (long)(now - lastTime).TotalMilliseconds;
        if (num2 != 0)
        {
            double num3 = (double)num / (double)num2 * 1000.0;
            log.Debug("rate={0}", num3);
            while (num3 > (double)thresholdBytesPerSec)
            {
                log.Debug("Sleeping to decrease transfer rate (rate = {0} bytes/s)", num3);
                Thread.Sleep(100);
                num2 = (long)(DateTime.Now - lastTime).TotalMilliseconds;
                num3 = (double)num / (double)num2 * 1000.0;
            }
            lastTime = now;
            lastBytes = bytesSoFar;
        }
    }

    public void Reset()
    {
        lastTime = DateTime.Now;
        lastBytes = 0L;
    }
}
