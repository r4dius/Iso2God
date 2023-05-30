using EnterpriseDT.Util;
using EnterpriseDT.Util.Debug;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace EnterpriseDT.Net.Ftp;

[ToolboxBitmap(typeof(FTPConnection))]
[DefaultProperty("Protocol")]
public class FTPConnection : Component, IFTPComponent
{
    private delegate object RunDelegateDelegate(RunDelegateArgs delArgs);

    private class RunDelegateArgs
    {
        private Delegate del;

        private object[] args;

        public Delegate Delegate => del;

        public object[] Arguments => args;

        public RunDelegateArgs(Delegate del, object[] args)
        {
            this.del = del;
            this.args = args;
        }
    }

    protected const string DEFAULT_WORKING_DIRECTORY = null;

    private Container components = null;

    private Logger log = Logger.GetLogger("FTPConnection");

    private static int instanceCount = 0;

    private static object instanceCountMutex = new object();

    private int instanceNumber;

    private string name;

    protected object clientLock = new object();

    protected FTPClient ftpClient;

    private IFileTransferClient activeClient;

    protected string loginUserName;

    protected string loginPassword;

    protected string accountInfoStr;

    protected FTPTransferType fileTransferType;

    protected bool useAutoLogin = true;

    protected bool areEventsEnabled = true;

    protected bool isTransferringData = false;

    protected Control guiControl = null;

    private static FTPSemaphore invokeSemaphore = new FTPSemaphore(1000);

    protected bool haveQueriedForControl = false;

    protected long currentFileSize = -1L;

    protected bool useGuiThread = true;

    protected string localDir = null;

    protected string remoteDir = null;

    protected bool lastTransferCancel = false;

    internal int InstanceNumber => instanceNumber;

    [Browsable(false)]
    public override ISite Site
    {
        get
        {
            return base.Site;
        }
        set
        {
            base.Site = value;
            guiControl = (Form)FTPComponentLinker.Find(value, typeof(Form));
            FTPComponentLinker.Link(value, this);
        }
    }

    [DefaultValue(null)]
    [Description("Name of the connection.")]
    [PropertyOrder(14)]
    [Category("Connection")]
    public string Name
    {
        get
        {
            return name;
        }
        set
        {
            bool flag = name != value;
            name = value;
            if (flag)
            {
                OnPropertyChanged("Name");
            }
        }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Browsable(false)]
    public Control ParentControl
    {
        get
        {
            return guiControl;
        }
        set
        {
            guiControl = value;
        }
    }

    [PropertyOrder(0)]
    [Category("Connection")]
    [Description("File transfer protocol to use.")]
    [DefaultValue(FileTransferProtocol.FTP)]
    public virtual FileTransferProtocol Protocol
    {
        get
        {
            return FileTransferProtocol.FTP;
        }
        set
        {
            CheckConnection(shouldBeConnected: false);
            if (value != 0)
            {
                throw new FTPException(string.Concat("FTPConnection only supports standard FTP.  ", value, " is supported in SecureFTPConnection.\nSecureFTPConnection is available in edtFTPnet/PRO (www.enterprisedt.com/products/edtftpnetpro)."));
            }
        }
    }

    [Description("The assembly's version string.")]
    [Category("Version")]
    public string Version
    {
        get
        {
            int[] version = FTPClient.Version;
            return version[0] + "." + version[1] + "." + version[2];
        }
    }

    [Description("The build timestamp of the assembly.")]
    [Category("Version")]
    public string BuildTimestamp => FTPClient.BuildTimestamp;

    [DefaultValue(false)]
    [Category("FTP/FTPS")]
    [Description("Controls whether or not checking of return codes is strict.")]
    public bool StrictReturnCodes
    {
        get
        {
            return ftpClient.StrictReturnCodes;
        }
        set
        {
            bool flag = StrictReturnCodes != value;
            ftpClient.StrictReturnCodes = value;
            if (flag)
            {
                OnPropertyChanged("StrictReturnCodes");
            }
        }
    }

    [Description("IP address of the client as the server sees it.")]
    [DefaultValue("")]
    [Category("FTP/FTPS")]
    public string PublicIPAddress
    {
        get
        {
            if (ftpClient.ActiveIPAddress == null)
            {
                return "";
            }
            return ftpClient.ActiveIPAddress.ToString();
        }
        set
        {
            bool flag = PublicIPAddress != value;
            if (value == null || value == "")
            {
                ftpClient.ActiveIPAddress = null;
            }
            else
            {
                try
                {
                    ftpClient.ActiveIPAddress = IPAddress.Parse(value);
                }
                catch (FormatException)
                {
                    ftpClient.ActiveIPAddress = null;
                }
            }
            if (flag)
            {
                OnPropertyChanged("PublicIPAddress");
            }
        }
    }

    [Description("Ensures that data-socket connections are made to the same IP address that the control socket is connected to.")]
    [DefaultValue(true)]
    [Category("FTP/FTPS")]
    public bool AutoPassiveIPSubstitution
    {
        get
        {
            return ftpClient.AutoPassiveIPSubstitution;
        }
        set
        {
            bool flag = AutoPassiveIPSubstitution != value;
            ftpClient.AutoPassiveIPSubstitution = value;
            if (flag)
            {
                OnPropertyChanged("AutoPassiveIPSubstitution");
            }
        }
    }

