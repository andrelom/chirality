using System;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Chirality.Core;
using Chirality.Core.Extensions;
using Chirality.P2P.Extensions;
using Chirality.P2P.Sockets;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Chirality.P2P
{
    public class Server : IHostedService
    {
        private readonly ILogger<Server> _logger;

        private readonly IHostApplicationLifetime _lifetime;

        private readonly ISettings _settings;

        private readonly AsyncTcpListener _listener;

        public Server(ILogger<Server> logger,
            IHostApplicationLifetime lifetime,
            ISettings settings)
        {
            _logger = logger;
            _lifetime = lifetime;
            _settings = settings;
            _listener = new AsyncTcpListener(_settings.Host, _settings.Port);
        }

        public async Task StartAsync(CancellationToken token)
        {
            _logger.LogInformation(string.Format(Constants.Messages.ListeningOn, _settings.Host, _settings.Port));

            try
            {
                await _listener.StartAcceptAsync(HandleClientAsync, token);
            }
            catch (Exception ex)
            {
                _lifetime.StopApplication(ex, _logger);
            }
        }

        public async Task StopAsync(CancellationToken token)
        {
            try
            {
                await _listener.StopAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
            }
        }

        #region Private Methods

        private async Task HandleClientAsync(TcpClient client, CancellationToken token)
        {
            // TODO: Implement the handler.

            await Task.Yield();
        }

        #endregion
    }
}
