using System;
using System.IO;
using System.Text.Json;
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
            var settings = GetSettings();

            // Shared
            services.AddSingleton<ISettings>(settings);

            // Hosted Services
            services.AddHostedService<Server>();
        }

        private static Settings GetSettings()
        {
            var path = ProcessUtility.GetExecutionPath();
            var file = Path.Join(path, Constants.SettingsFileName);

            if (File.Exists(file))
            {
                return JsonSerializer.Deserialize<Settings>(File.ReadAllText(file));
            }

            var settings = new Settings();
            var options = new JsonSerializerOptions { WriteIndented = true };

            File.WriteAllText(file, JsonSerializer.Serialize(settings, options));

            return settings;
        }

        #endregion
    }
}
