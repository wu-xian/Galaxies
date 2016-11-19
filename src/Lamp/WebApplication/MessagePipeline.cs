using Lamp.WebApplication;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Lamp.WebSocket.WebApplication
{
    public static class MessagePipeline
    {
        public static SocketMessage Factory(SocketMessage message,string displayName)
        {
            string userMessage = System.Text.Encoding.UTF8.GetString(message.Message);
            string title = displayName;
            string prefix = $"[{title}]:";
            string resultMsg = $"{prefix}{userMessage}";
            return new SocketMessage()
            {
                Count = System.Text.Encoding.UTF8.GetByteCount(resultMsg),
                Message = System.Text.Encoding.UTF8.GetBytes(resultMsg),
                IsEndMessage = message.IsEndMessage,
                MessageType = message.MessageType
            };
        }
    }
}
