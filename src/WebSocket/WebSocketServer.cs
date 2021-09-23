using Fleck;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chii.OneBot.SDK.WebSocket
{

    public class WebSocketServer : IDisposable
    {
        private readonly Fleck.WebSocketServer Server = null;
        /// <summary>
        /// 加強認證核對級別
        /// </summary>
        public bool isHardAuth = false;
        /// <summary>
        /// 連線端目錄
        /// </summary>
        public ConcurrentDictionary<string, ConnectionData> ConnectionBinding = new ConcurrentDictionary<string, ConnectionData>();
        /// <summary>
        /// 認證事件
        /// </summary>
        public event AsyncEventHandler<AuthorizationEventArgs> OnAuthorizationAsync;
        /// <summary>
        /// 開放連接事件
        /// </summary>
        public event AsyncEventHandler<ConnectionEventArgs> OnOpenConnectionAsync;
        /// <summary>
        /// 關閉連接事件
        /// </summary>
        public event AsyncEventHandler<ConnectionEventArgs> OnCloseConnectionAsync;
        /// <summary>
        /// 消息接收事件
        /// </summary>
        public event AsyncEventHandler<MessageEventArgs> OnReceiveMessageAsync;
        /// <summary>
        /// 接觸事件
        /// </summary>
        public event AsyncEventHandler<WSBaseEventArgs> OnPongAsync;
        /// <summary>
        /// 發生異常事件
        /// </summary>
        public event AsyncEventHandler<ErrorEventArgs> OnErrorAsync;

        public WebSocketServer(string location)
        {
            Server = new Fleck.WebSocketServer(location);
            Server.Start(socket =>
            {
                socket.OnPong = async (rcnb) =>
                {
                    if (OnPongAsync == null) { return; }
                    if (socket.ConnectionInfo.Headers.TryGetValue("X-Self-ID", out string selfId) == false) { return; }
                    if (ConnectionBinding.TryGetValue(selfId, out ConnectionData data))
                    {
                        if (isHardAuth) if (data.RoleAndConnections.Any(a => a.Value.Id == socket.ConnectionInfo.Id) == false) { return; }
                        var connection = new Connection { Send = socket.Send, WebSocketConnectionInfo = socket.ConnectionInfo };
                        await OnPongAsync(selfId, new WSBaseEventArgs() { Connection = connection });
                    }
                };
                socket.OnOpen = async () =>
                {
                    if (socket.ConnectionInfo.Headers.TryGetValue("X-Self-ID", out string selfId) == false) { return; }
                    if (socket.ConnectionInfo.Headers.TryGetValue("X-Client-Role", out string type) == false) { return; }
                /// OneBot 12
                //    if (socket.ConnectionInfo.Headers.TryGetValue("Upgrade", out string _u) == false || _u == "websocket") { return; }
                //    if (socket.ConnectionInfo.Headers.TryGetValue("Connection", out string _c) == false || _c == "Upgrade") { return; }
                    var connection = new Connection { Send = socket.Send, SendMessagePack = socket.Send, WebSocketConnectionInfo = socket.ConnectionInfo };
                    if (socket.ConnectionInfo.Headers.TryGetValue("Authorization", out string auth) == true)
                    {
                        var ea = new AuthorizationEventArgs(selfId, auth, type, connection);
                        if (OnAuthorizationAsync != null) { await OnAuthorizationAsync(selfId, ea); }
                        if (ea.Pass == false && OnAuthorizationAsync != null) { return; }
                    }
                  //  await socket.SendPing(new byte[] { 255 });
                    var cinfo = new ConnectionData(selfId, type, connection);
                    ConnectionBinding.AddOrUpdate(selfId, cinfo, (r, c) =>
                    {
                        c.RoleAndConnections.AddOrUpdate(type, connection, (n, b) =>
                        {
                            b.Send = connection.Send;
                            b.WebSocketConnectionInfo = connection.WebSocketConnectionInfo;
                            return b;
                        });
                        return c;
                    });
                    if (OnOpenConnectionAsync == null) { return; }
                    await OnOpenConnectionAsync(selfId, new ConnectionEventArgs(type, connection));
                };
                socket.OnClose = async () =>
                {
                    if (socket.ConnectionInfo.Headers.TryGetValue("X-Self-ID", out string selfId) == false) { return; }
                    if (socket.ConnectionInfo.Headers.TryGetValue("X-Client-Role", out string type) == false) { return; }
                    foreach (var cb in ConnectionBinding)
                    {
                        if (cb.Value.RoleAndConnections.Any(f => f.Value.Id == socket.ConnectionInfo.Id))
                        {
                            cb.Value.RoleAndConnections.TryRemove(type, out Connection dump);
                            if (OnCloseConnectionAsync == null) { return; }
                            await OnCloseConnectionAsync(selfId, new ConnectionEventArgs(type, dump));
                        }
                    }
                };
                socket.OnBinary = async (messagePack) =>
                {
                    if (OnReceiveMessageAsync == null) { return; }
                    if (socket.ConnectionInfo.Headers.TryGetValue("X-Self-ID", out string selfId) == false) { return; }
                    if (socket.ConnectionInfo.Headers.TryGetValue("X-Client-Role", out string type) == false) { return; }
                    var connection = new Connection { SendMessagePack = socket.Send, WebSocketConnectionInfo = socket.ConnectionInfo };
                    if (ConnectionBinding.TryGetValue(selfId, out ConnectionData data))
                    {
                        if (isHardAuth) if (data.RoleAndConnections.Any(a => a.Value.Id == socket.ConnectionInfo.Id) == false) { return; }
                        try
                        {
                            await OnReceiveMessageAsync(selfId, new MessageEventArgs(messagePack, type, data, connection, socket.Send));
                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine(ex);
                            if (OnErrorAsync == null) { return; }
                            await OnErrorAsync(selfId, new ErrorEventArgs(ex, connection));
                        }
                    }
                };
                socket.OnMessage = async (message) =>
                {
                    if (OnReceiveMessageAsync == null) { return; }
                    if (socket.ConnectionInfo.Headers.TryGetValue("X-Self-ID", out string selfId) == false) { return; }
                    if (socket.ConnectionInfo.Headers.TryGetValue("X-Client-Role", out string type) == false) { return; }
                    var connection = new Connection { Send = socket.Send, WebSocketConnectionInfo = socket.ConnectionInfo };
                    if (ConnectionBinding.TryGetValue(selfId, out ConnectionData data))
                    {
                        if (isHardAuth) if (data.RoleAndConnections.Any(a => a.Value.Id == socket.ConnectionInfo.Id) == false) { return; }
                        try
                        {
                            await OnReceiveMessageAsync(selfId, new MessageEventArgs(message, type, data, connection, socket.Send));
                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine(ex);
                            if (OnErrorAsync == null) { return; }
                            await OnErrorAsync(selfId, new ErrorEventArgs(ex, connection));
                        }
                    }
                };
            });
        }
        ~WebSocketServer()
        {
            Dispose();
        }
        public void Dispose()
        {
            Server.Dispose();
            ConnectionBinding.Clear();
        }
    }
}