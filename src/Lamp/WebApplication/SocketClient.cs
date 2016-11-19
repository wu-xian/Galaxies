using Lamp.WebApplication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;

namespace Lamp.WebSocket.WebApplication
{
    public class SocketClient
    {
        public Guid UserId { set; get; }
        public System.Net.WebSockets.WebSocket WebSocket { set; get; }
        public string DisplayName { set; get; }
        public string UserName { set; get; }
        public Action<SocketMessage> BroadcastAction { set; get; }
        public async Task<SocketMessage> RecvMsg()
        {
            byte[] bytes = new byte[2048];
            var result = await WebSocket.ReceiveAsync(new ArraySegment<byte>(bytes), CancellationToken.None);
            return new SocketMessage()
            {
                IsEndMessage = result.EndOfMessage,
                Count = result.Count,
                MessageType = result.MessageType,
                Message = bytes
            };
        }

        public string SendMsg(SocketMessage msg)
        {
            BroadcastAction(msg);
            return null;
        }

        public async Task Looper()
        {
            while (!WebSocket.CloseStatus.HasValue)
            {
                var result = await RecvMsg();
                SendMsg(MessagePipeline.Factory(result, DisplayName));
            }
            await WebSocket.CloseAsync(WebSocketCloseStatus.Empty, "连接关闭", CancellationToken.None);
        }

        
    }
}
