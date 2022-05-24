using Whomever.Data;

namespace Whomever
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            if (args.Length > 0 && args[0].ToLower() == "/seed")
            {
                SeedDatabase(host);
            }
            else
            {
                host.Run();
            }
        }

        private static void SeedDatabase(IHost host)
        {
            var scopeFactory = host.Services.GetService<IServiceScopeFactory>();
            using var scope = scopeFactory.CreateScope();
            var appSeeder = scope.ServiceProvider.GetService<ApplicationSeeder>();
            appSeeder.Seed();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}