namespace Chirality.Core
{
    public static class Constants
    {
        public const string Banner = " ██████╗██╗  ██╗██╗██████╗  █████╗ ██╗     \n██╔════╝██║  ██║██║██╔══██╗██╔══██╗██║     \n██║     ███████║██║██████╔╝███████║██║     \n██║     ██╔══██║██║██╔══██╗██╔══██║██║     \n╚██████╗██║  ██║██║██║  ██║██║  ██║███████╗\n ╚═════╝╚═╝  ╚═╝╚═╝╚═╝  ╚═╝╚═╝  ╚═╝╚══════╝";

        public const string Version = "v1.0.0";

        public const string SettingsFileName = "settings.json";

        public const string LogFileName = "{Date}.log";

        public static class Messages
        {
            public const string ShutdownDueUnexpectedError = "Shutdown due to unexpected error";

            public const string ListeningOn = "Server is listening on {0}:{1}";

            public const string PressCtrlC = "Press Ctrl+C to shut down";

            public const string ClientShuttingDown = "Client is shutting down...";
        }
    }
}
