﻿using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Net;

namespace greenlit
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        // TODO enhance if necessary
        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((WebHostBuilderContext context, IConfigurationBuilder builder) =>
                    {
                        builder.Sources.Clear();

                        builder.SetBasePath(Directory.GetCurrentDirectory())
                            .AddEnvironmentVariables(prefix: "ASPNETCORE_")
                            .AddJsonFile(GetAppSettingsFile(context.HostingEnvironment), optional: false, reloadOnChange: true)
                            .AddCommandLine(args)
                            .AddEnvironmentVariables();
                    })
                .UseKestrel((context, options) => {
                    if (context.HostingEnvironment.IsDevelopment())
                    {
                        options.Listen(IPAddress.Loopback, int.Parse(context.Configuration["Port"]));
                    }
                })
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseStartup<Startup>()
                .Build();

        private static string GetAppSettingsFile(IHostingEnvironment env)
        {
            return !env.IsDevelopment() ? "appsettings.json" : "appsettings.Development.json";
        }
    }
}
