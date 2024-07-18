using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdpChat
{

    internal class Program
    {
        [System.STAThreadAttribute()]
        public static void Main()
        {
            App app = new App();
            app.InitializeComponent();
            app.Run();
        }

        public static IHostBuilder CreateHostBuider(string[] Args)
        {
            var host_builder = Host.CreateDefaultBuilder(Args);

            host_builder.UseContentRoot(App.CurrentDirectory ?? Environment.CurrentDirectory);
            host_builder.ConfigureAppConfiguration((host, cfg) =>
            {
                cfg.SetBasePath(Environment.CurrentDirectory);
                cfg.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            });

            host_builder.ConfigureServices(App.ConfigureServices);

            return host_builder;
        }
    }
}
