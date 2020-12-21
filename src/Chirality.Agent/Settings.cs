using Chirality.Core;

namespace Chirality.Agent
{
    public class Settings : ISettings
    {
        public Settings()
        {
            Host = "::1";
            Port = 1984;
        }

        public string Host { get; set; }

        public int Port { get; set; }
    }
}
