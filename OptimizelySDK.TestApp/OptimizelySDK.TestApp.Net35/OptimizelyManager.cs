using System;
using System.IO;

namespace OptimizelySDK.TestApp
{
    public static class OptimizelyManager
    {
        public static Optimizely OptimizelyClient { get; }

        static OptimizelyManager()
        {
            string relativePath = Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\TestData.json");
            var configFilePath = Path.GetFullPath(relativePath);

            if (!File.Exists(configFilePath))
                throw new Exception("Config file not found.");
            
            string configFileContent = File.ReadAllText(configFilePath);
            OptimizelyClient = new Optimizely(configFileContent);
        }
    }
}
