using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Chirality.Core;
using Chirality.Core.Utilities;
using Chirality.P2P;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Chirality.Agent
{
    internal static class Program
    {
        public static async Task Main()
        {
            var token = ProcessUtility.CreateCancellationToken();
            var host = new HostBuilder()
                .ConfigureLogging(ConfigureLogging)
                .ConfigureServices(ConfigureServices)
                .Build();

            try
            {
                await host.RunAsync(token);
            }
            catch (Exception)
            {
                System.Console.WriteLine(Constants.Messages.ShutdownDueUnexpectedError);
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
            services.AddSingleton<IConsole, Console>();

            // Hosted Services
            services.AddHostedService<Client>();
            services.AddHostedService<Server>();
        }

        private static void ConfigureLogging(HostBuilderContext context, ILoggingBuilder logging)
        {
            var path = ProcessUtility.GetExecutionPath();
            var file = Path.Join(path, Constants.LogFileName);

            logging.ClearProviders();

            logging.AddFile(file, LogLevel.Information, new Dictionary<string, LogLevel>
            {
                { "System", LogLevel.None },
                { "Microsoft", LogLevel.None }
            });
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
