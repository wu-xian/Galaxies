using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Threading.Tasks;

namespace WebSocketTest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("starting .....");
            SocketSend();
            Console.ReadLine();
        }

        public static async void SocketSend()
        {
            ClientWebSocket socket = new ClientWebSocket();
            string message = "hello websocket";
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(message);
            await socket.ConnectAsync(new Uri("ws://10.202.101.45:8088"), System.Threading.CancellationToken.None);
            Console.WriteLine($"send:{bytes.ToString()}");
            await socket.SendAsync(new ArraySegment<byte>(bytes), WebSocketMessageType.Text, true, System.Threading.CancellationToken.None);
        }
    }
}
