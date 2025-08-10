namespace PluginBase
{
    public static class Logger
    {
        public static string LogName { get; set; } = "PluginLogger";
        public static void Log(string plugin , string message)
        {
            File.AppendAllText($"{LogName}.log", $"{plugin}:{DateTime.Now}: {message}\n");
        }
    }
}
