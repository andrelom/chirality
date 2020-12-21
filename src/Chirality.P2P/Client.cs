using System.Threading;
using System.Threading.Tasks;
using Chirality.Core;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Chirality.P2P
{
    public class Client : IHostedService
    {
        private readonly ILogger<Client> _logger;

        private readonly ISettings _settings;

        private readonly IConsole _console;

        public Client(ILogger<Client> logger,
            ISettings settings,
            IConsole console)
        {
            _logger = logger;
            _settings = settings;
            _console = console;
        }

        public async Task StartAsync(CancellationToken token)
        {
            _console.WriteLine(GetHeaderText());

            await Task.Yield();
        }

        public async Task StopAsync(CancellationToken token)
        {
            _logger.LogInformation(Constants.Messages.ClientShuttingDown);

            await Task.Yield();
        }

        #region Private Methods

        private string GetHeaderText()
        {
            return string.Join('\n', new string[]
            {
                $"{Constants.Banner}",
                $"{Constants.Version}",
                $"{string.Format(Constants.Messages.ListeningOn, _settings.Host, _settings.Port)}",
                $"{Constants.Messages.PressCtrlC}"
            });
        }

        #endregion
    }
}
