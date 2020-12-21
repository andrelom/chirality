using System;
using System.Diagnostics;
using System.IO;
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

        public static string GetExecutionPath()
        {
            if (Debugger.IsAttached)
            {
                return Path.GetFullPath(".");
            }

            var process = Process.GetCurrentProcess();

            return Path.GetDirectoryName(process.MainModule.FileName);
        }
    }
}
