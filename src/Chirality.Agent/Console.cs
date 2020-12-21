using System.IO;
using Chirality.Core;

namespace Chirality.Agent
{
    public class Console : IConsole
    {
        private readonly StreamReader _reader;

        private readonly StreamWriter _writer;

        public Console()
        {
            _reader = new StreamReader(System.Console.OpenStandardInput());
            _writer = new StreamWriter(System.Console.OpenStandardOutput());
        }

        public void Dispose()
        {
            _reader.Dispose();
            _writer.Dispose();
        }

        public void WriteLine(string value)
        {
            _writer.WriteLine(value);
            _writer.Flush();
        }
    }
}
