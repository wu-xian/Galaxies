using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
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
            private System.Net.WebSockets.WebSocket socket;
            RoomSocket roomSocket;


            public SocketPool(RequestDelegate dele)
            {
                next = dele;
                roomSocket = new RoomSocket("first");
            }

            public async Task Invoke(HttpContext context)
            {
                var currentSocket = await context.WebSockets.AcceptWebSocketAsync();
                roomSocket.AddUser(Thread.CurrentThread.ManagedThreadId.ToString(), currentSocket);
                await roomSocket.Receive(currentSocket);
            }


            private async Task Echo(System.Net.WebSockets.WebSocket webSocket,string aa)
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