    [Category("FTP/FTPS")]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    [Description("Specifies the range of ports to be used for data-channels in active mode.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public virtual PortRange ActivePortRange => ftpClient.ActivePortRange;

    [Category("FTP/FTPS")]
    [Description("Holds fragments of server messages that indicate a file was not found.")]
    public FileNotFoundStrings FileNotFoundMessages => ftpClient.FileNotFoundMessages;

    [Description("Holds fragments of server messages that indicate a transfer completed.")]
    [Category("FTP/FTPS")]
    public TransferCompleteStrings TransferCompleteMessages => ftpClient.TransferCompleteMessages;

    [Category("FTP/FTPS")]
    [Description("Holds fragments of server messages that indicate a directory is empty.")]
    public DirectoryEmptyStrings DirectoryEmptyMessages => ftpClient.DirectoryEmptyMessages;

    [Category("Transfer")]
    [Description("TCP timeout (in milliseconds) on the underlying sockets (0 means none).")]
    [DefaultValue(120000)]
    public virtual int Timeout
    {
        get
        {
            return ftpClient.Timeout;
        }
        set
        {
            bool flag = Timeout != value;
            ftpClient.Timeout = value;
            if (flag)
            {
                OnPropertyChanged("Timeout");
            }
        }
    }

    [Description("Include hidden files in operations that involve directory listings.")]
    [DefaultValue(false)]
    [Category("FTP/FTPS")]
    public virtual bool ShowHiddenFiles
    {
        get
        {
            return ftpClient.ShowHiddenFiles;
        }
        set
        {
            bool flag = ShowHiddenFiles != value;
            ftpClient.ShowHiddenFiles = value;
            if (flag)
            {
                OnPropertyChanged("ShowHiddenFiles");
            }
        }
    }

    [Category("FTP/FTPS")]
    [Description("The connection-mode of data-channels.  Usually passive when FTP client is behind a firewall.")]
    [DefaultValue(FTPConnectMode.PASV)]
    public FTPConnectMode ConnectMode
    {
        get
        {
            return ftpClient.ConnectMode;
        }
        set
        {
            lock (clientLock)
            {
                bool flag = ConnectMode != value;
                ftpClient.ConnectMode = value;
                if (flag)
                {
                    OnPropertyChanged("ConnectMode");
                }
            }
        }
    }

    [Browsable(false)]
    public bool IsConnected => ActiveClient.IsConnected;

    [Browsable(false)]
    public virtual bool IsTransferring => isTransferringData;

    [Description("The number of bytes transferred between each notification of the BytesTransferred event.")]
    [DefaultValue(4096)]
    [Category("Transfer")]
    public virtual long TransferNotifyInterval
    {
        get
        {
            return ftpClient.TransferNotifyInterval;
        }
        set
        {
            bool flag = TransferNotifyInterval != value;
            ftpClient.TransferNotifyInterval = value;
            if (flag)
            {
                OnPropertyChanged("TransferNotifyInterval");
            }
        }
    }

    [DefaultValue(false)]
    [Category("Transfer")]
    [Description("Controls if BytesTransferred event is triggered during directory listings.")]
    public virtual bool TransferNotifyListings
    {
        get
        {
            return ftpClient.TransferNotifyListings;
        }
        set
        {
            bool flag = TransferNotifyListings != value;
            ftpClient.TransferNotifyListings = value;
            if (flag)
            {
                OnPropertyChanged("TransferNotifyListings");
            }
        }
    }

    [Description("The size of the buffers used in writing to and reading from the data sockets.")]
    [Category("Transfer")]
    [DefaultValue(4096)]
    public virtual int TransferBufferSize
    {
        get
        {
            return ftpClient.TransferBufferSize;
        }
        set
        {
            bool flag = TransferBufferSize != value;
            ftpClient.TransferBufferSize = value;
            if (flag)
            {
                OnPropertyChanged("TransferBufferSize");
            }
        }
    }

    [Category("Transfer")]
    [Description("Determines if stream-based transfer-methods should close the stream once the transfer is completed.")]
    [DefaultValue(true)]
    public virtual bool CloseStreamsAfterTransfer
    {
        get
        {
            return ftpClient.CloseStreamsAfterTransfer;
        }
        set
        {
            bool flag = CloseStreamsAfterTransfer != value;
            ftpClient.CloseStreamsAfterTransfer = value;
            if (flag)
            {
                OnPropertyChanged("CloseStreamsAfterTransfer");
            }
        }
    }

    [Description("The domain-name or IP address of the FTP server.")]
    [Category("Connection")]
    [PropertyOrder(1)]
    [DefaultValue(null)]
    public virtual string ServerAddress
    {
        get
        {
            return ftpClient.RemoteHost;
        }
        set
        {
            bool flag = ServerAddress != value;
            ftpClient.RemoteHost = value;
            if (flag)
            {
                OnPropertyChanged("ServerAddress");
            }
        }
    }

    [DefaultValue(21)]
    [PropertyOrder(2)]
    [Description("Port on the server to which to connect the control-channel.")]
    [Category("Connection")]
    public virtual int ServerPort
    {
        get
        {
            return ftpClient.ControlPort;
        }
        set
        {
            bool flag = ServerPort != value;
            ftpClient.ControlPort = value;
            if (flag)
            {
                OnPropertyChanged("ServerPort");
            }
        }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    [Obsolete("Use ServerDirectory.")]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public string WorkingDirectory
    {
        get
        {
            return ServerDirectory;
        }
        set
        {
            ServerDirectory = value;
        }
    }

    [Category("Connection")]
    [DefaultValue(null)]
    [PropertyOrder(5)]
    [Description("Current/initial working directory on server.")]
    public string ServerDirectory
    {
        get
        {
            return remoteDir;
        }
        set
        {
            lock (clientLock)
            {
                bool flag = ServerDirectory != value;
                if (IsConnected)
                {
                    ChangeWorkingDirectory(value);
                }
                else
                {
                    remoteDir = value;
                }
                if (flag)
                {
                    OnPropertyChanged("ServerDirectory");
                }
            }
        }
    }

    [DefaultValue(null)]
    [PropertyOrder(6)]
    [Category("Connection")]
    [Description("Working directory on the local file-system into which files are downloaded.")]
    public string LocalDirectory
    {
        get
        {
            return localDir;
        }
        set
        {
            if (localDir == value)
            {
                return;
            }
            string oldDirectory = localDir;
            if (base.DesignMode)
            {
                localDir = value;
            }
            else
            {
                if (value != null && !Directory.Exists(value))
                {
                    log.Error("Directory {0} does not exist.  Leaving LocalDirectory unchanged.", null, value);
                    return;
                }
                if (OnChangingLocalDirectory(localDir, value))
                {
                    if (!Path.IsPathRooted(value))
                    {
                        throw new IOException("The specified path '" + value + "' is not absolute.");
                    }
                    localDir = value;
                    log.Debug("Set LocalDirectory='" + value + "'");
                    OnChangedLocalDirectory(oldDirectory, localDir, wasCancelled: false);
                }
                else
                {
                    OnChangedLocalDirectory(oldDirectory, localDir, wasCancelled: true);
                }
            }
            OnPropertyChanged("LocalDirectory");
        }
    }

    [Category("Transfer")]
    [DefaultValue(true)]
    [Description("Controls whether or not a file is deleted when a failure occurs while it is transferred.")]
    public virtual bool DeleteOnFailure
    {
        get
        {
            return ftpClient.DeleteOnFailure;
        }
        set
        {
            bool flag = DeleteOnFailure != value;
            ftpClient.DeleteOnFailure = value;
            if (flag)
            {
                OnPropertyChanged("DeleteOnFailure");
            }
        }
    }

    [DefaultValue(null)]
    [Category("FTP/FTPS/HTTP")]
    [Description("The character-encoding to use for data transfers in ASCII mode only.")]
    public virtual Encoding DataEncoding
    {
        get
        {
            return ftpClient.DataEncoding;
        }
        set
        {
            bool flag = DataEncoding != value;
            ftpClient.DataEncoding = value;
            if (flag)
            {
                OnPropertyChanged("DataEncoding");
            }
        }
    }

    [Description("The character-encoding to use for FTP control commands and file-names.")]
    [Category("FTP/FTPS")]
    [DefaultValue(null)]
    public virtual Encoding CommandEncoding
    {
        get
        {
            return ftpClient.ControlEncoding;
        }
        set
        {
            bool flag = CommandEncoding != value;
            ftpClient.ControlEncoding = value;
            if (flag)
            {
                OnPropertyChanged("CommandEncoding");
            }
        }
    }

    [Description("Used to synchronize the creation of passive data sockets.")]
    [DefaultValue(false)]
    [Category("FTP/FTPS")]
    public bool SynchronizePassiveConnections
    {
        get
        {
            return ftpClient.SynchronizePassiveConnections;
        }
        set
        {
            bool flag = SynchronizePassiveConnections != value;
            ftpClient.SynchronizePassiveConnections = value;
            if (flag)
            {
                OnPropertyChanged("SynchronizePassiveConnections");
            }
        }
    }

    [Obsolete("Use CommandEncoding")]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Encoding FilePathEncoding
    {
        get
        {
            return CommandEncoding;
        }
        set
        {
            CommandEncoding = value;
        }
    }

    [Description("The culture for parsing file listings.")]
    [DefaultValue(typeof(CultureInfo), "")]
    [Category("FTP/FTPS")]
    public CultureInfo ParsingCulture
    {
        get
        {
            return ftpClient.ParsingCulture;
        }
        set
        {
            bool flag = ParsingCulture != value;
            ftpClient.ParsingCulture = value;
            if (flag)
            {
                OnPropertyChanged("ParsingCulture");
            }
        }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public FTPFileFactory FileInfoParser
    {
        get
        {
            return ftpClient.FTPFileFactory;
        }
        set
        {
            bool flag = FileInfoParser != value;
            ftpClient.FTPFileFactory = value;
            if (flag)
            {
                OnPropertyChanged("FileInfoParser");
            }
        }
    }

    [Description("Time difference between server and client (relative to client).")]
    [Category("FTP/FTPS")]
    [DefaultValue(typeof(TimeSpan), "00:00:00")]
    public virtual TimeSpan TimeDifference
    {
        get
        {
            return ftpClient.TimeDifference;
        }
        set
        {
            bool flag = TimeDifference != value;
            ftpClient.TimeDifference = value;
            if (flag)
            {
                OnPropertyChanged("TimeDifference");
            }
        }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public bool TimeIncludesSeconds => ftpClient.TimeIncludesSeconds;

    [Browsable(false)]
    public FTPReply LastValidReply => ftpClient.LastValidReply;

    [Description("The type of file transfer to use, i.e. BINARY or ASCII.")]
    [DefaultValue(FTPTransferType.BINARY)]
    [Category("Transfer")]
    public virtual FTPTransferType TransferType
    {
        get
        {
            return fileTransferType;
        }
        set
        {
            lock (clientLock)
            {
                bool flag = TransferType != value;
                fileTransferType = value;
                if (IsConnected)
                {
                    ActiveClient.TransferType = value;
                }
                if (flag)
                {
                    OnPropertyChanged("TransferType");
                }
            }
        }
    }

    [PropertyOrder(3)]
    [DefaultValue(null)]
    [Category("Connection")]
    [Description("User-name of account on the server.")]
    public virtual string UserName
    {
        get
        {
            return loginUserName;
        }
        set
        {
            bool flag = UserName != value;
            CheckConnection(shouldBeConnected: false);
            loginUserName = value;
            if (flag)
            {
                OnPropertyChanged("UserName");
            }
        }
    }

    [Description("Password of account on the server.")]
    [DefaultValue(null)]
    [PropertyOrder(4)]
    [Category("Connection")]
    public virtual string Password
    {
        get
        {
            return loginPassword;
        }
        set
        {
            bool flag = Password != value;
            loginPassword = value;
            if (flag)
            {
                OnPropertyChanged("Password");
            }
        }
    }

    [Description("Account information string used in FTP/FTPS.")]
    [Category("FTP/FTPS")]
    [DefaultValue(null)]
    public virtual string AccountInfo
    {
        get
        {
            return accountInfoStr;
        }
        set
        {
            bool flag = AccountInfo != value;
            CheckConnection(shouldBeConnected: false);
            accountInfoStr = value;
            if (flag)
            {
                OnPropertyChanged("AccountInfo");
            }
        }
    }

    [DefaultValue(true)]
    [Description("Determines if the component will automatically log in upon connection.")]
    [Category("FTP/FTPS")]
    public bool AutoLogin
    {
        get
        {
            return useAutoLogin;
        }
        set
        {
            bool flag = AutoLogin != value;
            useAutoLogin = value;
            if (flag)
            {
                OnPropertyChanged("AutoLogin");
            }
        }
    }

    [Browsable(false)]
    [DefaultValue(true)]
    public bool EventsEnabled
    {
        get
        {
            return areEventsEnabled;
        }
        set
        {
            bool flag = EventsEnabled != value;
            areEventsEnabled = value;
            if (flag)
            {
                OnPropertyChanged("EventsEnabled");
            }
        }
    }

    [DefaultValue(true)]
    [Browsable(false)]
    public bool UseGuiThreadIfAvailable
    {
        get
        {
            return useGuiThread;
        }
        set
        {
            bool flag = UseGuiThreadIfAvailable != value;
            useGuiThread = value;
            if (flag)
            {
                OnPropertyChanged("UseGuiThreadIfAvailable");
            }
        }
    }

    [DefaultValue(LogLevel.Information)]
    [Category("Logging")]
    [Description("Level of logging to be written '")]
    public static LogLevel LogLevel
    {
        get
        {
            return Logger.CurrentLevel.GetLevel();
        }
        set
        {
            Logger.CurrentLevel = Level.GetLevel(value);
        }
    }

    [Category("Logging")]
    [DefaultValue(null)]
    [Description("Name of file to which logs will be written.")]
    public static string LogFile
    {
        get
        {
            return Logger.PrimaryLogFile;
        }
        set
        {
            Logger.PrimaryLogFile = value;
        }
    }

    [DefaultValue(false)]
    [Category("Logging")]
    [Description("Determines whether or not logs will be written to the console.")]
    public static bool LogToConsole
    {
        get
        {
            return Logger.LogToConsole;
        }
        set
        {
            Logger.LogToConsole = value;
        }
    }

    [Description("Determines whether or not logs will be written using .NET's trace.")]
    [DefaultValue(false)]
    [Category("Logging")]
    public static bool LogToTrace
    {
        get
        {
            return Logger.LogToTrace;
        }
        set
        {
            Logger.LogToTrace = value;
        }
    }

    protected internal IFileTransferClient ActiveClient
    {
        get
        {
            return activeClient;
        }
        set
        {
            activeClient = value;
        }
    }

    [Browsable(false)]
    public bool LastTransferCancelled => lastTransferCancel;

    [Category("Connection")]
    [Description("Occurs when the component is connecting to the server.")]
    public virtual event FTPConnectionEventHandler Connecting;

    [Category("Connection")]
    [Description("Occurs when the component has connected to the server.")]
    public virtual event FTPConnectionEventHandler Connected;

    [Description("Occurs when the component is about to log in.")]
    [Category("Connection")]
    public virtual event FTPLogInEventHandler LoggingIn;

    [Category("Connection")]
    [Description("Occurs when the component has logged in.")]
    public virtual event FTPLogInEventHandler LoggedIn;

    [Description("Occurs when the component is about to close its connection to the server.")]
    [Category("Connection")]
    public virtual event FTPConnectionEventHandler Closing;

    [Category("Connection")]
    [Description("Occurs when the component has closed its connection to the server.")]
    public virtual event FTPConnectionEventHandler Closed;

    [Description("Occurs when a file is about to be uploaded to the server.")]
    [Category("File")]
    public virtual event FTPFileTransferEventHandler Uploading;

    [Category("File")]
    [Description("Occurs when a file has been uploaded to the server.")]
    public virtual event FTPFileTransferEventHandler Uploaded;

    [Description("Occurs when a file is about to be downloaded from the server.")]
    [Category("File")]
    public virtual event FTPFileTransferEventHandler Downloading;

    [Description("Occurs when a file has been downloaded from the server.")]
    [Category("File")]
    public virtual event FTPFileTransferEventHandler Downloaded;

    [Category("Transfer")]
    [Description("Occurs every time 'TransferNotifyInterval' bytes have been transferred.")]
    public virtual event BytesTransferredHandler BytesTransferred;

    [Category("File")]
    [Description("Occurs when a remote file is about to be renamed.")]
    public virtual event FTPFileRenameEventHandler RenamingFile;

    [Category("File")]
    [Description("Occurs when a remote file has been renamed.")]
    public virtual event FTPFileRenameEventHandler RenamedFile;

    [Description("Occurs when a file is about to be deleted from the server.")]
    [Category("File")]
    public virtual event FTPFileTransferEventHandler Deleting;

    [Description("Occurs when a file has been deleted from the server.")]
    [Category("File")]
    public virtual event FTPFileTransferEventHandler Deleted;

    [Browsable(false)]
    [Obsolete("Use ServerDirectoryChanging")]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public virtual event FTPDirectoryEventHandler DirectoryChanging;

    [EditorBrowsable(EditorBrowsableState.Never)]
    [Obsolete("Use ServerDirectoryChanged")]
    [Browsable(false)]
    public virtual event FTPDirectoryEventHandler DirectoryChanged;

    [Description("Occurs when the server directory is about to be changed.")]
    [Category("Directory")]
    public virtual event FTPDirectoryEventHandler ServerDirectoryChanging;

    [Description("Occurs when the server directory has been changed.")]
    [Category("Directory")]
    public virtual event FTPDirectoryEventHandler ServerDirectoryChanged;

    [Description("Occurs when the local directory is about to be changed.")]
    [Category("Directory")]
    public virtual event FTPDirectoryEventHandler LocalDirectoryChanging;

    [Description("Occurs when the local directory has been changed.")]
    [Category("Directory")]
    public virtual event FTPDirectoryEventHandler LocalDirectoryChanged;

    [Category("Directory")]
    [Description("Occurs when a directory listing operations is commenced.")]
    public virtual event FTPDirectoryListEventHandler DirectoryListing;

    [Description("Occurs when a directory listing operations is completed.")]
    [Category("Directory")]
    public virtual event FTPDirectoryListEventHandler DirectoryListed;

    [Category("Directory")]
    [Description("Occurs when a directory is about to be created on the server.")]
    public virtual event FTPDirectoryEventHandler CreatingDirectory;

    [Category("Directory")]
    [Description("Occurs when a local directory has been created on the server.")]
    public virtual event FTPDirectoryEventHandler CreatedDirectory;

    [Description("Occurs when a directory is about to be deleted on the server.")]
    [Category("Directory")]
    public virtual event FTPDirectoryEventHandler DeletingDirectory;

    [Category("Directory")]
    [Description("Occurs when a local directory has been deleted on the server.")]
    public virtual event FTPDirectoryEventHandler DeletedDirectory;

    [Category("Commands")]
    [Description("Occurs when a command is sent to the server.")]
    public virtual event FTPMessageHandler CommandSent;

    [Description("Occurs when a reply is received from the server.")]
    [Category("Commands")]
    public virtual event FTPMessageHandler ReplyReceived;

    [Description("Occurs when a property is changed.")]
    [Category("Property Changed")]
    public event PropertyChangedEventHandler PropertyChanged;

    public FTPConnection(IContainer container)
        : this()
    {
        container.Add(this);
    }

    public FTPConnection()
        : this(new FTPClient())
    {
        components = new Container();
    }

    protected internal FTPConnection(FTPClient ftpClient)
    {
        lock (instanceCountMutex)
        {
            instanceNumber = instanceCount++;
        }
        this.ftpClient = ftpClient;
        activeClient = ftpClient;
        this.ftpClient.AutoPassiveIPSubstitution = true;
        this.ftpClient.BytesTransferred += ftpClient_BytesTransferred;
        fileTransferType = FTPTransferType.BINARY;
        ftpClient.CommandSent += ftpClient_CommandSent;
        ftpClient.ReplyReceived += ftpClient_ReplyReceived;
        ftpClient.ActivePortRange.PropertyChangeHandler = OnActivePortRangeChanged;
        ftpClient.FileNotFoundMessages.PropertyChangeHandler = OnFileNotFoundMessagesChanged;
        ftpClient.TransferCompleteMessages.PropertyChangeHandler = OnFileNotFoundMessagesChanged;
        ftpClient.DirectoryEmptyMessages.PropertyChangeHandler = OnDirectoryEmptyMessagesChanged;
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            if (components != null)
            {
                components.Dispose();
            }
            if (IsConnected)
            {
                Close(abruptClose: true);
            }
        }
        base.Dispose(disposing);
    }

    internal void SetIsTransferring(bool isTransferring)
    {
        isTransferringData = isTransferring;
    }

    [MethodIdentifier(MethodIdentifier.Connect)]
    public virtual void Connect()
    {
        lock (clientLock)
        {
            bool flag = false;
            try
            {
                if (LocalDirectory == null)
                {
                    LocalDirectory = Directory.GetCurrentDirectory();
                }
                if (ServerAddress == null)
                {
                    throw new FTPException("ServerAddress not set");
                }
                OnConnecting();
                ActiveClient.Connect();
                log.Debug("Connected to " + ServerAddress + " (instance=" + instanceNumber + ")");
                OnConnected(hasConnected: true);
                flag = PerformAutoLogin();
            }
            catch
            {
                OnConnected(IsConnected);
                if (!IsConnected)
                {
                    OnClosed();
                }
                throw;
            }
            if (flag)
            {
                PostLogin();
            }
        }
    }

    protected virtual void PostLogin()
    {
        ActiveClient.TransferType = fileTransferType;
        if (remoteDir != null && remoteDir.Trim().Length > 0)
        {
            try
            {
                ChangeWorkingDirectory(remoteDir);
                return;
            }
            catch (Exception ex)
            {
                log.Error("Failed to change working directory to '" + remoteDir + "': " + ex.Message);
                remoteDir = ActiveClient.Pwd();
                log.Warn("Set working directory to '" + remoteDir + "'");
                return;
            }
        }
        remoteDir = ActiveClient.Pwd();
        OnChangingServerDirectory(null, remoteDir);
        OnChangedServerDirectory(null, remoteDir, wasCancelled: false, null);
    }

    protected virtual bool PerformAutoLogin()
    {
        bool flag = false;
        if (useAutoLogin && loginUserName != null)
        {
            try
            {
                OnLoggingIn(loginUserName, loginPassword, hasLoggedIn: false);
                ftpClient.User(loginUserName);
                if (loginPassword != null && ftpClient.LastValidReply.ReplyCode != "230")
                {
                    ftpClient.Password(loginPassword);
                }
                if (accountInfoStr != null && ftpClient.LastValidReply.ReplyCode == "332")
                {
                    ftpClient.Account(accountInfoStr);
                }
                flag = true;
                log.Debug("Successfully logged in");
            }
            finally
            {
                OnLoggedIn(loginUserName, loginPassword, flag);
            }
        }
        return flag;
    }

    public void Close()
    {
        Close(abruptClose: false);
    }

    [MethodIdentifier(MethodIdentifier.Close, typeof(bool))]
    public virtual void Close(bool abruptClose)
    {
        try
        {
            OnClosing();
            log.Debug("Closing connection (instance=" + instanceNumber + ")");
            if (abruptClose)
            {
                if (isTransferringData)
                {
                    ActiveClient.CancelTransfer();
                }
                ActiveClient.QuitImmediately();
                return;
            }
            lock (clientLock)
            {
                ActiveClient.Quit();
            }
        }
        finally
        {
            OnClosed();
        }
    }

    [MethodIdentifier(MethodIdentifier.Login)]
    public virtual void Login()
    {
        CheckFTPType(ftpOnly: true);
        OnLoggingIn(loginUserName, loginPassword, hasLoggedIn: false);
        bool hasLoggedIn = false;
        lock (clientLock)
        {
            try
            {
                ftpClient.Login(loginUserName, loginPassword);
                hasLoggedIn = true;
            }
            finally
            {
                OnLoggedIn(loginUserName, loginPassword, hasLoggedIn);
            }
        }
    }

    [MethodIdentifier(MethodIdentifier.SendUserName, typeof(string))]
    public virtual void SendUserName(string user)
    {
        CheckFTPType(ftpOnly: true);
        lock (clientLock)
        {
            ftpClient.User(user);
            if (ftpClient.LastValidReply.ReplyCode == "230")
            {
                PostLogin();
            }
        }
    }

    [MethodIdentifier(MethodIdentifier.SendPassword, typeof(string))]
    public virtual void SendPassword(string loginPassword)
    {
        CheckFTPType(ftpOnly: true);
        lock (clientLock)
        {
            ftpClient.Password(loginPassword);
            if (ftpClient.LastValidReply.ReplyCode == "230")
            {
                PostLogin();
            }
        }
    }

    public virtual void SendAccountInfo(string accountInfo)
    {
        CheckFTPType(ftpOnly: true);
        lock (clientLock)
        {
            ftpClient.Account(accountInfo);
            if (ftpClient.LastValidReply.ReplyCode == "230")
            {
                PostLogin();
            }
        }
    }

    public virtual void CancelTransfer()
    {
        if (isTransferringData)
        {
            ActiveClient.CancelTransfer();
            lastTransferCancel = true;
        }
        else
        {
            log.Debug("CancelTransfer() called while not transfering data");
        }
    }

    [MethodIdentifier(MethodIdentifier.ResumeTransfer)]
    public virtual void ResumeTransfer()
    {
        log.Info("Resuming transfer");
        ActiveClient.Resume();
    }

    public virtual void CancelResume()
    {
        log.Info("Cancel resume");
        ActiveClient.CancelResume();
    }

    public virtual void UploadFile(string localPath, string remoteFile)
    {
        UploadFile(localPath, remoteFile, append: false);
    }

    [MethodIdentifier(MethodIdentifier.UploadStream, typeof(Stream), typeof(string))]
    public virtual void UploadStream(Stream srcStream, string remoteFile)
    {
        UploadStream(srcStream, remoteFile, append: false);
    }

    [MethodIdentifier(MethodIdentifier.UploadByteArray, typeof(byte[]), typeof(string))]
    public virtual void UploadByteArray(byte[] bytes, string remoteFile)
    {
        UploadByteArray(bytes, remoteFile, append: false);
    }

    public virtual void UploadFile(string localPath, string remoteFile, bool append)
    {
        log.Debug("UploadFile(" + localPath + "," + remoteFile + "," + append + ")");
        lock (clientLock)
        {
            Exception ex = null;
            string text = localPath;
            text = ((localDir == null) ? RelativePathToAbsolute(Directory.GetCurrentDirectory(), localDir) : RelativePathToAbsolute(localDir, localPath));
            try
            {
                lastTransferCancel = false;
                if (OnUploading(text, ref remoteFile, append))
                {
                    try
                    {
                        isTransferringData = true;
                        ActiveClient.Put(text, remoteFile, append);
                        return;
                    }
                    finally
                    {
                        isTransferringData = false;
                    }
                }
            }
            catch (Exception ex2)
            {
                ex = ex2;
                throw;
            }
            finally
            {
                OnUploaded(text, remoteFile, append, ex);
            }
        }
    }

    [MethodIdentifier(MethodIdentifier.UploadStream, typeof(Stream), typeof(string), typeof(bool))]
    public virtual void UploadStream(Stream srcStream, string remoteFile, bool append)
    {
        lock (clientLock)
        {
            long size = 0L;
            Exception ex = null;
            try
            {
                lastTransferCancel = false;
                if (OnUploading(srcStream, ref remoteFile, append))
                {
                    try
                    {
                        isTransferringData = true;
                        size = ActiveClient.Put(srcStream, remoteFile, append);
                        return;
                    }
                    finally
                    {
                        isTransferringData = false;
                    }
                }
            }
            catch (Exception ex2)
            {
                ex = ex2;
                throw;
            }
            finally
            {
                OnUploaded(srcStream, size, remoteFile, append, ex);
            }
        }
    }

    [MethodIdentifier(MethodIdentifier.UploadByteArray, typeof(byte[]), typeof(string), typeof(bool))]
    public virtual void UploadByteArray(byte[] bytes, string remoteFile, bool append)
    {
        lock (clientLock)
        {
            Exception ex = null;
            try
            {
                lastTransferCancel = false;
                if (OnUploading(bytes, ref remoteFile, append))
                {
                    try
                    {
                        isTransferringData = true;
                        ActiveClient.Put(bytes, remoteFile, append);
                        return;
                    }
                    finally
                    {
                        isTransferringData = false;
                    }
                }
            }
            catch (Exception ex2)
            {
                ex = ex2;
                throw;
            }
            finally
            {
                OnUploaded(bytes, remoteFile, append, ex);
            }
        }
    }

    public virtual void DownloadFile(string localPath, string remoteFile)
    {
        log.Debug("DownloadFile(" + localPath + "," + remoteFile + ")");
        string localPath2 = localPath;
        if (localDir != null)
        {
            localPath2 = RelativePathToAbsolute(localDir, localPath);
        }
        lock (clientLock)
        {
            Exception ex = null;
            try
            {
                lastTransferCancel = false;
                if (OnDownloading(ref localPath2, remoteFile))
                {
                    try
                    {
                        isTransferringData = true;
                        ActiveClient.Get(localPath2, remoteFile);
                        return;
                    }
                    finally
                    {
                        isTransferringData = false;
                    }
                }
            }
            catch (Exception ex2)
            {
                ex = ex2;
                throw;
            }
            finally
            {
                OnDownloaded(localPath2, remoteFile, ex);
            }
        }
    }

    [MethodIdentifier(MethodIdentifier.DownloadStream, typeof(Stream), typeof(string))]
    public virtual void DownloadStream(Stream destStream, string remoteFile)
    {
        lock (clientLock)
        {
            Exception ex = null;
            try
            {
                lastTransferCancel = false;
                if (OnDownloading(destStream, remoteFile))
                {
                    try
                    {
                        isTransferringData = true;
                        ActiveClient.Get(destStream, remoteFile);
                        return;
                    }
                    finally
                    {
                        isTransferringData = false;
                    }
                }
            }
            catch (Exception ex2)
            {
                ex = ex2;
                throw;
            }
            finally
            {
                OnDownloaded(destStream, remoteFile, ex);
            }
        }
    }

    [MethodIdentifier(MethodIdentifier.DownloadByteArray, typeof(string))]
    public virtual byte[] DownloadByteArray(string remoteFile)
    {
        lock (clientLock)
        {
            lastTransferCancel = false;
            byte[] array = null;
            Exception ex = null;
            try
            {
                if (OnDownloading(remoteFile))
                {
                    try
                    {
                        isTransferringData = true;
                        array = ActiveClient.Get(remoteFile);
                    }
                    finally
                    {
                        isTransferringData = false;
                    }
                }
            }
            catch (Exception ex2)
            {
                ex = ex2;
                throw;
            }
            finally
            {
                OnDownloaded(array, remoteFile, ex);
            }
            return array;
        }
    }

    [MethodIdentifier(MethodIdentifier.InvokeSiteCommand, typeof(string), typeof(string[]))]
    public virtual FTPReply InvokeSiteCommand(string command, params string[] arguments)
    {
        CheckFTPType(ftpOnly: true);
        StringBuilder stringBuilder = new StringBuilder(command);
        foreach (string value in arguments)
        {
            stringBuilder.Append(" ");
            stringBuilder.Append(value);
        }
        lock (clientLock)
        {
            ftpClient.Site(stringBuilder.ToString());
            return LastValidReply;
        }
    }

    [MethodIdentifier(MethodIdentifier.InvokeFTPCommand, typeof(string), typeof(string[]))]
    public virtual FTPReply InvokeFTPCommand(string command, params string[] validCodes)
    {
        CheckFTPType(ftpOnly: true);
        lock (clientLock)
        {
            ftpClient.Quote(command, validCodes);
            return LastValidReply;
        }
    }

    [MethodIdentifier(MethodIdentifier.GetFeatures)]
    public virtual string[] GetFeatures()
    {
        CheckFTPType(ftpOnly: true);
        lock (clientLock)
        {
            return ftpClient.Features();
        }
    }

    [MethodIdentifier(MethodIdentifier.GetSystemType)]
    public virtual string GetSystemType()
    {
        CheckFTPType(ftpOnly: true);
        lock (clientLock)
        {
            return ftpClient.GetSystem();
        }
    }

    [MethodIdentifier(MethodIdentifier.GetCommandHelp, typeof(string))]
    public virtual string GetCommandHelp(string command)
    {
        CheckFTPType(ftpOnly: true);
        lock (clientLock)
        {
            return ftpClient.Help(command);
        }
    }

    [MethodIdentifier(MethodIdentifier.GetFileInfos)]
    public virtual FTPFile[] GetFileInfos()
    {
        return GetFileInfos("");
    }

    [MethodIdentifier(MethodIdentifier.GetFileInfos, typeof(string))]
    public virtual FTPFile[] GetFileInfos(string directory)
    {
        lock (clientLock)
        {
            FTPFile[] array = null;
            try
            {
                OnDirectoryListing(directory);
                isTransferringData = true;
                array = ActiveClient.DirDetails(directory);
                if (array == null)
                {
                    array = new FTPFile[0];
                }
            }
            finally
            {
                isTransferringData = false;
                OnDirectoryListed(directory, array);
            }
            return array;
        }
    }

    [MethodIdentifier(MethodIdentifier.GetFiles)]
    public virtual string[] GetFiles()
    {
        return GetFiles("");
    }

    [MethodIdentifier(MethodIdentifier.GetFiles, typeof(string))]
    public virtual string[] GetFiles(string directory)
    {
        return GetFiles(directory, full: false);
    }

    [MethodIdentifier(MethodIdentifier.GetFiles, typeof(string), typeof(bool))]
    public virtual string[] GetFiles(string directory, bool full)
    {
        lock (clientLock)
        {
            try
            {
                isTransferringData = true;
                log.Debug("Listing directory '" + directory + "'");
                string[] array = ActiveClient.Dir(directory, full);
                log.Debug("Listed directory '" + directory + "'");
                if (array.Length == 0 && LastValidReply != null && LastValidReply.ReplyText.ToLower().IndexOf("permission") >= 0)
                {
                    FTPFile[] fileInfos = GetFileInfos(directory);
                    array = new string[fileInfos.Length];
                    for (int i = 0; i < fileInfos.Length; i++)
                    {
                        array[i] = (full ? fileInfos[i].Raw : fileInfos[i].Name);
                    }
                }
                return array;
            }
            finally
            {
                isTransferringData = false;
            }
        }
    }

    [MethodIdentifier(MethodIdentifier.DeleteDirectory, typeof(string))]
    public virtual void DeleteDirectory(string directory)
    {
        lock (clientLock)
        {
            Exception ex = null;
            bool cancelled = false;
            try
            {
                if (OnDeletingDirectory(directory))
                {
                    ActiveClient.RmDir(directory);
                }
                else
                {
                    cancelled = true;
                }
            }
            catch (Exception ex2)
            {
                ex = ex2;
                throw;
            }
            finally
            {
                OnDeletedDirectory(directory, cancelled, ex);
            }
        }
    }

    [MethodIdentifier(MethodIdentifier.CreateDirectory, typeof(string))]
    public virtual void CreateDirectory(string directory)
    {
        lock (clientLock)
        {
            Exception ex = null;
            bool cancelled = false;
            try
            {
                if (OnCreatingDirectory(directory))
                {
                    ActiveClient.MkDir(directory);
                }
                else
                {
                    cancelled = true;
                }
            }
            catch (Exception ex2)
            {
                ex = ex2;
                throw;
            }
            finally
            {
                OnCreatedDirectory(directory, cancelled, ex);
            }
        }
    }

    [MethodIdentifier(MethodIdentifier.GetWorkingDirectory)]
    [Obsolete("Use FTPConnection.ServerDirectory.")]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public string GetWorkingDirectory()
    {
        lock (clientLock)
        {
            return ActiveClient.Pwd();
        }
    }

    [MethodIdentifier(MethodIdentifier.ChangeWorkingDirectory, typeof(string))]
    public bool ChangeWorkingDirectory(string directory)
    {
        lock (clientLock)
        {
            string oldDirectory = null;
            if (areEventsEnabled && (this.DirectoryChanging != null || this.DirectoryChanged != null))
            {
                oldDirectory = remoteDir;
            }
            bool flag = false;
            Exception ex = null;
            try
            {
                if (OnChangingServerDirectory(oldDirectory, directory))
                {
                    ActiveClient.ChDir(directory);
                    remoteDir = ActiveClient.Pwd();
                }
                else
                {
                    flag = true;
                }
            }
            catch (Exception ex2)
            {
                ex = ex2;
                throw;
            }
            finally
            {
                OnChangedServerDirectory(oldDirectory, remoteDir, flag, ex);
            }
            return !flag;
        }
    }

    [MethodIdentifier(MethodIdentifier.ChangeWorkingDirectoryUp)]
    public bool ChangeWorkingDirectoryUp()
    {
        lock (clientLock)
        {
            string oldDirectory = null;
            if (areEventsEnabled && (this.DirectoryChanging != null || this.DirectoryChanged != null))
            {
                oldDirectory = ServerDirectory;
            }
            bool flag = false;
            Exception ex = null;
            try
            {
                if (OnChangingServerDirectory(oldDirectory, ".."))
                {
                    ActiveClient.CdUp();
                    remoteDir = ActiveClient.Pwd();
                }
                else
                {
                    flag = true;
                }
            }
            catch (Exception ex2)
            {
                ex = ex2;
                throw;
            }
            finally
            {
                OnChangedServerDirectory(oldDirectory, remoteDir, flag, ex);
            }
            return !flag;
        }
    }

    [MethodIdentifier(MethodIdentifier.DeleteFile, typeof(string))]
    public virtual bool DeleteFile(string remoteFile)
    {
        lock (clientLock)
        {
            bool flag = false;
            Exception ex = null;
            try
            {
                if (OnDeleting(remoteFile))
                {
                    ActiveClient.Delete(remoteFile);
                }
                else
                {
                    flag = true;
                }
            }
            catch (Exception ex2)
            {
                ex = ex2;
                throw;
            }
            finally
            {
                OnDeleted(remoteFile, flag, ex);
            }
            return !flag;
        }
    }

    [MethodIdentifier(MethodIdentifier.RenameFile, typeof(string), typeof(string))]
    public virtual bool RenameFile(string from, string to)
    {
        lock (clientLock)
        {
            bool flag = false;
            Exception ex = null;
            try
            {
                if (OnRenaming(from, to))
                {
                    ActiveClient.Rename(from, to);
                }
                else
                {
                    flag = true;
                }
            }
            catch (Exception ex2)
            {
                ex = ex2;
                throw;
            }
            finally
            {
                OnRenamed(from, to, flag, ex);
            }
            return !flag;
        }
    }

    [MethodIdentifier(MethodIdentifier.GetSize, typeof(string))]
    public virtual long GetSize(string remoteFile)
    {
        return GetSize(remoteFile, throwOnError: true);
    }

    [MethodIdentifier(MethodIdentifier.GetSize, typeof(string), typeof(bool))]
    private long GetSize(string remoteFile, bool throwOnError)
    {
        lock (clientLock)
        {
            try
            {
                return ActiveClient.Size(remoteFile);
            }
            catch (FTPException ex)
            {
                if (throwOnError)
                {
                    throw;
                }
                if (log.IsEnabledFor(Level.WARN))
                {
                    log.Warn("Could not get size of file " + remoteFile + " - " + ex.ReplyCode + " " + ex.Message);
                }
                return -1L;
            }
        }
    }

    [MethodIdentifier(MethodIdentifier.Exists, typeof(string))]
    public virtual bool Exists(string remoteFile)
    {
        lock (clientLock)
        {
            return ActiveClient.Exists(remoteFile);
        }
    }

    internal virtual bool DirectoryExists(string dir)
    {
        if (dir == null)
        {
            throw new ArgumentNullException();
        }
        if (dir.Length == 0)
        {
            throw new ArgumentException("Empty string", "dir");
        }
        lock (clientLock)
        {
            string dir2 = remoteDir;
            try
            {
                ActiveClient.ChDir(dir);
            }
            catch (Exception)
            {
                return false;
            }
            if (dir.IndexOf('/') < 0)
            {
                ActiveClient.ChDir("..");
            }
            else
            {
                ActiveClient.ChDir(dir2);
            }
            return true;
        }
    }

    [MethodIdentifier(MethodIdentifier.GetLastWriteTime, typeof(string))]
    public virtual DateTime GetLastWriteTime(string remoteFile)
    {
        lock (clientLock)
        {
            return ActiveClient.ModTime(remoteFile);
        }
    }

    public virtual void SetLastWriteTime(string remoteFile, DateTime lastWriteTime)
    {
        lock (clientLock)
        {
            ActiveClient.SetModTime(remoteFile, lastWriteTime);
        }
    }

    protected virtual void InvokeEventHandler(Delegate eventHandler, object sender, EventArgs e)
    {
        InvokeEventHandler(preferGuiThread: true, eventHandler, sender, e);
    }

    protected virtual void InvokeEventHandler(bool preferGuiThread, Delegate eventHandler, object sender, EventArgs e)
    {
        bool flag = e is FTPCancelableEventArgs && ((FTPCancelableEventArgs)e).CanBeCancelled;
        InvokeDelegate(preferGuiThread, !flag, eventHandler, sender, e);
    }

    protected internal object InvokeDelegate(bool preferGuiThread, bool permitAsync, Delegate del, params object[] args)
    {
        FTPEventArgs fTPEventArgs = ((args.Length == 2 && args[1] is FTPEventArgs) ? ((FTPEventArgs)args[1]) : null);
        if (useGuiThread && preferGuiThread && guiControl == null && !haveQueriedForControl)
        {
            try
            {
                if (base.Container is Control)
                {
                    guiControl = (Control)base.Container;
                }
                else
                {
                    IntPtr mainWindowHandle = Process.GetCurrentProcess().MainWindowHandle;
                    if (mainWindowHandle != IntPtr.Zero)
                    {
                        guiControl = Control.FromHandle(mainWindowHandle);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Log(Level.ALL, "Error while getting GUI control", ex);
            }
            finally
            {
                haveQueriedForControl = true;
            }
        }
        if (useGuiThread && preferGuiThread && guiControl != null && guiControl.InvokeRequired && !guiControl.IsDisposed)
        {
            if (fTPEventArgs != null)
            {
                fTPEventArgs.IsGuiThread = true;
            }
            invokeSemaphore.WaitOne(300000);
            IAsyncResult asyncResult = guiControl.BeginInvoke(new RunDelegateDelegate(RunDelegate), new RunDelegateArgs(del, args));
            if (permitAsync)
            {
                return null;
            }
            asyncResult.AsyncWaitHandle.WaitOne();
            return guiControl.EndInvoke(asyncResult);
        }
        if (fTPEventArgs != null)
        {
            fTPEventArgs.IsGuiThread = false;
        }
        return del.DynamicInvoke(args);
    }

    private object RunDelegate(RunDelegateArgs delArgs)
    {
        try
        {
            return delArgs.Delegate.DynamicInvoke(delArgs.Arguments);
        }
        catch (Exception t)
        {
            log.Error(t);
            return null;
        }
        finally
        {
            invokeSemaphore.Release();
        }
    }

    protected virtual void OnConnecting()
    {
        RaiseConnecting(new FTPConnectionEventArgs(ServerAddress, ServerPort, connected: false));
    }

    protected virtual void OnConnected(bool hasConnected)
    {
        RaiseConnected(new FTPConnectionEventArgs(ServerAddress, ServerPort, hasConnected));
    }

    protected virtual void OnLoggingIn(string userName, string password, bool hasLoggedIn)
    {
        RaiseLoggingIn(new FTPLogInEventArgs(userName, password, hasLoggedIn));
    }

    protected virtual void OnLoggedIn(string userName, string password, bool hasLoggedIn)
    {
        RaiseLoggedIn(new FTPLogInEventArgs(userName, password, hasLoggedIn));
    }

    protected virtual void OnClosing()
    {
        RaiseClosing(new FTPConnectionEventArgs(ServerAddress, ServerPort, connected: true));
    }

    protected virtual void OnClosed()
    {
        RaiseClosed(new FTPConnectionEventArgs(ServerAddress, ServerPort, connected: false));
    }

    protected bool OnUploading(string localPath, ref string remoteFile, bool append)
    {
        if (areEventsEnabled && this.Uploading != null)
        {
            long num = 0L;
            try
            {
                num = new FileInfo(localPath).Length;
            }
            catch (Exception t)
            {
                string text = "Failed to open file '" + localPath + "'";
                log.Error(text, t);
                throw new FTPException(text);
            }
            FTPFileTransferEventArgs fTPFileTransferEventArgs = new FTPFileTransferEventArgs(canBeCancelled: true, localPath, remoteFile, ServerDirectory, num, append, cancelled: false, null);
            RaiseUploading(fTPFileTransferEventArgs);
            remoteFile = fTPFileTransferEventArgs.RemoteFile;
            lastTransferCancel = fTPFileTransferEventArgs.Cancel;
            return !fTPFileTransferEventArgs.Cancel;
        }
        return true;
    }

    protected virtual void OnUploaded(string localPath, string remoteFile, bool append, Exception ex)
    {
        if (areEventsEnabled && this.Uploaded != null)
        {
            if (!Path.IsPathRooted(localPath))
            {
                localPath = Path.Combine(localDir, localPath);
            }
            long num = 0L;
            try
            {
                num = new FileInfo(localPath).Length;
            }
            catch (Exception t)
            {
                string text = "Failed to open file '" + localPath + "'";
                log.Error(text, t);
                throw new FTPException(text);
            }
            RaiseUploaded(new FTPFileTransferEventArgs(canBeCancelled: false, localPath, remoteFile, ServerDirectory, num, append, lastTransferCancel, ex));
        }
    }

    protected bool OnUploading(Stream srcStream, ref string remoteFile, bool append)
    {
        if (areEventsEnabled && this.Uploading != null)
        {
            FTPFileTransferEventArgs fTPFileTransferEventArgs = new FTPFileTransferEventArgs(canBeCancelled: true, srcStream, remoteFile, ServerDirectory, srcStream.Length, append, cancelled: false, null);
            RaiseUploading(fTPFileTransferEventArgs);
            remoteFile = fTPFileTransferEventArgs.RemoteFile;
            lastTransferCancel = fTPFileTransferEventArgs.Cancel;
            return !fTPFileTransferEventArgs.Cancel;
        }
        return true;
    }

    protected virtual void OnUploaded(Stream srcStream, long size, string remoteFile, bool append, Exception ex)
    {
        RaiseUploaded(new FTPFileTransferEventArgs(canBeCancelled: false, srcStream, remoteFile, ServerDirectory, size, append, lastTransferCancel, ex));
    }

    protected bool OnUploading(byte[] bytes, ref string remoteFile, bool append)
    {
        if (areEventsEnabled && this.Uploading != null)
        {
            FTPFileTransferEventArgs fTPFileTransferEventArgs = new FTPFileTransferEventArgs(canBeCancelled: true, remoteFile, ServerDirectory, bytes.Length, append, cancelled: false, null);
            RaiseUploading(fTPFileTransferEventArgs);
            remoteFile = fTPFileTransferEventArgs.RemoteFile;
            lastTransferCancel = fTPFileTransferEventArgs.Cancel;
            return !fTPFileTransferEventArgs.Cancel;
        }
        return true;
    }

    protected virtual void OnUploaded(byte[] bytes, string remoteFile, bool append, Exception ex)
    {
        RaiseUploaded(new FTPFileTransferEventArgs(canBeCancelled: false, bytes, remoteFile, ServerDirectory, bytes.Length, append, lastTransferCancel, ex));
    }

    protected bool OnDownloading(ref string localPath, string remoteFile)
    {
        if (areEventsEnabled && (this.Downloading != null || this.Downloaded != null))
        {
            currentFileSize = GetSize(remoteFile, throwOnError: false);
        }
        if (areEventsEnabled && this.Downloading != null)
        {
            FTPFileTransferEventArgs fTPFileTransferEventArgs = new FTPFileTransferEventArgs(canBeCancelled: true, localPath, remoteFile, ServerDirectory, currentFileSize, append: false, cancelled: false, null);
            RaiseDownloading(fTPFileTransferEventArgs);
            localPath = fTPFileTransferEventArgs.LocalPath;
            lastTransferCancel = fTPFileTransferEventArgs.Cancel;
            return !fTPFileTransferEventArgs.Cancel;
        }
        return true;
    }

    protected virtual void OnDownloaded(string localPath, string remoteFile, Exception ex)
    {
        RaiseDownloaded(new FTPFileTransferEventArgs(canBeCancelled: false, localPath, remoteFile, ServerDirectory, currentFileSize, append: false, lastTransferCancel, ex));
    }

    protected bool OnDownloading(Stream destStream, string remoteFile)
    {
        if (areEventsEnabled && (this.Downloading != null || this.Downloaded != null))
        {
            currentFileSize = GetSize(remoteFile, throwOnError: false);
        }
        if (areEventsEnabled && this.Downloading != null)
        {
            FTPFileTransferEventArgs fTPFileTransferEventArgs = new FTPFileTransferEventArgs(canBeCancelled: true, destStream, remoteFile, ServerDirectory, currentFileSize, append: false, cancelled: false, null);
            RaiseDownloading(fTPFileTransferEventArgs);
            lastTransferCancel = fTPFileTransferEventArgs.Cancel;
            return !fTPFileTransferEventArgs.Cancel;
        }
        return true;
    }

    protected virtual void OnDownloaded(Stream destStream, string remoteFile, Exception ex)
    {
        RaiseDownloaded(new FTPFileTransferEventArgs(canBeCancelled: false, destStream, remoteFile, ServerDirectory, currentFileSize, append: false, lastTransferCancel, ex));
    }

    protected bool OnDownloading(string remoteFile)
    {
        if (areEventsEnabled && (this.Downloading != null || this.Downloaded != null))
        {
            currentFileSize = GetSize(remoteFile, throwOnError: false);
        }
        if (areEventsEnabled && this.Downloading != null)
        {
            FTPFileTransferEventArgs fTPFileTransferEventArgs = new FTPFileTransferEventArgs(canBeCancelled: true, remoteFile, ServerDirectory, currentFileSize, append: false, cancelled: false, null);
            RaiseDownloading(fTPFileTransferEventArgs);
            lastTransferCancel = fTPFileTransferEventArgs.Cancel;
            return !fTPFileTransferEventArgs.Cancel;
        }
        return true;
    }

    protected virtual void OnDownloaded(byte[] bytes, string remoteFile, Exception ex)
    {
        RaiseDownloaded(new FTPFileTransferEventArgs(canBeCancelled: false, bytes, remoteFile, ServerDirectory, currentFileSize, append: false, lastTransferCancel, ex));
    }

    protected virtual void OnBytesTransferred(string remoteFile, long byteCount, long resumeOffset)
    {
        RaiseBytesTransferred(new BytesTransferredEventArgs(remoteDir, remoteFile, byteCount, resumeOffset));
    }

    protected virtual bool OnChangingServerDirectory(string oldDirectory, string newDirectory)
    {
        if (areEventsEnabled && (this.ServerDirectoryChanging != null || this.DirectoryChanging != null))
        {
            if (!PathUtil.IsAbsolute(oldDirectory))
            {
                oldDirectory = PathUtil.Combine(ServerDirectory, oldDirectory);
            }
            if (!PathUtil.IsAbsolute(newDirectory))
            {
                newDirectory = PathUtil.Combine(ServerDirectory, newDirectory);
            }
            if (this.ServerDirectoryChanging != null)
            {
                FTPDirectoryEventArgs fTPDirectoryEventArgs = new FTPDirectoryEventArgs(oldDirectory, newDirectory, cancel: false, null);
                RaiseServerDirectoryChanging(fTPDirectoryEventArgs);
                return !fTPDirectoryEventArgs.Cancel;
            }
            FTPDirectoryEventArgs fTPDirectoryEventArgs2 = new FTPDirectoryEventArgs(oldDirectory, newDirectory, cancel: false, null);
            RaiseServerDirectoryChanging(fTPDirectoryEventArgs2);
            return !fTPDirectoryEventArgs2.Cancel;
        }
        return true;
    }

    protected virtual bool OnChangingLocalDirectory(string oldDirectory, string newDirectory)
    {
        if (areEventsEnabled && this.LocalDirectoryChanging != null)
        {
            FTPDirectoryEventArgs fTPDirectoryEventArgs = new FTPDirectoryEventArgs(oldDirectory, newDirectory, cancel: false, null);
            RaiseLocalDirectoryChanging(fTPDirectoryEventArgs);
            return !fTPDirectoryEventArgs.Cancel;
        }
        return true;
    }

    protected virtual bool OnCreatingDirectory(string dir)
    {
        if (areEventsEnabled && this.CreatingDirectory != null)
        {
            if (!PathUtil.IsAbsolute(dir))
            {
                dir = PathUtil.Combine(ServerDirectory, dir);
            }
            FTPDirectoryEventArgs fTPDirectoryEventArgs = new FTPDirectoryEventArgs(null, dir, null);
            RaiseCreatingDirectory(fTPDirectoryEventArgs);
            return !fTPDirectoryEventArgs.Cancel;
        }
        return true;
    }

    protected virtual void OnCreatedDirectory(string dir, bool cancelled, Exception ex)
    {
        if (areEventsEnabled && this.CreatedDirectory != null)
        {
            if (!PathUtil.IsAbsolute(dir))
            {
                dir = PathUtil.Combine(ServerDirectory, dir);
            }
            RaiseCreatedDirectory(new FTPDirectoryEventArgs(null, dir, cancelled, ex));
        }
    }

    protected virtual bool OnDeletingDirectory(string dir)
    {
        if (areEventsEnabled && this.DeletingDirectory != null)
        {
            if (!PathUtil.IsAbsolute(dir))
            {
                dir = PathUtil.Combine(ServerDirectory, dir);
            }
            FTPDirectoryEventArgs fTPDirectoryEventArgs = new FTPDirectoryEventArgs(null, dir, null);
            RaiseDeletingDirectory(fTPDirectoryEventArgs);
            return !fTPDirectoryEventArgs.Cancel;
        }
        return true;
    }

    protected virtual void OnDeletedDirectory(string dir, bool cancelled, Exception ex)
    {
        if (areEventsEnabled && this.DeletedDirectory != null)
        {
            if (!PathUtil.IsAbsolute(dir))
            {
                dir = PathUtil.Combine(ServerDirectory, dir);
            }
            RaiseDeletedDirectory(new FTPDirectoryEventArgs(null, dir, cancelled, ex));
        }
    }

    protected virtual void OnDirectoryListing(string dir)
    {
        if (areEventsEnabled && this.DirectoryListing != null)
        {
            if (!PathUtil.IsAbsolute(dir))
            {
                dir = PathUtil.Combine(ServerDirectory, dir);
            }
            RaiseDirectoryListing(new FTPDirectoryListEventArgs(dir));
        }
    }

    protected virtual void OnDirectoryListed(string dir, FTPFile[] files)
    {
        if (areEventsEnabled && this.DirectoryListed != null)
        {
            if (!PathUtil.IsAbsolute(dir))
            {
                dir = PathUtil.Combine(ServerDirectory, dir);
            }
            RaiseDirectoryListed(new FTPDirectoryListEventArgs(dir, files));
        }
    }

    protected virtual void OnChangedServerDirectory(string oldDirectory, string newDirectory, bool wasCancelled, Exception ex)
    {
        if (areEventsEnabled && this.ServerDirectoryChanged != null)
        {
            RaiseServerDirectoryChanged(new FTPDirectoryEventArgs(oldDirectory, newDirectory, ex));
        }
        if (areEventsEnabled && this.DirectoryChanged != null)
        {
            RaiseServerDirectoryChanged(new FTPDirectoryEventArgs(oldDirectory, newDirectory, ex));
        }
    }

    protected virtual void OnChangedLocalDirectory(string oldDirectory, string newDirectory, bool wasCancelled)
    {
        RaiseLocalDirectoryChanged(new FTPDirectoryEventArgs(oldDirectory, newDirectory, null));
    }

    protected bool OnDeleting(string remoteFile)
    {
        if (areEventsEnabled && this.Deleting != null)
        {
            FTPFileTransferEventArgs fTPFileTransferEventArgs = new FTPFileTransferEventArgs(canBeCancelled: true, remoteFile, ServerDirectory, -1L, append: false, cancelled: false, null);
            RaiseDeleting(fTPFileTransferEventArgs);
            return !fTPFileTransferEventArgs.Cancel;
        }
        return true;
    }

    protected virtual void OnDeleted(string remoteFile, bool cancelled, Exception ex)
    {
        RaiseDeleted(new FTPFileTransferEventArgs(canBeCancelled: false, remoteFile, ServerDirectory, -1L, append: false, cancelled, ex));
    }

    protected virtual bool OnRenaming(string from, string to)
    {
        if (areEventsEnabled && this.RenamingFile != null)
        {
            if (!PathUtil.IsAbsolute(from))
            {
                from = PathUtil.Combine(ServerDirectory, from);
            }
            if (!PathUtil.IsAbsolute(to))
            {
                to = PathUtil.Combine(ServerDirectory, to);
            }
            FTPFileRenameEventArgs fTPFileRenameEventArgs = new FTPFileRenameEventArgs(canBeCancelled: true, from, to, cancel: false, null);
            RaiseRenamingFile(fTPFileRenameEventArgs);
            return !fTPFileRenameEventArgs.Cancel;
        }
        return true;
    }

    protected virtual void OnRenamed(string from, string to, bool cancelled, Exception ex)
    {
        if (areEventsEnabled && this.RenamedFile != null)
        {
            if (!PathUtil.IsAbsolute(from))
            {
                from = PathUtil.Combine(ServerDirectory, from);
            }
            if (!PathUtil.IsAbsolute(to))
            {
                to = PathUtil.Combine(ServerDirectory, to);
            }
            RaiseRenamedFile(new FTPFileRenameEventArgs(canBeCancelled: false, from, to, cancelled, ex));
        }
    }

    private void OnActivePortRangeChanged(object sender, PropertyChangedEventArgs e)
    {
        OnPropertyChanged("ActivePortRange." + e.PropertyName);
    }

    private void OnFileNotFoundMessagesChanged(object sender, PropertyChangedEventArgs e)
    {
        OnPropertyChanged("FileNotFoundMessagesChanged");
    }

    private void OnTransferCompleteMessagesChanged(object sender, PropertyChangedEventArgs e)
    {
        OnPropertyChanged("FileNotFoundMessages");
    }

    private void OnDirectoryEmptyMessagesChanged(object sender, PropertyChangedEventArgs e)
    {
        OnPropertyChanged("DirectoryEmptyMessages");
    }

    protected virtual void OnPropertyChanged(string propertyName)
    {
        if (this.PropertyChanged != null)
        {
            this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    protected internal void ftpClient_BytesTransferred(object sender, BytesTransferredEventArgs e)
    {
        OnBytesTransferred(e.RemoteFile, e.ByteCount, e.ResumeOffset);
    }

    protected internal void ftpClient_CommandSent(object sender, FTPMessageEventArgs e)
    {
        RaiseCommandSent(e);
    }

    protected internal virtual void ftpClient_ReplyReceived(object sender, FTPMessageEventArgs e)
    {
        RaiseReplyReceived(e);
    }

    protected internal void RaiseBytesTransferred(BytesTransferredEventArgs e)
    {
        if (areEventsEnabled && this.BytesTransferred != null)
        {
            InvokeEventHandler(this.BytesTransferred, this, e);
        }
    }

    protected internal void RaiseClosed(FTPConnectionEventArgs e)
    {
        if (areEventsEnabled && this.Closed != null)
        {
            InvokeEventHandler(this.Closed, this, e);
        }
    }

    protected internal void RaiseClosing(FTPConnectionEventArgs e)
    {
        if (areEventsEnabled && this.Closing != null)
        {
            InvokeEventHandler(this.Closing, this, e);
        }
    }

    protected internal void RaiseCommandSent(FTPMessageEventArgs e)
    {
        if (areEventsEnabled && this.CommandSent != null)
        {
            InvokeEventHandler(this.CommandSent, this, e);
        }
    }

    protected internal void RaiseConnected(FTPConnectionEventArgs e)
    {
        if (areEventsEnabled && this.Connected != null)
        {
            InvokeEventHandler(this.Connected, this, e);
        }
    }

    protected internal void RaiseConnecting(FTPConnectionEventArgs e)
    {
        if (areEventsEnabled && this.Connecting != null)
        {
            InvokeEventHandler(this.Connecting, this, e);
        }
    }

    protected internal void RaiseCreatedDirectory(FTPDirectoryEventArgs e)
    {
        if (areEventsEnabled && this.CreatedDirectory != null)
        {
            InvokeEventHandler(this.CreatedDirectory, this, e);
        }
    }

    protected internal void RaiseCreatingDirectory(FTPDirectoryEventArgs e)
    {
        if (areEventsEnabled && this.CreatingDirectory != null)
        {
            InvokeEventHandler(this.CreatingDirectory, this, e);
        }
    }

    protected internal void RaiseDeleted(FTPFileTransferEventArgs e)
    {
        if (areEventsEnabled && this.Deleted != null)
        {
            InvokeEventHandler(this.Deleted, this, e);
        }
    }

    protected internal void RaiseDeletedDirectory(FTPDirectoryEventArgs e)
    {
        if (areEventsEnabled && this.DeletedDirectory != null)
        {
            InvokeEventHandler(this.DeletedDirectory, this, e);
        }
    }

    protected internal void RaiseDeleting(FTPFileTransferEventArgs e)
    {
        if (areEventsEnabled && this.Deleting != null)
        {
            InvokeEventHandler(this.Deleting, this, e);
        }
    }

    protected internal void RaiseDeletingDirectory(FTPDirectoryEventArgs e)
    {
        if (areEventsEnabled && this.DeletingDirectory != null)
        {
            InvokeEventHandler(this.DeletingDirectory, this, e);
        }
    }

    protected internal void RaiseDirectoryChanged(FTPDirectoryEventArgs e)
    {
        if (areEventsEnabled && this.DirectoryChanged != null)
        {
            InvokeEventHandler(this.DirectoryChanged, this, e);
        }
    }

    protected internal void RaiseDirectoryChanging(FTPDirectoryEventArgs e)
    {
        if (areEventsEnabled && this.DirectoryChanging != null)
        {
            InvokeEventHandler(this.DirectoryChanging, this, e);
        }
    }

    protected internal void RaiseDirectoryListed(FTPDirectoryListEventArgs e)
    {
        if (areEventsEnabled && this.DirectoryListed != null)
        {
            InvokeEventHandler(this.DirectoryListed, this, e);
        }
    }

    protected internal void RaiseDirectoryListing(FTPDirectoryListEventArgs e)
    {
        if (areEventsEnabled && this.DirectoryListing != null)
        {
            InvokeEventHandler(this.DirectoryListing, this, e);
        }
    }

    protected internal void RaiseDownloaded(FTPFileTransferEventArgs e)
    {
        if (areEventsEnabled && this.Downloaded != null)
        {
            InvokeEventHandler(this.Downloaded, this, e);
        }
    }

    protected internal void RaiseDownloading(FTPFileTransferEventArgs e)
    {
        if (areEventsEnabled && this.Downloading != null)
        {
            InvokeEventHandler(this.Downloading, this, e);
        }
    }

    protected internal void RaiseLocalDirectoryChanged(FTPDirectoryEventArgs e)
    {
        if (areEventsEnabled && this.LocalDirectoryChanged != null)
        {
            InvokeEventHandler(this.LocalDirectoryChanged, this, e);
        }
    }

    protected internal void RaiseLocalDirectoryChanging(FTPDirectoryEventArgs e)
    {
        if (areEventsEnabled && this.LocalDirectoryChanging != null)
        {
            InvokeEventHandler(this.LocalDirectoryChanging, this, e);
        }
    }

    protected internal void RaiseLoggedIn(FTPLogInEventArgs e)
    {
        if (areEventsEnabled && this.LoggedIn != null)
        {
            InvokeEventHandler(this.LoggedIn, this, e);
        }
    }

    protected internal void RaiseLoggingIn(FTPLogInEventArgs e)
    {
        if (areEventsEnabled && this.LoggingIn != null)
        {
            InvokeEventHandler(this.LoggingIn, this, e);
        }
    }

    protected internal void RaisePropertyChanged(PropertyChangedEventArgs e)
    {
        if (areEventsEnabled && this.PropertyChanged != null)
        {
            InvokeEventHandler(this.PropertyChanged, this, e);
        }
    }

    protected internal void RaiseRenamedFile(FTPFileRenameEventArgs e)
    {
        if (areEventsEnabled && this.RenamedFile != null)
        {
            InvokeEventHandler(this.RenamedFile, this, e);
        }
    }

    protected internal void RaiseRenamingFile(FTPFileRenameEventArgs e)
    {
        if (areEventsEnabled && this.RenamingFile != null)
        {
            InvokeEventHandler(this.RenamingFile, this, e);
        }
    }

    protected internal void RaiseReplyReceived(FTPMessageEventArgs e)
    {
        if (areEventsEnabled && this.ReplyReceived != null)
        {
            InvokeEventHandler(this.ReplyReceived, this, e);
        }
    }

    protected internal void RaiseServerDirectoryChanged(FTPDirectoryEventArgs e)
    {
        if (areEventsEnabled && this.ServerDirectoryChanged != null)
        {
            InvokeEventHandler(this.ServerDirectoryChanged, this, e);
        }
    }

    protected internal void RaiseServerDirectoryChanging(FTPDirectoryEventArgs e)
    {
        if (areEventsEnabled && this.ServerDirectoryChanging != null)
        {
            InvokeEventHandler(this.ServerDirectoryChanging, this, e);
        }
    }

    protected internal void RaiseUploaded(FTPFileTransferEventArgs e)
    {
        if (areEventsEnabled && this.Uploaded != null)
        {
            InvokeEventHandler(this.Uploaded, this, e);
        }
    }

    protected internal void RaiseUploading(FTPFileTransferEventArgs e)
    {
        if (areEventsEnabled && this.Uploading != null)
        {
            InvokeEventHandler(this.Uploading, this, e);
        }
    }

    protected internal void CheckConnection(bool shouldBeConnected)
    {
        if (shouldBeConnected && !ActiveClient.IsConnected)
        {
            throw new FTPException("The FTP client has not yet connected to the server.  The requested action cannot be performed until after a connection has been established.");
        }
        if (!shouldBeConnected && ActiveClient.IsConnected)
        {
            throw new FTPException("The FTP client has already been connected to the server.  The requested action must be performed before a connection is established.");
        }
    }

    protected virtual void CheckFTPType(bool ftpOnly)
    {
        if (ftpOnly)
        {
            if (Protocol.Equals(FileTransferProtocol.HTTP))
            {
                throw new FTPException("This operation is only supported for FTP/FTPS");
            }
            if (Protocol.Equals(FileTransferProtocol.SFTP))
            {
                throw new FTPException("This operation is only supported for FTP/FTPS");
            }
        }
    }

    protected string RelativePathToAbsolute(string absolutePath, string relativePath)
    {
        log.Debug("Combining absolute path '" + absolutePath + "' with relative path '" + relativePath + "'");
        if (Path.IsPathRooted(relativePath))
        {
            return relativePath;
        }
        string fileName = Path.GetFileName(relativePath);
        if (fileName == relativePath)
        {
            return Path.Combine(absolutePath, relativePath);
        }
        string text = "\\";
        string[] array = relativePath.Split(text.ToCharArray());
        string text2 = absolutePath;
        bool flag = false;
        for (int i = 0; i < array.Length; i++)
        {
            if (array[i].Length == 0 || array[i] == ".")
            {
                continue;
            }
            if (array[i] == "..")
            {
                if (flag)
                {
                    throw new IOException("Cannot embed '..' in middle of path");
                }
                text2 = new DirectoryInfo(text2).Parent.FullName;
                continue;
            }
            flag = true;
            if (text2.Length > 0 && text2[text2.Length - 1] != '\\')
            {
                text2 += "\\";
            }
            text2 += array[i];
        }
        return text2;
    }

    public virtual string GetURL()
    {
        return GetURL(includeDirectory: true, includeUserName: true, includePassword: true);
    }

    public virtual string GetURL(bool includeDirectory, bool includeUserName, bool includePassword)
    {
        if (includePassword && !includeUserName)
        {
            throw new ArgumentException("Cannot include password in URL without also including the user-name");
        }
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("ftp://");
        if (includeUserName)
        {
            stringBuilder.Append(UserName);
            if (includePassword)
            {
                stringBuilder.Append(":" + Password);
            }
            stringBuilder.Append("@");
        }
        stringBuilder.Append(ServerAddress);
        if (ServerPort != 21)
        {
            stringBuilder.Append(":" + ServerPort);
        }
        if (includeDirectory)
        {
            if (!ServerDirectory.StartsWith("/"))
            {
                stringBuilder.Append("/");
            }
            stringBuilder.Append(ServerDirectory);
        }
        return stringBuilder.ToString();
    }

    public override int GetHashCode()
    {
        return instanceNumber;
    }

    public void LinkComponent(IFTPComponent component)
    {
    }
}
