using System;
using System.ComponentModel;

namespace EnterpriseDT.Net.Ftp;

public class PortRange
{
    internal const int LOW_PORT = 1024;

    internal const int DEFAULT_HIGH_PORT = 5000;

    private const int HIGH_PORT = 65535;

    private PropertyChangedEventHandler propertyChangeHandler = null;

    private int low;

    private int high;

    [RefreshProperties(RefreshProperties.All)]
    [DefaultValue(1024)]
    [Description("Lowest port number in range.")]
    public int LowPort
    {
        get
        {
            return low;
        }
        set
        {
            if (value > 65535 || value < 1024)
            {
                throw new ArgumentException("Ports must be in range [" + 1024 + "," + 65535 + "]");
            }
            if (LowPort != value)
            {
                low = value;
                if (propertyChangeHandler != null)
                {
                    propertyChangeHandler(this, new PropertyChangedEventArgs("LowPort"));
                }
            }
        }
    }

    [DefaultValue(5000)]
    [Description("Highest port number in range.")]
    [RefreshProperties(RefreshProperties.All)]
    public int HighPort
    {
        get
        {
            return high;
        }
        set
        {
            if (value > 65535 || value < 1024)
            {
                throw new ArgumentException("Ports must be in range [" + 1024 + "," + 65535 + "]");
            }
            if (HighPort != value)
            {
                high = value;
                if (propertyChangeHandler != null)
                {
                    propertyChangeHandler(this, new PropertyChangedEventArgs("HighPort"));
                }
            }
        }
    }

    [DefaultValue(true)]
    [RefreshProperties(RefreshProperties.All)]
    [Description("Determines if the operating system should select the ports within the range 1024-5000.")]
    public bool UseOSAssignment
    {
        get
        {
            if (low == 1024)
            {
                return high == 5000;
            }
            return false;
        }
        set
        {
            LowPort = 1024;
            HighPort = 5000;
        }
    }

    internal PropertyChangedEventHandler PropertyChangeHandler
    {
        get
        {
            return propertyChangeHandler;
        }
        set
        {
            propertyChangeHandler = value;
        }
    }

    internal PortRange()
    {
        low = 1024;
        high = 5000;
    }

    internal PortRange(int low, int high)
    {
        if (low < 1024 || high > 65535)
        {
            throw new ArgumentException("Ports must be in range [" + 1024 + "," + 65535 + "]");
        }
        if (low >= high)
        {
            throw new ArgumentException("Low port (" + low + ") must be smaller than high port (" + high + ")");
        }
        this.low = low;
        this.high = high;
    }

    internal void ValidateRange()
    {
        if (low >= high)
        {
            throw new FTPException("Low port (" + low + ") must be smaller than high port (" + high + ")");
        }
    }

    public override string ToString()
    {
        return $"{low} -> {high}";
    }
}
