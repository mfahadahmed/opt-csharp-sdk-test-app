using OptimizelySDK.TestApp.Tests;
using OptimizelySDK.Entity;
using OptimizelySDK.TestApp.Entities;
using System.Collections.Generic;
using System.Reflection;
using System.IO;
using System;

namespace OptimizelySDK.TestApp
{
    public class OptimizelyRunner
    {
        private const string TestUserID = "TestUserID";
        private UserAttributes UserAttributes = new UserAttributes
        {
            { "device_type", "iPhone" },
            { "company", "Optimizely" },
            { "location", "San Francisco" }
        };

        private Optimizely OptimizelyClient = null;
        private OptimizelyTests OptimizelyTests = null;
        private TestAppConsoleLogger AppLogger = TestAppConsoleLogger.Instance;
        
        public OptimizelyRunner()
        {
            string configFileContent;
            var assembly = Assembly.GetExecutingAssembly();

            // Reading Config file content.
            using (var stream = assembly.GetManifestResourceStream(assembly.GetName().Name + ".TestData.json"))
            using (var reader = new StreamReader(stream))
                configFileContent = reader.ReadToEnd();

            // Initializing the Optimizely client.
            OptimizelyClient = new Optimizely(configFileContent, null, AppLogger);
            OptimizelyTests = new OptimizelyTests(OptimizelyClient);
        }

        public void RunTestCases(string option)
        {
            switch (option)
            {
                case "activate":
                    AppLogger.LogAppMessage("Running only Activate tests.");
                    RunActivateTests();
                    break;
                case "track":
                    AppLogger.LogAppMessage("Running only Track tests.");
                    RunTrackTests();
                    break;
                case "is_feature_enabled":
                    AppLogger.LogAppMessage("Running only IsFeatureEnabled tests.");
                    RunIsFeatureEnabledTests();
                    break;
                case "get_enabled_features":
                    AppLogger.LogAppMessage("Running only GetEnabledFeatures tests.");
                    RunGetEnabledFeaturesTest();
                    break;
                default:
                    AppLogger.LogAppMessage("Running All tests.");
                    RunAllTests();
                    break;
            }

            AppLogger.LogAppMessage($"All the required test cases have been executed. Please see log file {TestAppConsoleLogger.LOG_FILE} for details.");
        }
        
        public void RunAllTests()
        {
            RunActivateTests();
            RunTrackTests();
            RunIsFeatureEnabledTests();
            RunGetEnabledFeaturesTest();
        }

        private void PrintActivate(Variation variation)
        {
            if (variation == null || string.IsNullOrEmpty(variation.Id))
                AppLogger.LogAppMessage("The provided instance is Null!");
            else
                AppLogger.LogAppMessage($"Variation ID: {variation.Id}, Variation Key: {variation.Key}");
        }

        public void RunActivateTests()
        {
            var result = OptimizelyTests.TestActivate(new ActivateParams("test_experiment", TestUserID));
            PrintActivate(result);

            result = OptimizelyTests.TestActivate(new ActivateParams("test_experiment", TestUserID, UserAttributes));
            PrintActivate(result);
        }

        public void RunTrackTests()
        {
            var eventTags = new EventTags
            {
                {"int_param", 420 },
                {"bool_param", false },
                {"revenue", 1337 },
                {"value", 100 }
            };

            OptimizelyTests.TestTrack(new TrackParams("purchase", TestUserID));
            OptimizelyTests.TestTrack(new TrackParams("purchase", TestUserID, UserAttributes));
            OptimizelyTests.TestTrack(new TrackParams("purchase", TestUserID, UserAttributes, eventTags));
        }

        private void PrintIsFeatureEnabled(bool isEnabled)
        {
            AppLogger.LogAppMessage(isEnabled ? "Feature is enabled." : "Feature is not enabled.");
        }

        public void RunIsFeatureEnabledTests()
        {
            var result = OptimizelyTests.TestIsFeatureEnabled(new IsFeatureEnabledParams("invalid_feature", TestUserID));
            PrintIsFeatureEnabled(result);

            result = OptimizelyTests.TestIsFeatureEnabled(new IsFeatureEnabledParams("boolean_feature", TestUserID));
            PrintIsFeatureEnabled(result);

            result = OptimizelyTests.TestIsFeatureEnabled(new IsFeatureEnabledParams("multi_variate_feature", TestUserID));
            PrintIsFeatureEnabled(result);

            result = OptimizelyTests.TestIsFeatureEnabled(new IsFeatureEnabledParams("multi_variate_feature", TestUserID, UserAttributes));
            PrintIsFeatureEnabled(result);
        }

        private void PrintGetEnabledFeatures(List<string> featuresList)
        {
            if (featuresList == null || featuresList.Count == 0)
                AppLogger.LogAppMessage("Enabled Features: None!");
            else
                AppLogger.LogAppMessage($@"Enabled Features: ""{string.Join(", ", featuresList.ToArray())}"".");
        }

        public void RunGetEnabledFeaturesTest()
        {
            var featuresList = OptimizelyTests.TestGetEnabledFeatures(new GetEnabledFeaturesParams("invalid_feature", TestUserID));
            PrintGetEnabledFeatures(featuresList);

            featuresList = OptimizelyTests.TestGetEnabledFeatures(new GetEnabledFeaturesParams("boolean_feature", TestUserID));
            PrintGetEnabledFeatures(featuresList);

            featuresList = OptimizelyTests.TestGetEnabledFeatures(new GetEnabledFeaturesParams("multi_variate_feature", TestUserID));
            PrintGetEnabledFeatures(featuresList);

            featuresList = OptimizelyTests.TestGetEnabledFeatures(new GetEnabledFeaturesParams("multi_variate_feature", TestUserID, UserAttributes));
            PrintGetEnabledFeatures(featuresList);
        }
    }
}
