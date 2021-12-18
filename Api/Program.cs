using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Shared.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                   .UseStartup<Startup>()
                   .UseLogging("Api-Gateway")
                   .ConfigureAppConfiguration((hostingContext, config) =>
                   {
                       config.SetBasePath(hostingContext.HostingEnvironment.ContentRootPath)
                            .AddJsonFile("ocelot.json")
                            .AddEnvironmentVariables();
                   });
    }
}
