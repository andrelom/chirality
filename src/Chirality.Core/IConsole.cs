using System;

namespace Chirality.Core
{
    public interface IConsole : IDisposable
    {
        void WriteLine(string value);
    }
}
