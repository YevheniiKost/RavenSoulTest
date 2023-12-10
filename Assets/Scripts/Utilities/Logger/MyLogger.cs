using System.Collections.Generic;

namespace RavenSoul.Utilities.Logger
{
    public static class MyLogger
    {
        private static readonly List<ILogger> Loggers = new List<ILogger>();
        
        static MyLogger()
        {
            Loggers.Add(new UnityLogger());
        }
        
        public static void Log(string message)
        {
            foreach (var logger in Loggers)
            {
                logger.Log(message);
            }
        }
        
        public static void LogWarning(string message)
        {
            foreach (var logger in Loggers)
            {
                logger.LogWarning(message);
            }
        }
        
        public static void LogError(string message)
        {
            foreach (var logger in Loggers)
            {
                logger.LogError(message);
            }
        }
    }
}