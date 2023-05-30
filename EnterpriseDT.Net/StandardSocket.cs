using System;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace EnterpriseDT.Net;

public class StandardSocket : BaseSocket
{
    private Socket socket;

    public override int Available => socket.Available;

    public override bool Connected
    {
        get
        {
            if (socket != null)
            {
                return socket.Connected;
            }
            return false;
        }
    }

    public override EndPoint LocalEndPoint => socket.LocalEndPoint;

    public override EndPoint RemoteEndPoint => socket.RemoteEndPoint;

    public Socket Socket => socket;

    public StandardSocket(AddressFamily addressFamily, SocketType socketType, ProtocolType protocolType)
        : base(addressFamily, socketType, protocolType)
    {
        socket = new Socket(addressFamily, socketType, protocolType);
    }

    protected StandardSocket(Socket socket)
    {
        this.socket = socket;
    }

    public override BaseSocket Accept(int timeout)
    {
        if (!socket.Poll(timeout * 1000, SelectMode.SelectRead))
        {
            socket.Close();
            throw new IOException("Failed to accept connection within timeout period (" + timeout + ")");
        }
        return new StandardSocket(socket.Accept());
    }

    public override bool Poll(int microseconds, SelectMode mode)
    {
        return socket.Poll(microseconds, mode);
    }

    public override IAsyncResult BeginAccept(AsyncCallback callback, object state)
    {
        return socket.BeginAccept(callback, state);
    }

    public override BaseSocket EndAccept(IAsyncResult asyncResult)
    {
        return new StandardSocket(socket.EndAccept(asyncResult));
    }

    public override void Bind(EndPoint localEP)
    {
        socket.Bind(localEP);
    }

    public override void Close()
    {
        socket.Close();
        socket = null;
    }

    public override void Connect(EndPoint remoteEP)
    {
        if (socket == null)
        {
            socket = new Socket(addressFamily, socketType, protocolType);
        }
        socket.Connect(remoteEP);
    }

    public override void Listen(int backlog)
    {
        socket.Listen(backlog);
    }

    public override Stream GetStream()
    {
        return new NetworkStream(socket, ownsSocket: true);
    }

    public override Stream GetStream(bool ownsSocket)
    {
        return new NetworkStream(socket, ownsSocket);
    }

    public override int Receive(byte[] buffer)
    {
        return socket.Receive(buffer);
    }

    public override IAsyncResult BeginReceive(byte[] buffer, int offset, int size, SocketFlags socketFlags, AsyncCallback callback, object state)
    {
        return socket.BeginReceive(buffer, offset, size, socketFlags, callback, state);
    }

    public override int EndReceive(IAsyncResult asyncResult)
    {
        return socket.EndReceive(asyncResult);
    }

    public override int Send(byte[] buffer)
    {
        return socket.Send(buffer);
    }

    public override int Send(byte[] buffer, int offset, int size, SocketFlags socketFlags)
    {
        return socket.Send(buffer, offset, size, socketFlags);
    }

    public override void SetSocketOption(SocketOptionLevel optionLevel, SocketOptionName optionName, int optionValue)
    {
        socket.SetSocketOption(optionLevel, optionName, optionValue);
    }
}
