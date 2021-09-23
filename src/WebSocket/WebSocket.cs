using Fleck;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Chii.OneBot.SDK.WebSocket
{
    public delegate ValueTask AsyncEventHandler<TEventArgs>(object sender, TEventArgs e) where TEventArgs : EventArgs;
    public class WSBaseEventArgs : EventArgs
    {
        public Connection Connection { get; set; }
    }
    public class WebSocketClientBaseEventArgs : EventArgs
    {
        public string SelfId { get; set; }
    }

    public class PongEventArgs : WSBaseEventArgs
    {
        public byte[] Echo { get; set; }
        public PongEventArgs(byte[] echo, Connection connection)
        {
            Echo = echo;
            base.Connection = connection;
        }
    }
    public class ErrorEventArgs : WSBaseEventArgs
    {
        public Exception Exception { get; set; }
        public ErrorEventArgs(Exception ex, Connection connection)
        {
            Exception = ex;
            base.Connection = connection;
        }
    }
    public class WebSocketClientErrorEventArgs : WebSocketClientBaseEventArgs
    {
        public Exception Exception { get; set; }
        public WebSocketClientErrorEventArgs(Exception ex, string selfId)
        {
            Exception = ex;
            base.SelfId = selfId;
        }
    }
    public sealed class AuthorizationEventArgs : WSBaseEventArgs
    {
        public string SelfId { get; private set; }
        public string AuthCode { get; private set; }
        public string Role { get; private set; }
        public bool Pass { get; private set; } = false;
        /// <summary>
        /// 設置通過連接
        /// </summary>
        public void Allow() { this.Pass = true; }
        public AuthorizationEventArgs(string selfId, string AuthCode, string Role, Connection connection)
        {
            this.SelfId = selfId;
            this.AuthCode = AuthCode;
            this.Role = Role;
            base.Connection = connection;
        }
    }
    public sealed class ConnectionEventArgs : WSBaseEventArgs
    {
        public string Role { get; private set; }
        public ConnectionEventArgs(string Role, Connection connection)
        {
            this.Role = Role;
            base.Connection = connection;
        }
    }
    public sealed class WebSocketClientMessageEventArgs : WebSocketClientBaseEventArgs
    {
        public string Message { get; private set; }
        public byte[] MessagePack { get; private set; }
        public Func<string, Task> Send { get; private set; }
        public Func<byte[], Task> SendMessagePack { get; private set; }
        public WebSocketClientMessageEventArgs(string Message, string SelfId, Func<string, Task> send)
        {
            this.Message = Message;
            this.Send = send;
            base.SelfId = SelfId;
        }
        public WebSocketClientMessageEventArgs(byte[] MessagePack, string SelfId, Func<byte[], Task> SendMessagePack)
        {
            this.MessagePack = MessagePack;
            this.SendMessagePack = SendMessagePack;
            base.SelfId = SelfId;
        }
    }
    public sealed class MessageEventArgs : WSBaseEventArgs
    {
        public string Message { get; private set; }
        public byte[] MessagePack { get; private set; }
        public string Role { get; private set; }
        public ConnectionData ConnectionData { get; private set; }
        public MessageEventArgs(string Message, string Role, ConnectionData ConnectionData, Connection connection, Func<string, Task> send)
        {
            this.Message = Message;
            this.Role = Role;
            this.ConnectionData = ConnectionData;
            base.Connection = connection;
        }
        public MessageEventArgs(byte[] MessagePack, string Role, ConnectionData ConnectionData, Connection connection, Func<byte[], Task> send)
        {
            this.MessagePack = MessagePack;
            this.Role = Role;
            this.ConnectionData = ConnectionData;
            base.Connection = connection;
        }
    }
    public sealed class Connection
    {
        public Guid Id => WebSocketConnectionInfo.Id;
        public IWebSocketConnectionInfo WebSocketConnectionInfo { get; set; }
        public Func<string, Task> Send { get; set; }
        public Func<byte[], Task> SendMessagePack { get; set; }
    }
    public sealed class ConnectionData
    {
        public string SelfId { get; private set; }
        public ConcurrentDictionary<string, Connection> RoleAndConnections = new ConcurrentDictionary<string, Connection>();
        public DateTime CreateDateTime { get; private set; }
        public ConnectionData(string selfId, string role, Connection connection)
        {
            RoleAndConnections.TryAdd(role, connection);
            this.CreateDateTime = DateTime.Now;
            this.SelfId = selfId;
        }
    }
}
