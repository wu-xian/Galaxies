using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.WebSockets.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;

namespace Lamp.WebSocket.WebApplication
{
    public class RoomSocket
    {
        public string Name { set; get; }
        public List<string> Users { set; get; }
        public List<System.Net.WebSockets.WebSocket> Sockets { set; get; }

        public RoomSocket(string _name)
        {
            Name = _name;
            Users = new List<string>();
            Sockets = new List<System.Net.WebSockets.WebSocket>();
        }

        public void AddUser(string userName, System.Net.WebSockets.WebSocket _socket)
        {
            Users.Add(userName);
            Sockets.Add(_socket);
        }
        public async void Send(SocketMessage message)
        {
            if (Sockets.Count == 0)
            {
                throw new ArgumentNullException(nameof(RoomSocket.Send), "发送对象为空");
            }
            foreach (var item in Sockets)
            {
                await item.SendAsync(new ArraySegment<byte>(message.Message, 0, message.Count), message.MessageType, message.IsEndMessage, CancellationToken.None);
            }
        }
        public async Task Receive(System.Net.WebSockets.WebSocket _socket)
        {
            while (!_socket.CloseStatus.HasValue)
            {
                var result = await GetSingleUserMessage(_socket);
                Send(MessagePipeline.Factory(result));
            }
        }
        private async Task<SocketMessage> GetSingleUserMessage(System.Net.WebSockets.WebSocket _socket)
        {
            byte[] bytes = new byte[2048];
            var result = await _socket.ReceiveAsync(new ArraySegment<byte>(bytes), CancellationToken.None);
            return new SocketMessage()
            {
                IsEndMessage=result.EndOfMessage,
                Count=result.Count,
                MessageType=result.MessageType,
                Message = bytes
            };
        }
    }

    public struct SocketMessage
    {
        public bool IsEndMessage { set; get; }
        public int Count { set; get; }
        public WebSocketMessageType MessageType { set; get; }
        public byte[] Message { set; get; }
    }
}
