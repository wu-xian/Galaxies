using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Threading.Tasks;

namespace Lamp.WebApplication
{
    public struct SocketMessage
    {
        public bool IsEndMessage { set; get; }
        public int Count { set; get; }
        public WebSocketMessageType MessageType { set; get; }
        public byte[] Message { set; get; }
    }
}
