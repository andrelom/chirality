using System;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace Chirality.P2P.Extensions
{
    public static class TcpListenerExtensions
    {
        public static async Task<TcpClient> AcceptTcpClientAsync(this TcpListener listener, CancellationToken token)
        {
            try
            {
                return await listener.AcceptTcpClientAsync();
            }
            catch (SocketException ex) when (ex.SocketErrorCode == SocketError.Interrupted)
            {
                throw new OperationCanceledException();
            }
            catch (ObjectDisposedException) when (token.IsCancellationRequested)
            {
                throw new OperationCanceledException();
            }
        }
    }
}
