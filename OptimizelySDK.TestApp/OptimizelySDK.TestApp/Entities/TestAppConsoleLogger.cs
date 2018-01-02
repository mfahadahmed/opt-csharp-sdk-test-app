using OptimizelySDK.Logger;
using System;
using System.Collections.Generic;
using System.IO;

namespace OptimizelySDK.TestApp.Entities
{
    public class TestAppConsoleLogger : ILogger
    {
        private List<string> LogsList = new List<string>();
        public const string LOG_FILE = "TestAppLogs.txt";

        private static TestAppConsoleLogger _Instance;
        public static TestAppConsoleLogger Instance
        {
            get
            {
                if (_Instance == null)
                    _Instance = new TestAppConsoleLogger();

                return _Instance;
            }
        }

        private TestAppConsoleLogger() => File.Create(LOG_FILE);

        public void Log(LogLevel level, string message)
        {
            var line = string.Format("[{0}] : {1}", level, message);
            WriteToConsole(level, line);

            LogsList.Add($"[{DateTime.Now}] - {line}");
            WriteLogsToFile();
        }

        private void WriteToConsole(LogLevel level, string message)
        {
            ConsoleColor color = Console.ForegroundColor;

            if (level == LogLevel.ERROR)
                color = ConsoleColor.Red;
            else if (level == LogLevel.DEBUG)
                color = ConsoleColor.Yellow;

            Console.ForegroundColor = color;
            Console.WriteLine(message);

            Console.ResetColor();
        }

        private void WriteLogsToFile(bool forceWrite = false)
        {
            if (LogsList.Count > 50 || (forceWrite && LogsList.Count > 0))
            {
                using (var fileWriter = new StreamWriter(LOG_FILE, true))
                    LogsList.ForEach(log => fileWriter.WriteLine(log));

                LogsList.Clear();
            }
        }

        public void LogAppMessage(string message)
        {
            var prefixedMsg = "[TEST_APP_LOG] : " + message;

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(prefixedMsg);
            Console.ResetColor();

            LogsList.Add($"[{DateTime.Now}] - {prefixedMsg}");
            WriteLogsToFile();
        }

        ~TestAppConsoleLogger()
        {
            WriteLogsToFile(true);
        }
    }
}
