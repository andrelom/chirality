using System;
using Chirality.Core.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Chirality.P2P.Extensions
{
    public static class HostApplicationLifetimeExtensions
    {
        public static void StopApplication<T>(this IHostApplicationLifetime lifetime, Exception ex, ILogger<T> logger)
        {
            logger.LogError(ex);

            lifetime.StopApplication();
        }
    }
}
