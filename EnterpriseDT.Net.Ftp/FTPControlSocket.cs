using EnterpriseDT.Util.Debug;
using System;
using System.Collections;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace EnterpriseDT.Net.Ftp;

public class FTPControlSocket
{
    internal const string EOL = "\r\n";

    private const byte CARRIAGE_RETURN = 13;

    private const byte LINE_FEED = 10;

    internal const int MAX_ACTIVE_RETRY = 100;

    public const int CONTROL_PORT = 21;

    private const string DEBUG_ARROW = "---> ";

    private static string PASV_MUTEX_NAME = "Global\\edtFTPnet_SynchronizePassiveConnections";

    private static readonly string PASSWORD_MESSAGE = "---> PASS";

    private Logger log = Logger.GetLogger("FTPControlSocket");

    private bool synchronizePassiveConnections = false;

    private bool strictReturnCodes = false;

    protected string remoteHost = null;

    protected IPAddress remoteAddr = null;

    protected int controlPort = -1;

    protected BaseSocket controlSock = null;

    protected int timeout = 0;

    protected StreamWriter writer = null;

    protected StreamReader reader = null;

    private Encoding encoding;

    private PortRange activePortRange = null;

    private IPAddress activeIPAddress = null;

    private int nextPort = 0;

    private bool autoPassiveIPSubstitution = false;

    internal virtual bool SynchronizePassiveConnections
    {
        get
        {
            return synchronizePassiveConnections;
        }
        set
        {
            synchronizePassiveConnections = value;
        }
    }

    internal virtual bool StrictReturnCodes
    {
        get
        {
            return strictReturnCodes;
        }
        set
        {
            log.Debug("StrictReturnCodes=" + value);
            strictReturnCodes = value;
        }
    }

    internal virtual int Timeout
    {
        get
        {
            return timeout;
        }
        set
        {
            timeout = value;
            log.Debug("Setting socket timeout=" + value);
            if (controlSock == null)
            {
                throw new SystemException("Failed to set timeout - no control socket");
            }
            SetSocketTimeout(controlSock, value);
        }
    }

    public bool Connected
    {
        get
        {
            if (controlSock != null)
            {
                return controlSock.Connected;
            }
            return false;
        }
    }

    internal IPAddress LocalAddress => ((IPEndPoint)controlSock.LocalEndPoint).Address;

    internal bool AutoPassiveIPSubstitution
    {
        get
        {
            return autoPassiveIPSubstitution;
        }
        set
        {
            autoPassiveIPSubstitution = value;
        }
    }

    internal event FTPMessageHandler CommandSent;

    internal event FTPMessageHandler ReplyReceived;

    internal event FTPErrorEventHandler CommandError;

