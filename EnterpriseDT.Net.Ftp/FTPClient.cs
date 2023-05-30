using EnterpriseDT.Util.Debug;
using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace EnterpriseDT.Net.Ftp;

public class FTPClient : IFileTransferClient
{
    private const int DEFAULT_MONITOR_INTERVAL = 4096;

    private const int DEFAULT_BUFFER_SIZE = 4096;

    private const string DEFAULT_TIME_FORMAT = "yyyyMMddHHmmss";

    private static string majorVersion;

    private static string middleVersion;

    private static string minorVersion;

    private static int[] version;

    private static string buildTimestamp;

    private static string BINARY_CHAR;

    private static string ASCII_CHAR;

    private static int SHORT_TIMEOUT;

    internal DirectoryEmptyStrings dirEmptyStrings = new DirectoryEmptyStrings();

    internal TransferCompleteStrings transferCompleteStrings = new TransferCompleteStrings();

    internal FileNotFoundStrings fileNotFoundStrings = new FileNotFoundStrings();

    private string[] modtimeFormats = new string[4] { "yyyyMMddHHmmss", "yyyyMMddHHmmss'.'f", "yyyyMMddHHmmss'.'ff", "yyyyMMddHHmmss'.'fff" };

    private Logger log;

    internal FTPControlSocket control = null;

    internal FTPDataSocket data = null;

    internal int timeout = 120000;

    protected int noOperationInterval = 0;

    private bool strictReturnCodes = false;

    private bool cancelTransfer = false;

    private bool transferNotifyListings = false;

    private bool resume = false;

    private bool deleteOnFailure = true;

    private bool mdtmSupported = true;

    private bool sizeSupported = true;

    private long resumeMarker = 0L;

    private bool showHiddenFiles = false;

    private long monitorInterval = 4096L;

    private int transferBufferSize = 4096;

    private CultureInfo parserCulture = CultureInfo.InvariantCulture;

    private FTPFileFactory fileFactory = new FTPFileFactory();

    private FTPTransferType transferType = FTPTransferType.ASCII;

    private FTPConnectMode connectMode = FTPConnectMode.PASV;

    private bool synchronizePassiveConnections = false;

    private PortRange activePortRange = new PortRange();

    private IPAddress activeIPAddress = null;

    internal FTPReply lastValidReply;

    internal int controlPort = -1;

    internal string remoteHost = null;

    private bool autoPassiveIPSubstitution = false;

    private bool closeStreamsAfterTransfer = true;

    private Encoding controlEncoding = null;

    private Encoding dataEncoding = null;

    protected BandwidthThrottler throttler = null;

    public static int[] Version => version;

    public static string BuildTimestamp => buildTimestamp;

    public bool StrictReturnCodes
    {
        get
        {
            return strictReturnCodes;
        }
        set
        {
            strictReturnCodes = value;
            if (control != null)
            {
                control.StrictReturnCodes = value;
            }
        }
    }

    public virtual int Timeout
    {
        get
        {
            return timeout;
        }
        set
        {
            timeout = value;
            if (control != null)
            {
                control.Timeout = value;
            }
        }
    }

    public bool Connected
    {
        get
        {
            if (control != null)
            {
                return control.Connected;
            }
            return false;
        }
    }

    public FTPConnectMode ConnectMode
    {
        get
        {
            return connectMode;
        }
        set
        {
            connectMode = value;
        }
    }

    public bool IsConnected => Connected;

    public bool SynchronizePassiveConnections
    {
        get
        {
            return synchronizePassiveConnections;
        }
        set
        {
            synchronizePassiveConnections = value;
            if (control != null)
            {
                control.SynchronizePassiveConnections = value;
            }
        }
    }

    public bool ShowHiddenFiles
    {
        get
        {
            return showHiddenFiles;
        }
        set
        {
            showHiddenFiles = value;
        }
    }

    public long TransferNotifyInterval
    {
        get
        {
            return monitorInterval;
        }
        set
        {
            monitorInterval = value;
        }
    }

    public int TransferBufferSize
    {
        get
        {
            return transferBufferSize;
        }
        set
        {
            if (value <= 0)
            {
                throw new ArgumentException("TransferBufferSize must be greater than 0.");
            }
            transferBufferSize = value;
        }
    }

    public virtual string RemoteHost
    {
        get
        {
            return remoteHost;
        }
        set
        {
            CheckConnection(shouldBeConnected: false);
            remoteHost = value;
        }
    }

    public bool DeleteOnFailure
    {
        get
        {
            return deleteOnFailure;
        }
        set
        {
            deleteOnFailure = value;
        }
    }

    public int ControlPort
    {
        get
        {
            return controlPort;
        }
        set
        {
            CheckConnection(shouldBeConnected: false);
            controlPort = value;
        }
    }

    public CultureInfo ParsingCulture
    {
        get
        {
            return parserCulture;
        }
        set
        {
            if (value == null)
            {
                value = CultureInfo.InvariantCulture;
            }
            parserCulture = value;
            if (fileFactory != null)
            {
                fileFactory.ParsingCulture = value;
            }
        }
    }

    public Encoding ControlEncoding
    {
        get
        {
            return controlEncoding;
        }
        set
        {
            controlEncoding = value;
        }
    }

    public Encoding DataEncoding
    {
        get
        {
            return dataEncoding;
        }
        set
        {
            dataEncoding = value;
        }
    }

    public FTPFileFactory FTPFileFactory
    {
        get
        {
            return fileFactory;
        }
        set
        {
            fileFactory = value;
        }
    }

    public TimeSpan TimeDifference
    {
        get
        {
            if (fileFactory == null)
            {
                return default(TimeSpan);
            }
            return fileFactory.TimeDifference;
        }
        set
        {
            fileFactory.TimeDifference = value;
        }
    }

    public bool TimeIncludesSeconds => fileFactory.TimeIncludesSeconds;

    public FTPReply LastValidReply => lastValidReply;

    public FTPTransferType TransferType
    {
        get
        {
            return transferType;
        }
        set
        {
            CheckConnection(shouldBeConnected: true);
            string text = ASCII_CHAR;
            if (value.Equals(FTPTransferType.BINARY))
            {
                text = BINARY_CHAR;
            }
            FTPReply reply = control.SendCommand("TYPE " + text);
            lastValidReply = control.ValidateReply(reply, "200", "250");
            transferType = value;
        }
    }

    public PortRange ActivePortRange
    {
        get
        {
            return activePortRange;
        }
        set
        {
            value.ValidateRange();
            activePortRange = value;
            if (control != null)
            {
                control.SetActivePortRange(value);
            }
        }
    }

    public IPAddress ActiveIPAddress
    {
        get
        {
            return activeIPAddress;
        }
        set
        {
            activeIPAddress = value;
            if (control != null)
            {
                control.SetActiveIPAddress(value);
            }
        }
    }

    public bool AutoPassiveIPSubstitution
    {
        get
        {
            return autoPassiveIPSubstitution;
        }
        set
        {
            autoPassiveIPSubstitution = value;
            if (control != null)
            {
                control.AutoPassiveIPSubstitution = value;
            }
        }
    }

