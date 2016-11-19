using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;

namespace Lamp.WebApplication
{
    public class SocketClient
    {
        public Guid UserId { set; get; }
        public System.Net.WebSockets.WebSocket WebSocket { set; get; }
        public string DisplayName { set; get; }
        public string UserName { set; get; }
        public Action<SocketMessage> BroadcastAction { set; get; }
        public SocketMessage RecvMsg()
        {
            byte[] bytes = new byte[2048];
            var result = WebSocket.ReceiveAsync(new ArraySegment<byte>(bytes), CancellationToken.None);
            return new SocketMessage()
            {
                IsEndMessage = result.Result.EndOfMessage,
                Count = result.Result.Count,
                MessageType = result.Result.MessageType,
                Message = bytes
            };
        }

        public void SendMsg(SocketMessage msg)
        {
            BroadcastAction(msg);
        }

        public void Looper()
        {
            while (!WebSocket.CloseStatus.HasValue)
            {
                var result = RecvMsg();
                SendMsg(MessagePipeline.Factory(result));
            }
            WebSocket.CloseAsync(WebSocketCloseStatus.Empty, "连接关闭", CancellationToken.None);
        }

        
    }
}
