using System;
using Microsoft.Extensions.Logging;

namespace Chirality.Core.Extensions
{
    public static class LoggerExtensions
    {
        public static void LogError(this ILogger logger, Exception ex)
        {
            logger.LogError(ex.Message);
        }
    }
}
