using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;

namespace Lamp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                //.UseUrls(new string[] {"http://192.168.0.88:88" })
                //.UseUrls(new string[] { "http://10.202.101.45:88" })
                .UseUrls(new string[] { "http://0.0.0.0:9001" })
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }
}
