using System;
using System.Threading;

namespace Chirality.Core.Utilities
{
    public static class ProcessUtility
    {
        public static CancellationToken CreateCancellationToken()
        {
            var source = new CancellationTokenSource();

            AppDomain.CurrentDomain.ProcessExit += delegate { source.Cancel(); };

            return source.Token;
        }
    }
}
