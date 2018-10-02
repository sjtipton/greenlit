using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System.IO;

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
                            .AddJsonFile($"appsettings.json", optional: false, reloadOnChange: true)
                            //.AddJsonFile($"appsettings.{context.HostingEnvironment.EnvironmentName}.json", optional: false, reloadOnChange: true)
                            .AddCommandLine(args)
                            .AddEnvironmentVariables();
                    })
                .UseKestrel()
                .UseUrls("http://*:5000")
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseStartup<Startup>()
                .Build();
    }
}
