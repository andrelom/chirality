using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Chirality.P2P.Extensions;

namespace Chirality.P2P.Sockets
{
    public delegate Task AsyncTcpClientHandler(TcpClient client, CancellationToken token);

    public class AsyncTcpListener
    {
        private readonly IPAddress _host;

        private readonly int _port;

        private TcpListener _listener;

        private ConcurrentDictionary<Guid, TcpClient> _clients;

        private ConcurrentDictionary<Guid, Task> _handlers;

        public AsyncTcpListener(IPAddress host, int port)
        {
            _host = host;
            _port = port;
        }

        public AsyncTcpListener(string host, int port)
        {
            _host = IPAddress.Parse(host);
            _port = port;
        }

        public async Task StartAcceptAsync(AsyncTcpClientHandler handler, CancellationToken token)
        {
            if (_listener != null)
            {
                throw new InvalidOperationException("The listener is already started");
            }

            // Initialize all the listener resources.
            _listener = new TcpListener(_host, _port);
            _clients = new ConcurrentDictionary<Guid, TcpClient>();
            _handlers = new ConcurrentDictionary<Guid, Task>();

            // Accepts IPv4 and IPv6.
            _listener.Server.DualMode = true;

            // Start listening for incoming connections.
            _listener.Start();

            // Register to stop any waiting listener.
            using (token.Register(async delegate { await StopAsync(); }))
            {
                await AcceptAsync(handler, token);
            }
        }

        public async Task StopAsync()
        {
            if (_listener == null)
            {
                return;
            }

            // Stop listening for incoming connections.
            _listener.Stop();

            // Disposes all active connections.
            _clients.Values.ToList().ForEach((client) => client.Dispose());

            // Wait for all tasks to complete.
            await Task.WhenAll(_handlers.Values);

            // Clear the dictionaries.
            _clients.Clear();
            _handlers.Clear();

            // Clear the base listener resources.
            _listener = null;
            _clients = null;
            _handlers = null;
        }

        #region Private Methods

        private async Task AcceptAsync(AsyncTcpClientHandler handler, CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                TcpClient client;

                try
                {
                    client = await _listener.AcceptTcpClientAsync(token);
                }
                catch (OperationCanceledException)
                {
                    break;
                }

                var guid = Guid.NewGuid();

                // Keep track of the connected clients.
                _clients.TryAdd(guid, client);

                // Queues the client connection handler to run on the thread pool.
                _handlers.TryAdd(guid, Handle(guid, client, handler, token));
            }
        }

        private Task Handle(Guid guid, TcpClient client, AsyncTcpClientHandler handler, CancellationToken token)
        {
            return Task.Run(async delegate
            {
                using (client)
                {
                    try
                    {
                        await handler(client, token);
                    }
                    finally
                    {
                        _clients.TryRemove(guid, out _);
                        _handlers.TryRemove(guid, out _);
                    }
                }
            }, token);
        }

        #endregion
    }
}
