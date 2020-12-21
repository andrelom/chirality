namespace Chirality.Core
{
    public static class Constants
    {
        public const string SettingsFileName = "settings.json";

        public const string LogFileName = "{Date}.log";

        public static class Messages
        {
            public const string ShutdownDueUnexpectedError = "Shutdown due to unexpected error";

            public const string ListeningOn = "Server is listening on {0}:{1}";
        }
    }
}
