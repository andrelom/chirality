using System;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Chirality.Core.Extensions
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
