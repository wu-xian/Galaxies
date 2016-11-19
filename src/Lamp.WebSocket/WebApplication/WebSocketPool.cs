using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lamp.WebSocket.WebApplication
{
    public class WebSocketPool
    {
        private List<RoomSocket> roomSockets;

        public WebSocketPool()
        {
            if (roomSockets == null)
            {
                roomSockets = new List<RoomSocket>();
            }
        }
    }
}