    public bool CloseStreamsAfterTransfer
    {
        get
        {
            return closeStreamsAfterTransfer;
        }
        set
        {
            closeStreamsAfterTransfer = value;
        }
    }

    public int ServerWakeupInterval
    {
        get
        {
            return noOperationInterval;
        }
        set
        {
            noOperationInterval = value;
        }
    }

    public bool TransferNotifyListings
    {
        get
        {
            return transferNotifyListings;
        }
        set
        {
            transferNotifyListings = value;
        }
    }

    public DirectoryEmptyStrings DirectoryEmptyMessages => dirEmptyStrings;

    public FileNotFoundStrings FileNotFoundMessages => fileNotFoundStrings;

    public TransferCompleteStrings TransferCompleteMessages => transferCompleteStrings;

    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    [Obsolete("Use TransferStartedEx")]
    public virtual event EventHandler TransferStarted;

    public virtual event TransferHandler TransferStartedEx;

    [Obsolete("Use TransferCompleteEx")]
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public virtual event EventHandler TransferComplete;

    public virtual event TransferHandler TransferCompleteEx;

    public virtual event BytesTransferredHandler BytesTransferred;

    public virtual event FTPMessageHandler CommandSent;

    public virtual event FTPMessageHandler ReplyReceived;

    [Obsolete("This constructor is obsolete; use the default constructor and properties instead")]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public FTPClient(string remoteHost)
        : this(remoteHost, 21, 0)
    {
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    [Obsolete("This constructor is obsolete; use the default constructor and properties instead")]
    public FTPClient(string remoteHost, int controlPort)
        : this(remoteHost, controlPort, 0)
    {
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    [Obsolete("This constructor is obsolete; use the default constructor and properties instead")]
    public FTPClient(string remoteHost, int controlPort, int timeout)
        : this(HostNameResolver.GetAddress(remoteHost), controlPort, timeout)
    {
        this.remoteHost = remoteHost;
    }

    [Obsolete("This constructor is obsolete; use the default constructor and properties instead")]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public FTPClient(IPAddress remoteAddr)
        : this(remoteAddr, 21, 0)
    {
    }

    [Obsolete("This constructor is obsolete; use the default constructor and properties instead")]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public FTPClient(IPAddress remoteAddr, int controlPort)
        : this(remoteAddr, controlPort, 0)
    {
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    [Obsolete("This constructor is obsolete; use the default constructor and properties instead")]
    public FTPClient(IPAddress remoteAddr, int controlPort, int timeout)
    {
        InitBlock();
        remoteHost = remoteAddr.ToString();
        Connect(remoteAddr, controlPort, timeout);
    }

    public FTPClient()
    {
        InitBlock();
    }

    private void InitBlock()
    {
        log = Logger.GetLogger("FTPClient");
        transferType = FTPTransferType.ASCII;
        connectMode = FTPConnectMode.PASV;
        controlPort = 21;
    }

    public virtual void Connect()
    {
        CheckConnection(shouldBeConnected: false);
        if (remoteHost == null)
        {
            throw new FTPException("RemoteHost is not set.");
        }
        Connect(remoteHost, controlPort, timeout);
    }

    internal virtual void Connect(IPAddress remoteHost, int controlPort, int timeout)
    {
        Connect(remoteHost.ToString(), controlPort, timeout);
    }

    internal virtual void Connect(string remoteHost, int controlPort, int timeout)
    {
        if (controlPort < 0)
        {
            log.Warn("Invalid control port supplied: " + controlPort + " Using default: " + 21);
            controlPort = 21;
        }
        this.controlPort = controlPort;
        if (log.DebugEnabled)
        {
            log.Debug("Connecting to " + remoteHost + ":" + controlPort);
        }
        Initialize(new FTPControlSocket(remoteHost, controlPort, timeout, controlEncoding));
    }

    internal void Initialize(FTPControlSocket control)
    {
        this.control = control;
        this.control.AutoPassiveIPSubstitution = autoPassiveIPSubstitution;
        this.control.SynchronizePassiveConnections = synchronizePassiveConnections;
        control.CommandSent += CommandSentControl;
        control.ReplyReceived += ReplyReceivedControl;
        if (activePortRange != null)
        {
            control.SetActivePortRange(activePortRange);
        }
        if (activeIPAddress != null)
        {
            control.SetActiveIPAddress(activeIPAddress);
        }
        control.StrictReturnCodes = strictReturnCodes;
        this.control.ValidateConnection();
    }

    internal void CheckConnection(bool shouldBeConnected)
    {
        if (shouldBeConnected && !Connected)
        {
            throw new FTPException("The FTP client has not yet connected to the server.  The requested action cannot be performed until after a connection has been established.");
        }
        if (!shouldBeConnected && Connected)
        {
            throw new FTPException("The FTP client has already been connected to the server.  The requested action must be performed before a connection is established.");
        }
    }

    internal void CommandSentControl(object client, FTPMessageEventArgs message)
    {
        if (this.CommandSent != null)
        {
            this.CommandSent(this, message);
        }
    }

    internal void ReplyReceivedControl(object client, FTPMessageEventArgs message)
    {
        if (this.ReplyReceived != null)
        {
            this.ReplyReceived(this, message);
        }
    }

    public void DebugResponses(bool on)
    {
        if (on)
        {
            Logger.CurrentLevel = Level.DEBUG;
        }
        else
        {
            Logger.CurrentLevel = Level.OFF;
        }
    }

    public virtual void CancelTransfer()
    {
        cancelTransfer = true;
    }

    public virtual void Login(string user, string password)
    {
        CheckConnection(shouldBeConnected: true);
        FTPReply reply = control.SendCommand("USER " + user);
        lastValidReply = control.ValidateReply(reply, "230", "331");
        if (!lastValidReply.ReplyCode.Equals("230"))
        {
            Password(password);
        }
    }

    public virtual void User(string user)
    {
        CheckConnection(shouldBeConnected: true);
        FTPReply reply = control.SendCommand("USER " + user);
        lastValidReply = control.ValidateReply(reply, "230", "331");
    }

    public virtual void Password(string password)
    {
        CheckConnection(shouldBeConnected: true);
        FTPReply reply = control.SendCommand("PASS " + password);
        lastValidReply = control.ValidateReply(reply, "230", "202", "332");
    }

    public virtual void Account(string accountInfo)
    {
        CheckConnection(shouldBeConnected: true);
        FTPReply reply = control.SendCommand("ACCT " + accountInfo);
        lastValidReply = control.ValidateReply(reply, "230", "202");
    }

    public virtual string Quote(string command, string[] validCodes)
    {
        CheckConnection(shouldBeConnected: true);
        FTPReply reply = control.SendCommand(command);
        if (validCodes != null)
        {
            lastValidReply = control.ValidateReply(reply, validCodes);
        }
        else
        {
            lastValidReply = reply;
        }
        return lastValidReply.ReplyText;
    }

    internal string GetPASVAddress(string pasvReply)
    {
        int num = -1;
        for (int i = 0; i < pasvReply.Length; i++)
        {
            if (char.IsDigit(pasvReply[i]))
            {
                num = i;
                break;
            }
        }
        int num2 = -1;
        for (int i = pasvReply.Length - 1; i >= 0; i--)
        {
            if (char.IsDigit(pasvReply[i]))
            {
                num2 = i;
                break;
            }
        }
        if (num < 0 || num2 < 0)
        {
            return null;
        }
        int length = num2 - num + 1;
        return pasvReply.Substring(num, length);
    }

    internal FTPReply SendCommand(string command)
    {
        return control.SendCommand(command);
    }

    internal void ValidateReply(FTPReply reply, string expectedReplyCode)
    {
        control.ValidateReply(reply, expectedReplyCode);
    }

    internal void ValidateReply(FTPReply reply, string[] expectedReplyCodes)
    {
        control.ValidateReply(reply, expectedReplyCodes);
    }

    public virtual long Size(string remoteFile)
    {
        CheckConnection(shouldBeConnected: true);
        FTPReply reply = control.SendCommand("SIZE " + remoteFile);
        lastValidReply = control.ValidateReply(reply, "213");
        string text = lastValidReply.ReplyText;
        int num = text.IndexOf(' ');
        if (num >= 0)
        {
            text = text.Substring(0, num);
        }
        try
        {
            return long.Parse(text);
        }
        catch (FormatException)
        {
            throw new FTPException("Failed to parse reply: " + text);
        }
    }

    public virtual void Resume()
    {
        if (transferType.Equals(FTPTransferType.ASCII))
        {
            throw new FTPException("Resume only supported for BINARY transfers");
        }
        resume = true;
    }

    public virtual void CancelResume()
    {
        Restart(0L);
        resume = false;
    }

    public void Restart(long size)
    {
        FTPReply reply = control.SendCommand("REST " + size);
        lastValidReply = control.ValidateReply(reply, "350");
    }

    public virtual void Put(string localPath, string remoteFile)
    {
        Put(localPath, remoteFile, append: false);
    }

    public virtual long Put(Stream srcStream, string remoteFile)
    {
        return Put(srcStream, remoteFile, append: false);
    }

    public virtual void Put(string localPath, string remoteFile, bool append)
    {
        if (this.TransferStarted != null)
        {
            this.TransferStarted(this, new EventArgs());
        }
        if (this.TransferStartedEx != null)
        {
            this.TransferStartedEx(this, new TransferEventArgs(localPath, remoteFile, TransferDirection.UPLOAD, transferType));
        }
        try
        {
            if (transferType == FTPTransferType.ASCII)
            {
                PutASCII(localPath, remoteFile, append);
            }
            else
            {
                PutBinary(localPath, remoteFile, append);
            }
        }
        catch (SystemException t)
        {
            log.Error("SystemException in Put(string,string,bool)", t);
            ValidateTransferOnError();
            throw;
        }
        ValidateTransfer();
        if (this.TransferComplete != null)
        {
            this.TransferComplete(this, new EventArgs());
        }
        if (this.TransferCompleteEx != null)
        {
            this.TransferCompleteEx(this, new TransferEventArgs(localPath, remoteFile, TransferDirection.UPLOAD, transferType));
        }
    }

    public virtual long Put(Stream srcStream, string remoteFile, bool append)
    {
        if (this.TransferStarted != null)
        {
            this.TransferStarted(this, new EventArgs());
        }
        if (this.TransferStartedEx != null)
        {
            this.TransferStartedEx(this, new TransferEventArgs(srcStream, remoteFile, TransferDirection.UPLOAD, transferType));
        }
        long num = 0L;
        try
        {
            num = ((transferType != FTPTransferType.ASCII) ? PutBinary(srcStream, remoteFile, append, alwaysCloseStreams: false) : PutASCII(srcStream, remoteFile, append, alwaysCloseStreams: false));
        }
        catch (SystemException t)
        {
            log.Error("SystemException in Put(Stream,string,bool)", t);
            ValidateTransferOnError();
            throw;
        }
        ValidateTransfer();
        if (this.TransferComplete != null)
        {
            this.TransferComplete(this, new EventArgs());
        }
        if (this.TransferCompleteEx != null)
        {
            this.TransferCompleteEx(this, new TransferEventArgs(srcStream, remoteFile, TransferDirection.UPLOAD, transferType));
        }
        return num;
    }

    public void ValidateTransfer()
    {
        CheckConnection(shouldBeConnected: true);
        FTPReply reply = control.ReadReply();
        if (cancelTransfer)
        {
            lastValidReply = reply;
            log.Warn("Transfer cancelled");
            throw new FTPTransferCancelledException("Transfer cancelled.");
        }
        lastValidReply = control.ValidateReply(reply, "225", "226", "250");
    }

    protected void ValidateTransferOnError()
    {
        try
        {
            CheckConnection(shouldBeConnected: true);
            if (control != null)
            {
                control.Timeout = SHORT_TIMEOUT;
            }
            ValidateTransfer();
        }
        catch (Exception t)
        {
            log.Error("Exception in ValidateTransferOnError())", t);
        }
        finally
        {
            if (control != null)
            {
                control.Timeout = timeout;
            }
        }
    }

    private void CloseDataSocket()
    {
        if (data != null)
        {
            try
            {
                data.Close();
                data = null;
            }
            catch (SystemException t)
            {
                log.Warn("Caught exception closing data socket", t);
            }
        }
    }

    protected void CloseDataSocket(Stream stream)
    {
        if (stream != null)
        {
            try
            {
                stream.Close();
            }
            catch (IOException t)
            {
                log.Warn("Caught exception closing stream", t);
            }
        }
        CloseDataSocket();
    }

    protected void CloseDataSocket(StreamReader stream)
    {
        if (stream != null)
        {
            try
            {
                stream.Close();
            }
            catch (IOException t)
            {
                log.Warn("Caught exception closing stream", t);
            }
        }
        CloseDataSocket();
    }

    protected void CloseDataSocket(StreamWriter stream)
    {
        if (stream != null)
        {
            try
            {
                stream.Close();
            }
            catch (IOException t)
            {
                log.Warn("Caught exception closing stream", t);
            }
        }
        CloseDataSocket();
    }

    private DateTime SendServerWakeup(DateTime start)
    {
        if (noOperationInterval == 0)
        {
            return start;
        }
        int num = (int)(DateTime.Now - start).TotalSeconds;
        if (num >= noOperationInterval)
        {
            log.Info("Sending server wakeup message");
            control.WriteCommand("NOOP");
            return DateTime.Now;
        }
        return start;
    }

    private void InitPut(string remoteFile, bool append)
    {
        CheckConnection(shouldBeConnected: true);
        cancelTransfer = false;
        bool flag = false;
        data = null;
        try
        {
            resumeMarker = 0L;
            if (resume)
            {
                if (transferType.Equals(FTPTransferType.ASCII))
                {
                    throw new FTPException("Resume only supported for BINARY transfers");
                }
                try
                {
                    resumeMarker = Size(remoteFile);
                }
                catch (FTPException ex)
                {
                    log.Warn("Failed to find size of file '" + remoteFile + "' for resuming (" + ex.Message + ")");
                }
            }
            data = control.CreateDataSocket(connectMode);
            data.Timeout = timeout;
            if (resume)
            {
                Restart(resumeMarker);
            }
            string text = (append ? "APPE " : "STOR ");
            FTPReply reply = control.SendCommand(text + remoteFile);
            lastValidReply = control.ValidateReply(reply, "125", "150", "151", "350");
        }
        catch (SystemException)
        {
            flag = true;
            throw;
        }
        catch (FTPException)
        {
            flag = true;
            throw;
        }
        finally
        {
            if (flag)
            {
                resume = false;
                CloseDataSocket();
            }
        }
    }

    private void PutASCII(string localPath, string remoteFile, bool append)
    {
        Stream stream = null;
        try
        {
            stream = new FileStream(localPath, FileMode.Open, FileAccess.Read);
        }
        catch (Exception t)
        {
            string text = "Failed to open file '" + localPath + "'";
            log.Error(text, t);
            throw new FTPException(text);
        }
        PutASCII(stream, remoteFile, append, alwaysCloseStreams: true);
    }

    private long PutASCII(Stream srcStream, string remoteFile, bool append, bool alwaysCloseStreams)
    {
        StreamReader streamReader = null;
        StreamWriter streamWriter = null;
        Exception ex = null;
        long num = 0L;
        try
        {
            streamReader = ((dataEncoding == null) ? new StreamReader(srcStream) : new StreamReader(srcStream, dataEncoding));
            InitPut(remoteFile, append);
            streamWriter = ((dataEncoding == null) ? new StreamWriter(GetOutputStream()) : new StreamWriter(GetOutputStream(), dataEncoding));
            long num2 = 0L;
            string text = null;
            DateTime start = DateTime.Now;
            if (throttler != null)
            {
                throttler.Reset();
            }
            while ((text = streamReader.ReadLine()) != null && !cancelTransfer)
            {
                num += text.Length + 2;
                num2 += text.Length + 2;
                streamWriter.Write(text);
                streamWriter.Write("\r\n");
                if (throttler != null)
                {
                    throttler.ThrottleTransfer(num);
                }
                if (this.BytesTransferred != null && !cancelTransfer && num2 >= monitorInterval)
                {
                    this.BytesTransferred(this, new BytesTransferredEventArgs(remoteFile, num, 0L));
                    num2 = 0L;
                }
                start = SendServerWakeup(start);
            }
        }
        catch (Exception ex2)
        {
            ex = ex2;
        }
        finally
        {
            try
            {
                if (alwaysCloseStreams || closeStreamsAfterTransfer)
                {
                    log.Debug("Closing source stream");
                    srcStream.Close();
                    streamReader?.Close();
                }
            }
            catch (SystemException t)
            {
                log.Warn("Caught exception closing stream", t);
            }
            try
            {
                streamWriter?.Flush();
            }
            catch (SystemException t2)
            {
                log.Warn("Caught exception flushing output-stream", t2);
            }
            CloseDataSocket(streamWriter);
            if (ex != null)
            {
                log.Error("Caught exception", ex);
                throw ex;
            }
            if (this.BytesTransferred != null && !cancelTransfer)
            {
                this.BytesTransferred(this, new BytesTransferredEventArgs(remoteFile, num, 0L));
            }
        }
        return num;
    }

    private void PutBinary(string localPath, string remoteFile, bool append)
    {
        Stream stream = null;
        try
        {
            stream = new FileStream(localPath, FileMode.Open, FileAccess.Read);
        }
        catch (Exception t)
        {
            string text = "Failed to open file '" + localPath + "'";
            log.Error(text, t);
            throw new FTPException(text);
        }
        PutBinary(stream, remoteFile, append, alwaysCloseStreams: true);
    }

    private long PutBinary(Stream srcStream, string remoteFile, bool append, bool alwaysCloseStreams)
    {
        BufferedStream bufferedStream = null;
        BufferedStream bufferedStream2 = null;
        Exception ex = null;
        long num = 0L;
        try
        {
            bufferedStream = new BufferedStream(srcStream);
            InitPut(remoteFile, append);
            bufferedStream2 = new BufferedStream(GetOutputStream());
            if (resume)
            {
                bufferedStream.Seek(resumeMarker, SeekOrigin.Current);
            }
            else
            {
                resumeMarker = 0L;
            }
            byte[] array = new byte[transferBufferSize];
            long num2 = 0L;
            int num3 = 0;
            DateTime start = DateTime.Now;
            if (throttler != null)
            {
                throttler.Reset();
            }
            while ((num3 = bufferedStream.Read(array, 0, array.Length)) > 0 && !cancelTransfer)
            {
                bufferedStream2.Write(array, 0, num3);
                num += num3;
                num2 += num3;
                if (throttler != null)
                {
                    throttler.ThrottleTransfer(num);
                }
                if (this.BytesTransferred != null && !cancelTransfer && num2 >= monitorInterval)
                {
                    this.BytesTransferred(this, new BytesTransferredEventArgs(remoteFile, num, resumeMarker));
                    num2 = 0L;
                }
                start = SendServerWakeup(start);
            }
        }
        catch (Exception ex2)
        {
            ex = ex2;
        }
        finally
        {
            resume = false;
            try
            {
                if (alwaysCloseStreams || closeStreamsAfterTransfer)
                {
                    log.Debug("Closing source stream");
                    bufferedStream?.Close();
                }
            }
            catch (SystemException t)
            {
                log.Warn("Caught exception closing stream", t);
            }
            try
            {
                bufferedStream2?.Flush();
            }
            catch (SystemException t2)
            {
                log.Warn("Caught exception flushing output-stream", t2);
            }
            CloseDataSocket(bufferedStream2);
            if (ex != null)
            {
                log.Error("Caught exception", ex);
                throw ex;
            }
            if (this.BytesTransferred != null && !cancelTransfer)
            {
                this.BytesTransferred(this, new BytesTransferredEventArgs(remoteFile, num, resumeMarker));
            }
            log.Debug("Transferred " + num + " bytes to remote host");
        }
        return num;
    }

    public virtual void Put(byte[] bytes, string remoteFile)
    {
        Put(bytes, remoteFile, append: false);
    }

    public virtual void Put(byte[] bytes, string remoteFile, bool append)
    {
        MemoryStream memoryStream = new MemoryStream(bytes);
        Put(memoryStream, remoteFile, append);
        memoryStream.Close();
    }

    protected virtual Stream GetOutputStream()
    {
        return data.DataStream;
    }

    protected virtual Stream GetInputStream()
    {
        return data.DataStream;
    }

    public virtual void Get(string localPath, string remoteFile)
    {
        if (Directory.Exists(localPath))
        {
            localPath = Path.Combine(localPath, remoteFile);
            log.Debug("Setting local path to " + localPath);
        }
        if (this.TransferStarted != null)
        {
            this.TransferStarted(this, new EventArgs());
        }
        if (this.TransferStartedEx != null)
        {
            TransferEventArgs transferEventArgs = new TransferEventArgs(localPath, remoteFile, TransferDirection.DOWNLOAD, transferType);
            this.TransferStartedEx(this, transferEventArgs);
            localPath = transferEventArgs.LocalFilePath;
        }
        try
        {
            if (transferType == FTPTransferType.ASCII)
            {
                GetASCII(localPath, remoteFile);
            }
            else
            {
                GetBinary(localPath, remoteFile);
            }
        }
        catch (SystemException t)
        {
            ValidateTransferOnError();
            log.Error("SystemException in Get(string,string)", t);
            throw;
        }
        ValidateTransfer();
        if (this.TransferComplete != null)
        {
            this.TransferComplete(this, new EventArgs());
        }
        if (this.TransferCompleteEx != null)
        {
            this.TransferCompleteEx(this, new TransferEventArgs(localPath, remoteFile, TransferDirection.DOWNLOAD, transferType));
        }
    }

    public virtual void Get(Stream destStream, string remoteFile)
    {
        if (this.TransferStarted != null)
        {
            this.TransferStarted(this, new EventArgs());
        }
        if (this.TransferStartedEx != null)
        {
            this.TransferStartedEx(this, new TransferEventArgs(destStream, remoteFile, TransferDirection.DOWNLOAD, transferType));
        }
        try
        {
            if (transferType == FTPTransferType.ASCII)
            {
                GetASCII(destStream, remoteFile);
            }
            else
            {
                GetBinary(destStream, remoteFile);
            }
        }
        catch (SystemException t)
        {
            ValidateTransferOnError();
            log.Error("SystemException in Get(Stream,string)", t);
            throw;
        }
        ValidateTransfer();
        if (this.TransferComplete != null)
        {
            this.TransferComplete(this, new EventArgs());
        }
        if (this.TransferCompleteEx != null)
        {
            this.TransferCompleteEx(this, new TransferEventArgs(destStream, remoteFile, TransferDirection.DOWNLOAD, transferType));
        }
    }

    private void InitGet(string remoteFile)
    {
        CheckConnection(shouldBeConnected: true);
        cancelTransfer = false;
        bool flag = false;
        data = null;
        try
        {
            data = control.CreateDataSocket(connectMode);
            data.Timeout = timeout;
            if (resume)
            {
                if (transferType.Equals(FTPTransferType.ASCII))
                {
                    throw new FTPException("Resume only supported for BINARY transfers");
                }
                Restart(resumeMarker);
            }
            else
            {
                resumeMarker = 0L;
            }
            FTPReply reply = control.SendCommand("RETR " + remoteFile);
            lastValidReply = control.ValidateReply(reply, "125", "150");
        }
        catch (SystemException)
        {
            flag = true;
            throw;
        }
        catch (FTPException)
        {
            flag = true;
            throw;
        }
        finally
        {
            if (flag)
            {
                resume = false;
                CloseDataSocket();
            }
        }
    }

    private void GetASCII(string localPath, string remoteFile)
    {
        FileInfo fileInfo = new FileInfo(localPath);
        StreamWriter streamWriter = null;
        if (fileInfo.Exists)
        {
            if ((fileInfo.Attributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
            {
                throw new FTPException(localPath + " is readonly - cannot write");
            }
            streamWriter = ((dataEncoding == null) ? new StreamWriter(localPath) : new StreamWriter(localPath, append: false, dataEncoding));
        }
        if (streamWriter == null)
        {
            streamWriter = ((dataEncoding == null) ? new StreamWriter(localPath) : new StreamWriter(localPath, append: false, dataEncoding));
        }
        Exception ex = null;
        long num = 0L;
        StreamReader streamReader = null;
        try
        {
            InitGet(remoteFile);
            streamReader = ((dataEncoding == null) ? new StreamReader(GetInputStream()) : new StreamReader(GetInputStream(), dataEncoding));
            long num2 = 0L;
            string text = null;
            DateTime start = DateTime.Now;
            if (throttler != null)
            {
                throttler.Reset();
            }
            while ((text = ReadLine(streamReader)) != null && !cancelTransfer)
            {
                num += text.Length + 2;
                num2 += text.Length + 2;
                streamWriter.WriteLine(text);
                if (throttler != null)
                {
                    throttler.ThrottleTransfer(num);
                }
                if (this.BytesTransferred != null && !cancelTransfer && num2 >= monitorInterval)
                {
                    this.BytesTransferred(this, new BytesTransferredEventArgs(remoteFile, num, 0L));
                    num2 = 0L;
                }
                start = SendServerWakeup(start);
            }
        }
        catch (Exception ex2)
        {
            ex = ex2;
        }
        finally
        {
            try
            {
                streamWriter.Close();
            }
            catch (SystemException t)
            {
                log.Warn("Caught exception closing output stream", t);
            }
            CloseDataSocket(streamReader);
            if (ex != null)
            {
                if (deleteOnFailure)
                {
                    fileInfo.Delete();
                }
                log.Error("Caught exception", ex);
                throw ex;
            }
            if (this.BytesTransferred != null && !cancelTransfer)
            {
                this.BytesTransferred(this, new BytesTransferredEventArgs(remoteFile, num, 0L));
            }
        }
    }

    private void GetASCII(Stream destStream, string remoteFile)
    {
        InitGet(remoteFile);
        StreamWriter streamWriter = ((dataEncoding == null) ? new StreamWriter(destStream) : new StreamWriter(destStream, dataEncoding));
        StreamReader streamReader = null;
        Exception ex = null;
        long num = 0L;
        try
        {
            streamReader = ((dataEncoding == null) ? new StreamReader(GetInputStream()) : new StreamReader(GetInputStream(), dataEncoding));
            long num2 = 0L;
            string text = null;
            DateTime start = DateTime.Now;
            if (throttler != null)
            {
                throttler.Reset();
            }
            while ((text = ReadLine(streamReader)) != null && !cancelTransfer)
            {
                num += text.Length + 2;
                num2 += text.Length + 2;
                streamWriter.WriteLine(text);
                if (throttler != null)
                {
                    throttler.ThrottleTransfer(num);
                }
                if (this.BytesTransferred != null && !cancelTransfer && num2 >= monitorInterval)
                {
                    this.BytesTransferred(this, new BytesTransferredEventArgs(remoteFile, num, 0L));
                    num2 = 0L;
                }
                start = SendServerWakeup(start);
            }
            streamWriter.Flush();
        }
        catch (Exception ex2)
        {
            ex = ex2;
        }
        finally
        {
            try
            {
                if (closeStreamsAfterTransfer)
                {
                    streamWriter.Close();
                }
            }
            catch (SystemException t)
            {
                log.Warn("Caught exception closing output stream", t);
            }
            CloseDataSocket(streamReader);
            if (ex != null)
            {
                log.Error("Caught exception", ex);
                throw ex;
            }
            if (this.BytesTransferred != null && !cancelTransfer)
            {
                this.BytesTransferred(this, new BytesTransferredEventArgs(remoteFile, num, 0L));
            }
        }
    }

    private void GetBinary(string localPath, string remoteFile)
    {
        FileInfo fileInfo = new FileInfo(localPath);
        FileMode mode = (resume ? FileMode.Append : FileMode.Create);
        BufferedStream bufferedStream = null;
        if (fileInfo.Exists)
        {
            if ((fileInfo.Attributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
            {
                throw new FTPException(localPath + " is readonly - cannot write");
            }
            if (resume)
            {
                resumeMarker = fileInfo.Length;
            }
            else
            {
                resumeMarker = 0L;
            }
            bufferedStream = new BufferedStream(new FileStream(localPath, mode));
        }
        if (bufferedStream == null)
        {
            bufferedStream = new BufferedStream(new FileStream(localPath, mode));
        }
        BufferedStream bufferedStream2 = null;
        long num = 0L;
        Exception ex = null;
        try
        {
            InitGet(remoteFile);
            bufferedStream2 = new BufferedStream(GetInputStream());
            long num2 = 0L;
            byte[] array = new byte[transferBufferSize];
            DateTime start = DateTime.Now;
            if (throttler != null)
            {
                throttler.Reset();
            }
            int num3;
            while ((num3 = ReadChunk(bufferedStream2, array, transferBufferSize)) > 0 && !cancelTransfer)
            {
                bufferedStream.Write(array, 0, num3);
                num += num3;
                num2 += num3;
                if (throttler != null)
                {
                    throttler.ThrottleTransfer(num);
                }
                if (this.BytesTransferred != null && !cancelTransfer && num2 >= monitorInterval)
                {
                    this.BytesTransferred(this, new BytesTransferredEventArgs(remoteFile, num, resumeMarker));
                    num2 = 0L;
                }
                start = SendServerWakeup(start);
            }
        }
        catch (Exception ex2)
        {
            ex = ex2;
        }
        finally
        {
            resume = false;
            try
            {
                bufferedStream.Close();
            }
            catch (SystemException t)
            {
                log.Warn("Caught exception closing output stream", t);
            }
            CloseDataSocket(bufferedStream2);
            if (ex != null)
            {
                if (deleteOnFailure)
                {
                    fileInfo.Delete();
                }
                log.Error("Caught exception", ex);
                throw ex;
            }
            if (this.BytesTransferred != null && !cancelTransfer)
            {
                this.BytesTransferred(this, new BytesTransferredEventArgs(remoteFile, num, resumeMarker));
            }
            log.Debug("Transferred " + num + " bytes from remote host");
        }
    }

    private void GetBinary(Stream destStream, string remoteFile)
    {
        InitGet(remoteFile);
        BufferedStream bufferedStream = new BufferedStream(destStream);
        BufferedStream bufferedStream2 = null;
        long num = 0L;
        Exception ex = null;
        try
        {
            bufferedStream2 = new BufferedStream(GetInputStream());
            long num2 = 0L;
            byte[] array = new byte[transferBufferSize];
            DateTime start = DateTime.Now;
            if (throttler != null)
            {
                throttler.Reset();
            }
            int num3;
            while ((num3 = ReadChunk(bufferedStream2, array, transferBufferSize)) > 0 && !cancelTransfer)
            {
                bufferedStream.Write(array, 0, num3);
                num += num3;
                num2 += num3;
                if (throttler != null)
                {
                    throttler.ThrottleTransfer(num);
                }
                if (this.BytesTransferred != null && !cancelTransfer && num2 >= monitorInterval)
                {
                    this.BytesTransferred(this, new BytesTransferredEventArgs(remoteFile, num, resumeMarker));
                    num2 = 0L;
                }
                start = SendServerWakeup(start);
            }
            bufferedStream.Flush();
        }
        catch (Exception ex2)
        {
            ex = ex2;
        }
        finally
        {
            try
            {
                if (closeStreamsAfterTransfer)
                {
                    bufferedStream.Close();
                }
            }
            catch (SystemException t)
            {
                log.Warn("Caught exception closing output stream", t);
            }
            CloseDataSocket(bufferedStream2);
            if (ex != null)
            {
                log.Error("Caught exception", ex);
                throw ex;
            }
            if (this.BytesTransferred != null && !cancelTransfer)
            {
                this.BytesTransferred(this, new BytesTransferredEventArgs(remoteFile, num, resumeMarker));
            }
            log.Debug("Transferred " + num + " bytes from remote host");
        }
    }

    public virtual byte[] Get(string remoteFile)
    {
        if (this.TransferStarted != null)
        {
            this.TransferStarted(this, new EventArgs());
        }
        if (this.TransferStartedEx != null)
        {
            this.TransferStartedEx(this, new TransferEventArgs(new byte[0], remoteFile, TransferDirection.DOWNLOAD, transferType));
        }
        InitGet(remoteFile);
        BufferedStream bufferedStream = null;
        long num = 0L;
        Exception ex = null;
        MemoryStream memoryStream = null;
        byte[] array = null;
        try
        {
            bufferedStream = new BufferedStream(GetInputStream());
            long num2 = 0L;
            byte[] array2 = new byte[transferBufferSize];
            memoryStream = new MemoryStream(transferBufferSize);
            DateTime start = DateTime.Now;
            if (throttler != null)
            {
                throttler.Reset();
            }
            int num3;
            while ((num3 = ReadChunk(bufferedStream, array2, transferBufferSize)) > 0 && !cancelTransfer)
            {
                memoryStream.Write(array2, 0, num3);
                num += num3;
                num2 += num3;
                if (throttler != null)
                {
                    throttler.ThrottleTransfer(num);
                }
                if (this.BytesTransferred != null && !cancelTransfer && num2 >= monitorInterval)
                {
                    this.BytesTransferred(this, new BytesTransferredEventArgs(remoteFile, num, resumeMarker));
                    num2 = 0L;
                }
                start = SendServerWakeup(start);
            }
        }
        catch (Exception ex2)
        {
            ex = ex2;
        }
        finally
        {
            try
            {
                memoryStream?.Close();
            }
            catch (SystemException t)
            {
                log.Warn("Caught exception closing stream", t);
            }
            CloseDataSocket(bufferedStream);
            if (ex != null)
            {
                log.Error("Caught exception", ex);
                throw ex;
            }
            if (this.BytesTransferred != null && !cancelTransfer)
            {
                this.BytesTransferred(this, new BytesTransferredEventArgs(remoteFile, num, resumeMarker));
            }
            ValidateTransfer();
            array = memoryStream?.ToArray();
            if (this.TransferComplete != null)
            {
                this.TransferComplete(this, new EventArgs());
            }
            if (this.TransferCompleteEx != null)
            {
                this.TransferCompleteEx(this, new TransferEventArgs(array, remoteFile, TransferDirection.UPLOAD, transferType));
            }
        }
        return array;
    }

    public virtual bool Site(string command)
    {
        CheckConnection(shouldBeConnected: true);
        FTPReply fTPReply = control.SendCommand("SITE " + command);
        lastValidReply = control.ValidateReply(fTPReply, "200", "202", "250", "502");
        if (fTPReply.ReplyCode.Equals("200"))
        {
            return true;
        }
        return false;
    }

    public virtual FTPFile[] DirDetails()
    {
        return DirDetails(null);
    }

    public virtual FTPFile[] DirDetails(string dirname)
    {
        if (fileFactory == null)
        {
            fileFactory = new FTPFileFactory();
        }
        if (!fileFactory.ParserSetExplicitly && fileFactory.System == null)
        {
            try
            {
                fileFactory.System = GetSystem();
            }
            catch (FTPException t)
            {
                log.Warn("SYST command failed - setting Unix as default parser", t);
                fileFactory.System = "UNIX";
            }
        }
        if (parserCulture != null)
        {
            fileFactory.ParsingCulture = parserCulture;
        }
        string text = Pwd();
        if (dirname != null && dirname.Length > 0 && dirname.IndexOf('*') < 0 && dirname.IndexOf('?') < 0)
        {
            text = Path.Combine(text, dirname);
        }
        FTPFile[] array = fileFactory.Parse(Dir(dirname, full: true));
        for (int i = 0; i < array.Length; i++)
        {
            array[i].Path = text + (text.EndsWith("/") ? "" : "/") + array[i].Name;
        }
        return array;
    }

    public virtual string[] Dir()
    {
        return Dir(null, full: false);
    }

    public virtual string[] Dir(string dirname)
    {
        return Dir(dirname, full: false);
    }

    public virtual string[] Dir(string dirname, bool full)
    {
        CheckConnection(shouldBeConnected: true);
        try
        {
            data = control.CreateDataSocket(connectMode);
            data.Timeout = timeout;
            string text = (full ? "LIST " : "NLST ");
            if (showHiddenFiles)
            {
                text += "-a ";
            }
            if (dirname != null)
            {
                text += dirname;
            }
            text = text.Trim();
            FTPReply reply = control.SendCommand(text);
            lastValidReply = control.ValidateReply(reply, "125", "226", "150", "450", "550");
            string[] array = new string[0];
            string replyCode = lastValidReply.ReplyCode;
            if (!replyCode.Equals("450") && !replyCode.Equals("550") && !replyCode.Equals("226"))
            {
                Encoding encoding = ((controlEncoding == null) ? Encoding.ASCII : controlEncoding);
                ArrayList arrayList = null;
                cancelTransfer = false;
                try
                {
                    arrayList = ((!encoding.Equals(Encoding.ASCII)) ? ReadListingData(dirname, encoding) : ReadASCIIListingData(dirname));
                    reply = control.ReadReply();
                    lastValidReply = control.ValidateReply(reply, "226", "250");
                }
                catch (SystemException t)
                {
                    ValidateTransferOnError();
                    log.Error("SystemException in directory listing", t);
                    throw;
                }
                if (arrayList.Count != 0)
                {
                    log.Debug("Found " + arrayList.Count + " listing lines");
                    array = new string[arrayList.Count];
                    arrayList.CopyTo(array);
                }
                else
                {
                    log.Debug("No listing data found");
                }
            }
            else
            {
                string reply2 = lastValidReply.ReplyText.ToUpper();
                if (!dirEmptyStrings.Matches(reply2) && !transferCompleteStrings.Matches(reply2))
                {
                    throw new FTPException(reply);
                }
            }
            return array;
        }
        finally
        {
            CloseDataSocket();
        }
    }

    private ArrayList ReadListingData(string dirname, Encoding enc)
    {
        StreamReader streamReader = new StreamReader(GetInputStream(), enc);
        ArrayList arrayList = new ArrayList(10);
        string text = null;
        long num = 0L;
        long num2 = 0L;
        try
        {
            while ((text = ReadLine(streamReader)) != null && !cancelTransfer)
            {
                num += text.Length;
                num2 += text.Length;
                arrayList.Add(text);
                if (transferNotifyListings && this.BytesTransferred != null && !cancelTransfer && num2 >= monitorInterval)
                {
                    this.BytesTransferred(this, new BytesTransferredEventArgs(dirname, num, 0L));
                    num2 = 0L;
                }
                log.Debug("-->" + text);
            }
            if (transferNotifyListings && this.BytesTransferred != null)
            {
                this.BytesTransferred(this, new BytesTransferredEventArgs(dirname, num, 0L));
            }
            return arrayList;
        }
        finally
        {
            CloseDataSocket(streamReader);
        }
    }

    private ArrayList ReadASCIIListingData(string dirname)
    {
        log.Debug("Reading ASCII listing data");
        BufferedStream bufferedStream = new BufferedStream(GetInputStream());
        MemoryStream memoryStream = new MemoryStream(TransferBufferSize * 2);
        long num = 0L;
        long num2 = 0L;
        try
        {
            int num3;
            while ((num3 = bufferedStream.ReadByte()) != -1 && !cancelTransfer)
            {
                if ((num3 >= 32 || num3 == 10 || num3 == 13) && num3 <= 127)
                {
                    num++;
                    num2++;
                    memoryStream.WriteByte((byte)num3);
                    if (transferNotifyListings && this.BytesTransferred != null && !cancelTransfer && num2 >= monitorInterval)
                    {
                        this.BytesTransferred(this, new BytesTransferredEventArgs(dirname, num, 0L));
                        num2 = 0L;
                    }
                }
            }
            if (transferNotifyListings && this.BytesTransferred != null)
            {
                this.BytesTransferred(this, new BytesTransferredEventArgs(dirname, num, 0L));
            }
        }
        finally
        {
            CloseDataSocket(bufferedStream);
        }
        memoryStream.Seek(0L, SeekOrigin.Begin);
        StreamReader streamReader = new StreamReader(memoryStream, Encoding.ASCII);
        ArrayList arrayList = new ArrayList(10);
        string text = null;
        while ((text = ReadLine(streamReader)) != null)
        {
            arrayList.Add(text);
            log.Debug("-->" + text);
        }
        streamReader.Close();
        memoryStream.Close();
        return arrayList;
    }

    internal virtual int ReadChunk(Stream input, byte[] chunk, int chunksize)
    {
        return input.Read(chunk, 0, chunksize);
    }

    internal virtual int ReadChar(StreamReader input)
    {
        return input.Read();
    }

    internal virtual string ReadLine(StreamReader input)
    {
        return input.ReadLine();
    }

    public virtual void Delete(string remoteFile)
    {
        CheckConnection(shouldBeConnected: true);
        FTPReply reply = control.SendCommand("DELE " + remoteFile);
        lastValidReply = control.ValidateReply(reply, "200", "250");
    }

    public virtual void Rename(string from, string to)
    {
        CheckConnection(shouldBeConnected: true);
        FTPReply reply = control.SendCommand("RNFR " + from);
        lastValidReply = control.ValidateReply(reply, "350");
        reply = control.SendCommand("RNTO " + to);
        lastValidReply = control.ValidateReply(reply, "250");
    }

    public virtual void RmDir(string dir)
    {
        CheckConnection(shouldBeConnected: true);
        FTPReply reply = control.SendCommand("RMD " + dir);
        lastValidReply = control.ValidateReply(reply, "200", "250", "257");
    }

    public virtual void MkDir(string dir)
    {
        CheckConnection(shouldBeConnected: true);
        FTPReply reply = control.SendCommand("MKD " + dir);
        lastValidReply = control.ValidateReply(reply, "200", "250", "257");
    }

    public virtual void ChDir(string dir)
    {
        CheckConnection(shouldBeConnected: true);
        FTPReply reply = control.SendCommand("CWD " + dir);
        lastValidReply = control.ValidateReply(reply, "200", "250");
    }

    public virtual void CdUp()
    {
        CheckConnection(shouldBeConnected: true);
        FTPReply reply = control.SendCommand("CDUP");
        lastValidReply = control.ValidateReply(reply, "200", "250");
    }

    public virtual bool Exists(string remoteFile)
    {
        CheckConnection(shouldBeConnected: true);
        FTPReply fTPReply = null;
        if (sizeSupported)
        {
            fTPReply = control.SendCommand("SIZE " + remoteFile);
            switch (fTPReply.ReplyCode[0])
            {
                case '2':
                    return true;
                case '5':
                    if (fileNotFoundStrings.Matches(fTPReply.ReplyText))
                    {
                        return false;
                    }
                    break;
            }
            sizeSupported = false;
            log.Debug("SIZE not supported");
        }
        if (mdtmSupported)
        {
            fTPReply = control.SendCommand("MDTM " + remoteFile);
            switch (fTPReply.ReplyCode[0])
            {
                case '2':
                    return true;
                case '5':
                    if (fileNotFoundStrings.Matches(fTPReply.ReplyText))
                    {
                        return false;
                    }
                    break;
            }
            mdtmSupported = false;
            log.Debug("MDTM not supported");
        }
        Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        IPEndPoint localEP = new IPEndPoint(control.LocalAddress, 0);
        socket.Bind(localEP);
        try
        {
            control.SetDataPort((IPEndPoint)socket.LocalEndPoint);
        }
        finally
        {
            socket.Close();
        }
        fTPReply = control.SendCommand("RETR " + remoteFile);
        switch (fTPReply.ReplyCode[0])
        {
            case '1':
            case '2':
            case '4':
                return true;
            case '5':
                if (fileNotFoundStrings.Matches(fTPReply.ReplyText))
                {
                    return false;
                }
                break;
        }
        string text = "Unable to determine if file '" + remoteFile + "' exists.";
        log.Warn(text);
        throw new FTPException(text);
    }

    public virtual DateTime ModTime(string remoteFile)
    {
        CheckConnection(shouldBeConnected: true);
        FTPReply reply = control.SendCommand("MDTM " + remoteFile);
        lastValidReply = control.ValidateReply(reply, "213");
        DateTime time = DateTime.ParseExact(lastValidReply.ReplyText, modtimeFormats, null, DateTimeStyles.None);
        return TimeZone.CurrentTimeZone.ToLocalTime(time);
    }

    public virtual void SetModTime(string remoteFile, DateTime modTime)
    {
        CheckConnection(shouldBeConnected: true);
        string text = TimeZone.CurrentTimeZone.ToUniversalTime(modTime).ToString("yyyyMMddHHmmss");
        FTPReply reply = control.SendCommand("MFMT " + text + " " + remoteFile);
        lastValidReply = control.ValidateReply(reply, "213");
    }

    public virtual string Pwd()
    {
        CheckConnection(shouldBeConnected: true);
        FTPReply reply = control.SendCommand("PWD");
        lastValidReply = control.ValidateReply(reply, "257");
        string replyText = lastValidReply.ReplyText;
        int num = replyText.IndexOf('"');
        int num2 = replyText.LastIndexOf('"');
        if (num >= 0 && num2 > num)
        {
            return replyText.Substring(num + 1, num2 - (num + 1));
        }
        return replyText;
    }

    public virtual string[] Features()
    {
        CheckConnection(shouldBeConnected: true);
        FTPReply reply = control.SendCommand("FEAT");
        lastValidReply = control.ValidateReply(reply, "211", "500", "502");
        if (lastValidReply.ReplyCode == "211")
        {
            return lastValidReply.ReplyData;
        }
        throw new FTPException(reply);
    }

    public virtual string GetSystem()
    {
        CheckConnection(shouldBeConnected: true);
        FTPReply reply = control.SendCommand("SYST");
        lastValidReply = control.ValidateReply(reply, "200", "213", "215", "250");
        return lastValidReply.ReplyText;
    }

    public void NoOperation()
    {
        CheckConnection(shouldBeConnected: true);
        FTPReply reply = control.SendCommand("NOOP");
        lastValidReply = control.ValidateReply(reply, "200", "250");
    }

    public virtual string Help(string command)
    {
        CheckConnection(shouldBeConnected: true);
        FTPReply reply = control.SendCommand("HELP " + command);
        lastValidReply = control.ValidateReply(reply, "211", "214");
        return lastValidReply.ReplyText;
    }

    protected virtual void Abort()
    {
        CheckConnection(shouldBeConnected: true);
        FTPReply reply = control.SendCommand("ABOR");
        lastValidReply = control.ValidateReply(reply, "426", "226");
    }

    public virtual void Quit()
    {
        CheckConnection(shouldBeConnected: true);
        if (fileFactory != null)
        {
            fileFactory.System = null;
        }
        try
        {
            FTPReply reply = control.SendCommand("QUIT");
            lastValidReply = control.ValidateReply(reply, "221", "226");
        }
        finally
        {
            if (data != null)
            {
                data.Close();
            }
            data = null;
            control.Logout();
            control = null;
        }
    }

    public virtual void QuitImmediately()
    {
        if (fileFactory != null)
        {
            fileFactory.System = null;
        }
        try
        {
            if (data != null)
            {
                data.Close();
            }
        }
        finally
        {
            if (control != null)
            {
                control.Kill();
            }
            control = null;
            data = null;
        }
    }

    static FTPClient()
    {
        majorVersion = "2";
        middleVersion = "0";
        minorVersion = "1";
        buildTimestamp = "26-Oct-2009 13:44:34 EST";
        BINARY_CHAR = "I";
        ASCII_CHAR = "A";
        SHORT_TIMEOUT = 500;
        try
        {
            version = new int[3];
            version[0] = int.Parse(majorVersion);
            version[1] = int.Parse(middleVersion);
            version[2] = int.Parse(minorVersion);
        }
        catch (FormatException ex)
        {
            Console.Error.WriteLine("Failed to calculate version: " + ex.Message);
        }
    }
}
