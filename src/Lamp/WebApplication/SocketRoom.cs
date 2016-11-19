using Lamp.WebApplication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;

namespace Lamp.WebSocket.WebApplication
{
    public class SocketRoom
    {
        public int RoomId { set; get; }
        public string RoomName { set; get; }
        private List<SocketClient> clients;

        public SocketRoom()
        {
            clients = new List<SocketClient>();
        }

        public int CurrentLinkCount
        {
            get
            {
                return clients.Count;
            }
        }

        public async Task RunClient(SocketClient _client)
        {
            clients.Add(_client);
            _client.BroadcastAction = msg => Broadcast(msg);
            await _client.Looper();
            clients.Remove(_client);
        }


        public void Drop()
        {
        }

        public void Broadcast(SocketMessage rawMsg)
        {
            if (clients.Count == 0)
            {
                throw new ArgumentNullException(nameof(clients), "发送对象为空");
            }
            foreach (var item in clients)
            {
                item.WebSocket.SendAsync(new ArraySegment<byte>(rawMsg.Message, 0, rawMsg.Count), rawMsg.MessageType, rawMsg.IsEndMessage, CancellationToken.None);
            }
        }
    }
}