    internal FTPControlSocket(string remoteHost, int controlPort, int timeout, Encoding encoding)
    {
        if (activePortRange != null)
        {
            activePortRange.ValidateRange();
        }
        Initialize(new StandardSocket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp), remoteHost, controlPort, timeout, encoding);
    }

    internal FTPControlSocket()
    {
    }

    internal void Initialize(BaseSocket sock, string remoteHost, int controlPort, int timeout, Encoding encoding)
    {
        this.remoteHost = remoteHost;
        this.controlPort = controlPort;
        this.timeout = timeout;
        controlSock = sock;
        ConnectSocket(controlSock, remoteHost, controlPort);
        Timeout = timeout;
        InitStreams(encoding);
    }

    internal virtual void ConnectSocket(BaseSocket socket, string address, int port)
    {
        remoteAddr = HostNameResolver.GetAddress(address);
        socket.Connect(new IPEndPoint(remoteAddr, port));
    }

    internal void ValidateConnection()
    {
        FTPReply reply = ReadReply();
        ValidateReply(reply, "220", "230");
    }

    internal void InitStreams(Encoding encoding)
    {
        Stream stream = controlSock.GetStream();
        if (encoding == null)
        {
            encoding = Encoding.ASCII;
        }
        this.encoding = encoding;
        log.Debug("Command encoding=" + encoding.ToString());
        writer = new StreamWriter(stream, encoding);
        reader = new StreamReader(stream, encoding);
    }

    internal void SetActivePortRange(PortRange portRange)
    {
        portRange.ValidateRange();
        activePortRange = portRange;
        if (!portRange.UseOSAssignment)
        {
            Random random = new Random();
            nextPort = random.Next(activePortRange.LowPort, activePortRange.HighPort);
            log.Debug("SetActivePortRange(" + activePortRange.LowPort + "," + activePortRange.HighPort + "). NextPort=" + nextPort);
        }
    }

    internal void SetActiveIPAddress(IPAddress address)
    {
        activeIPAddress = address;
    }

    internal void Kill()
    {
        try
        {
            if (controlSock != null)
            {
                controlSock.Close();
            }
            controlSock = null;
        }
        catch (Exception t)
        {
            log.Debug("Killed socket", t);
        }
        log.Info("Killed control socket");
    }

    internal virtual void Logout()
    {
        SystemException ex = null;
        try
        {
            writer.Close();
        }
        catch (SystemException ex2)
        {
            ex = ex2;
        }
        try
        {
            reader.Close();
        }
        catch (SystemException ex3)
        {
            ex = ex3;
        }
        try
        {
            controlSock.Close();
            controlSock = null;
        }
        catch (SystemException ex4)
        {
            ex = ex4;
        }
        if (ex != null)
        {
            log.Error("Caught exception", ex);
            throw ex;
        }
    }

    internal virtual FTPDataSocket CreateDataSocket(FTPConnectMode connectMode)
    {
        if (connectMode == FTPConnectMode.ACTIVE)
        {
            return CreateDataSocketActive();
        }
        return CreateDataSocketPASV();
    }

    internal virtual FTPDataSocket CreateDataSocketActive()
    {
        try
        {
            int num = 0;
            int num2 = 100;
            if (activePortRange != null)
            {
                int num3 = activePortRange.HighPort - activePortRange.LowPort + 1;
                if (num3 < 100)
                {
                    num2 = num3;
                }
            }
            while (num < num2)
            {
                num++;
                try
                {
                    return NewActiveDataSocket(nextPort);
                }
                catch (SocketException)
                {
                    if (num < num2)
                    {
                        log.Warn("Detected socket in use - retrying and selecting new port");
                        SetNextAvailablePortFromRange();
                    }
                }
            }
            throw new FTPException("Exhausted active port retry count - giving up");
        }
        finally
        {
            SetNextAvailablePortFromRange();
        }
    }

    private void SetNextAvailablePortFromRange()
    {
        if (activePortRange != null && !activePortRange.UseOSAssignment)
        {
            if (nextPort == 0)
            {
                Random random = new Random();
                nextPort = random.Next(activePortRange.LowPort, activePortRange.HighPort);
            }
            else
            {
                nextPort++;
            }
            if (nextPort > activePortRange.HighPort)
            {
                nextPort = activePortRange.LowPort;
            }
            log.Debug("Next active port will be: " + nextPort);
        }
    }

    internal void SetDataPort(IPEndPoint ep)
    {
        byte[] bytes = ep.Address.GetAddressBytes();
        if (activeIPAddress != null)
        {
            log.Info("Forcing use of fixed IP for PORT command");
            bytes = activeIPAddress.GetAddressBytes();
        }
        byte[] array = ToByteArray((ushort)ep.Port);
        string command = new StringBuilder("PORT ").Append((short)bytes[0]).Append(",").Append((short)bytes[1])
            .Append(",")
            .Append((short)bytes[2])
            .Append(",")
            .Append((short)bytes[3])
            .Append(",")
            .Append((short)array[0])
            .Append(",")
            .Append((short)array[1])
            .ToString();
        FTPReply reply = SendCommand(command);
        ValidateReply(reply, "200", "250");
    }

    internal byte[] ToByteArray(ushort val)
    {
        return new byte[2]
        {
            (byte)(val >> 8),
            (byte)(val & 0xFFu)
        };
    }

    internal virtual FTPDataSocket CreateDataSocketPASV()
    {
        bool flag = SynchronizePassiveConnections;
        Mutex mutex = null;
        if (flag)
        {
            mutex = new Mutex(initiallyOwned: false, PASV_MUTEX_NAME);
            mutex.WaitOne();
        }
        try
        {
            FTPReply fTPReply = SendCommand("PASV");
            ValidateReply(fTPReply, "227");
            string replyText = fTPReply.ReplyText;
            Regex regex = new Regex("(?<a0>\\d{1,3}),(?<a1>\\d{1,3}),(?<a2>\\d{1,3}),(?<a3>\\d{1,3}),(?<p0>\\d{1,3}),(?<p1>\\d{1,3})");
            Match match = regex.Match(replyText);
            string text = match.Groups["a0"].Value + "." + match.Groups["a1"].Value + "." + match.Groups["a2"].Value + "." + match.Groups["a3"].Value;
            log.Debug("Server supplied address=" + text);
            int[] array = new int[2]
            {
                int.Parse(match.Groups["p0"].Value),
                int.Parse(match.Groups["p1"].Value)
            };
            int num = (array[0] << 8) + array[1];
            log.Debug("Server supplied port=" + num);
            string text2 = text;
            if (autoPassiveIPSubstitution && remoteAddr != null)
            {
                text2 = remoteAddr.ToString();
                if (log.IsEnabledFor(Level.DEBUG))
                {
                    log.Debug($"Substituting server supplied IP ({text}) with remote host IP ({text2})");
                }
            }
            return NewPassiveDataSocket(text2, num);
        }
        finally
        {
            if (flag && mutex != null)
            {
                mutex.ReleaseMutex();
                mutex.Close();
            }
        }
    }

    internal virtual FTPDataSocket NewPassiveDataSocket(string ipAddress, int port)
    {
        log.Debug("NewPassiveDataSocket(" + ipAddress + "," + port + ")");
        IPAddress address = IPAddress.Parse(ipAddress);
        IPEndPoint remoteEP = new IPEndPoint(address, port);
        BaseSocket baseSocket = new StandardSocket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        try
        {
            SetSocketTimeout(baseSocket, timeout);
            baseSocket.Connect(remoteEP);
        }
        catch (Exception t)
        {
            log.Error("Failed to create connecting socket", t);
            baseSocket.Close();
            throw;
        }
        return new FTPPassiveDataSocket(baseSocket);
    }

    internal virtual FTPDataSocket NewActiveDataSocket(int port)
    {
        BaseSocket baseSocket = new StandardSocket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        try
        {
            log.Debug("NewActiveDataSocket(" + port + ")");
            IPEndPoint localEP = new IPEndPoint(((IPEndPoint)controlSock.LocalEndPoint).Address, port);
            baseSocket.Bind(localEP);
            baseSocket.Listen(5);
            SetDataPort((IPEndPoint)baseSocket.LocalEndPoint);
        }
        catch (Exception t)
        {
            log.Error("Failed to create listening socket", t);
            baseSocket.Close();
            throw;
        }
        return new FTPActiveDataSocket(baseSocket);
    }

    public virtual FTPReply SendCommand(string command)
    {
        try
        {
            WriteCommand(command);
            return ReadReply();
        }
        catch (Exception ex)
        {
            log.Error("Exception in SendCommand", ex);
            if (this.CommandError != null)
            {
                this.CommandError(this, new FTPErrorEventArgs(ex));
            }
            throw;
        }
    }

    internal virtual void WriteCommand(string command)
    {
        Log("---> " + command, command: true);
        writer.Write(command + "\r\n");
        writer.Flush();
    }

    private string ReadLine()
    {
        int num = 0;
        StringBuilder stringBuilder = new StringBuilder();
        StringBuilder stringBuilder2 = new StringBuilder();
        while (true)
        {
            try
            {
                num = reader.Read();
            }
            catch (IOException ex)
            {
                log.Error("Read failed ('" + stringBuilder2.ToString() + "' read so far)");
                throw new ControlChannelIOException(ex.Message);
            }
            if (num < 0)
            {
                break;
            }
            switch (num)
            {
                case 13:
                    continue;
                case 10:
                    return stringBuilder.ToString();
            }
            stringBuilder.Append((char)num);
            stringBuilder2.Append((char)num);
        }
        string message = "Control channel unexpectedly closed ('" + stringBuilder2.ToString() + "' read so far)";
        log.Error(message);
        throw new ControlChannelIOException(message);
    }

    internal virtual FTPReply ReadReply()
    {
        string text = ReadLine();
        while (text.Length == 0)
        {
            text = ReadLine();
        }
        Log(text, command: false);
        if (text.Length < 3)
        {
            string message = "Short reply received (" + text + ")";
            log.Error(message);
            throw new MalformedReplyException(message);
        }
        string text2 = text.Substring(0, 3);
        StringBuilder stringBuilder = new StringBuilder("");
        if (text.Length > 3)
        {
            stringBuilder.Append(text.Substring(4));
        }
        ArrayList arrayList = null;
        if (text[3] == '-')
        {
            arrayList = ArrayList.Synchronized(new ArrayList(10));
            bool flag = false;
            while (!flag)
            {
                text = ReadLine();
                if (text.Length != 0)
                {
                    Log(text, command: false);
                    if (text.Length > 3 && text.Substring(0, 3).Equals(text2) && text[3] == ' ')
                    {
                        stringBuilder.Append(text.Substring(3));
                        flag = true;
                    }
                    else
                    {
                        stringBuilder.Append(" ").Append(text);
                        arrayList.Add(text);
                    }
                }
            }
        }
        if (arrayList != null)
        {
            string[] array = new string[arrayList.Count];
            arrayList.CopyTo(array);
            return new FTPReply(text2, stringBuilder.ToString(), array);
        }
        return new FTPReply(text2, stringBuilder.ToString());
    }

    public virtual FTPReply ValidateReply(FTPReply reply, params string[] expectedReplyCodes)
    {
        if ("421" == reply.ReplyCode)
        {
            log.Error("Received 421 - throwing exception");
            throw new FTPConnectionClosedException(reply.ReplyText);
        }
        string[] array = expectedReplyCodes;
        foreach (string text in array)
        {
            if (strictReturnCodes)
            {
                if (reply.ReplyCode == text)
                {
                    return reply;
                }
            }
            else if (reply.ReplyCode[0] == text[0])
            {
                return reply;
            }
        }
        StringBuilder stringBuilder = new StringBuilder("[");
        int num = 0;
        array = expectedReplyCodes;
        foreach (string value in array)
        {
            stringBuilder.Append(value);
            if (num + 1 < expectedReplyCodes.Length)
            {
                stringBuilder.Append(",");
            }
            num++;
        }
        stringBuilder.Append("]");
        log.Info("Expected reply codes = " + stringBuilder.ToString() + " (strict=" + strictReturnCodes + ")");
        throw new FTPException(reply);
    }

    internal virtual void Log(string msg, bool command)
    {
        if (msg.StartsWith(PASSWORD_MESSAGE))
        {
            msg = PASSWORD_MESSAGE + " ********";
        }
        log.Debug(msg);
        if (command)
        {
            if (this.CommandSent != null)
            {
                this.CommandSent(this, new FTPMessageEventArgs(msg));
            }
        }
        else if (this.ReplyReceived != null)
        {
            this.ReplyReceived(this, new FTPMessageEventArgs(msg));
        }
    }

    internal void SetSocketTimeout(BaseSocket sock, int timeout)
    {
        if (timeout > 0)
        {
            try
            {
                sock.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, timeout);
                sock.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.SendTimeout, timeout);
            }
            catch (SocketException ex)
            {
                log.Warn("Failed to set socket timeout: " + ex.Message);
            }
        }
    }
}
