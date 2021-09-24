using Fleck;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Chii.OneBot.SDK.WebSocket
{
    public class WebSocketClient : IDisposable
    {
        private ClientWebSocket clientWebSocket;
        /// <summary>
        /// 消息接收事件
        /// </summary>
        public event AsyncEventHandler<WebSocketClientMessageEventArgs> OnReceiveMessageAsync;
        /// <summary>
        /// 發生異常事件
        /// </summary>
        public event AsyncEventHandler<WebSocketClientErrorEventArgs> OnErrorAsync;

        public WebSocketClient(Uri url, WebSocketMessageType type, string selfId, string AccessToken, CancellationToken cancellationToken)
        {
            Task.Factory.StartNew(async () =>
              {
                  try
                  {
                      WebSocketReceiveResultProcessor resultProcessor = new WebSocketReceiveResultProcessor();
                      clientWebSocket = new ClientWebSocket();
                      clientWebSocket.Options.SetRequestHeader("Authorization", $"Bearer {AccessToken}");
                      await clientWebSocket.ConnectAsync(url, cancellationToken);
                      while (cancellationToken.IsCancellationRequested == false)
                      {
                          while (clientWebSocket.State == WebSocketState.Open || cancellationToken.IsCancellationRequested == false)
                          {
                              ArraySegment<byte> buffer = new ArraySegment<byte>(ArrayPool<byte>.Shared.Rent(512));
                              var result = await clientWebSocket.ReceiveAsync(buffer, cancellationToken);
                              if (result.MessageType == WebSocketMessageType.Close) { break; }
                              var isEndOfMessage = resultProcessor.Receive(result, buffer, out var frame);
                              if (isEndOfMessage)
                              {
                                  if (frame.IsEmpty == true) { break; }
                                  if (result.MessageType == WebSocketMessageType.Text && type == WebSocketMessageType.Text)
                                  {
                                      Func<string, Task> func = (data) =>
                                      {
                                          return clientWebSocket.SendAsync(new ArraySegment<byte>(Encoding.UTF8.GetBytes(data)), result.MessageType, true, cancellationToken);
                                      };
                                      await OnReceiveMessageAsync(selfId, new WebSocketClientMessageEventArgs(frame.ToString(), selfId, func));
                                  }
                                  if (result.MessageType == WebSocketMessageType.Binary && type == WebSocketMessageType.Binary)
                                  {
                                       Func<byte[], Task> func = (data) =>
                                       {
                                           return clientWebSocket.SendAsync(new ArraySegment<byte>(data), result.MessageType, true, cancellationToken);
                                       };
                                      await OnReceiveMessageAsync(selfId, new WebSocketClientMessageEventArgs(frame.ToArray(), selfId, func));
                                  }
                              }
                          }
                          clientWebSocket.Dispose();
                          await Task.Delay(TimeSpan.FromSeconds(5));
                      }
                  }catch (Exception ex)
                  {
                      if (OnErrorAsync == null) { throw; }
                      await OnErrorAsync(selfId, new WebSocketClientErrorEventArgs(ex, selfId));
                  }
              }, cancellationToken, TaskCreationOptions.LongRunning, TaskScheduler.Current);
        }

        ~WebSocketClient()
        {
            Dispose();
        }

        public void Dispose()
        {
            clientWebSocket?.Dispose();
        }
    }
}
