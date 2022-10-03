using System.Reflection;
using BlockchainTestProject.Application;
using BlockchainTestProject.Output;
using BlockchainTestProject.Persistence;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BlockchainTestProject
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var configuration = BuildConfiguration();
            var serviceProvider = BuildServiceProvider(configuration);
            var app = serviceProvider.GetRequiredService<BlockchainApplication>();
            await app.RunAsync(args);
        }

        private static void ConfigureServices(IServiceCollection services, IConfigurationRoot configuration)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddSingleton<BlockchainApplication>();
            services.AddSingleton<WalletRepository>();
            services.AddSingleton<IConsoleWriter, ConsoleWriter>();
        }
        
        private static ServiceProvider BuildServiceProvider(IConfigurationRoot configuration)
        {
            var services = new ServiceCollection();
            ConfigureServices(services, configuration);
            var serviceProvider = services.BuildServiceProvider();
            return serviceProvider;
        }

        private static IConfigurationRoot BuildConfiguration()
        {
            var configuration = new ConfigurationBuilder()
                .AddEnvironmentVariables("CLI_")
                .Build();
            return configuration;
        }
    }
}
