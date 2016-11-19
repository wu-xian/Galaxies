using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;

namespace Lamp.WebSocket
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
               //.UseUrls(new string[] { "http://10.202.101.45:8088" })
                //.UseUrls(new string[] { " http://192.168.0.88:8088/home/index" })
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }
}
