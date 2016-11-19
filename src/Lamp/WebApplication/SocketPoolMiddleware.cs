using Galaxies.Core.Services;
using Galaxies.Model.EntityModel;
using Lamp.BIZ;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Model.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Lamp.WebSocket.WebApplication
{
    public static class SocketPoolMiddleware
    {
        public static void UseWebSocketPool(this IApplicationBuilder app)
        {
            app.UseMiddleware<SocketPool>();
        }

        public class SocketPool
        {
            private readonly RequestDelegate next;
            SessionService sessionService;
            private System.Net.WebSockets.WebSocket socket;
            List<SocketRoom> rooms;
            RoomBIZ roomBIZ;


            public SocketPool(RequestDelegate dele
                , SessionService _sessionService
                , RoomBIZ _roomBIZ)
            {
                next = dele;
                rooms = new List<SocketRoom>();
                sessionService = _sessionService;
                roomBIZ = _roomBIZ;
            }

            public async Task Invoke(HttpContext context)
            {
                var resultUser = SessionService.GetObj2<User>(context.Session, ContextService.USER);
                if (resultUser != null)
                {
                    await CreateConnection(context, resultUser);
                }
            }
            private int GetRoomId(string path)
            {
                string roomIdStr = path.Substring(1);
                int roomId;
                if (int.TryParse(roomIdStr, out roomId))
                {
                    return roomId;
                }
                return -1;
            }

            private Room GetRoom(int roomId)
            {
                return roomBIZ.GetRoomById(roomId);
            }

            private async Task CreateConnection(HttpContext context, User user)
            {
                int roomId;
                if ((roomId = GetRoomId(context.Request.Path)) == -1)
                {
                    return;
                }

                SocketRoom socketRoom = rooms.FirstOrDefault(d => d.RoomId == roomId);
                if (socketRoom == null)
                {
                    Room room = GetRoom(roomId);
                    if (room == null)
                    {
                        return;
                    }
                    socketRoom = new SocketRoom()
                    {
                        RoomId = room.Id,
                        RoomName = room.Name
                    };
                    rooms.Add(socketRoom);
                }
                SocketClient client = new SocketClient()
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                    DisplayName = user.UserName
                };
                client.WebSocket = await context.WebSockets.AcceptWebSocketAsync();

                await socketRoom.RunClient(client);
            }
            private async Task Echo(System.Net.WebSockets.WebSocket webSocket, string aa)
            {
                byte[] buffer = new byte[1024 * 4];
                byte[] strings = System.Text.Encoding.UTF8.GetBytes(aa);
                var result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                while (!result.CloseStatus.HasValue)
                {
                    await webSocket.SendAsync(new ArraySegment<byte>(buffer, 0, buffer.Length), result.MessageType, result.EndOfMessage, CancellationToken.None);
                    result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                }
                Console.WriteLine(Guid.NewGuid().ToString() + "/" + DateTime.Now.ToString());
                await webSocket.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, CancellationToken.None);
            }
        }
    }
}
