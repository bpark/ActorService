using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace ActorService
{
    public class Program
    {
        public static void Mainn(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}