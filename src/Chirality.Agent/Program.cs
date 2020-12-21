using System;
using System.Threading.Tasks;
using Chirality.Core;
using Chirality.Core.Utilities;
using Chirality.P2P;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Chirality.Agent
{
    internal static class Program
    {
        public static async Task Main()
        {
            var token = ProcessUtility.CreateCancellationToken();
            var host = new HostBuilder()
                .ConfigureServices(ConfigureServices)
                .Build();

            try
            {
                await host.RunAsync(token);
            }
            catch (Exception)
            {
                Console.WriteLine(Messages.ShutdownDueUnexpectedError);
            }
            finally
            {
                await Task.Delay(250, token);
            }
        }

        #region Private Methods

        private static void ConfigureServices(HostBuilderContext context, IServiceCollection services)
        {
            // Hosted Services
            services.AddHostedService<Server>();
        }

        #endregion
    }
}
