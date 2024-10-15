using EnterpriseDT.Util.Debug;
using System;
using System.Net;
using System.Text.RegularExpressions;

namespace EnterpriseDT.Net;

internal class HostNameResolver
{
    private const string IP_ADDRESS_REGEX = "[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}";

    private static Logger log = Logger.GetLogger("HostNameResolver");

    public static IPAddress GetAddress(string hostName)
    {
        if (hostName == null)
        {
            throw new ArgumentNullException();
        }
        IPAddress iPAddress = null;
        iPAddress = ((!Regex.IsMatch(hostName, "[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}")) ? Dns.GetHostEntry(hostName).AddressList[0] : IPAddress.Parse(hostName));
        if (log.DebugEnabled)
        {
            log.Debug(hostName + " resolved to " + iPAddress.ToString());
        }
        return iPAddress;
    }
}
