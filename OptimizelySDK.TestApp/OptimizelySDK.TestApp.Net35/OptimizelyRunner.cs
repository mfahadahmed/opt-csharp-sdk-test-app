using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OptimizelySDK.TestApp.Tests;
using OptimizelySDK.Entity;
using OptimizelySDK.TestApp.Entities;

namespace OptimizelySDK.TestApp
{
    public static class OptimizelyRunner
    {
        private const string TEST_USER_ID = "TestUserID";

        private static void PrintVariation(Variation variation)
        {
            if (variation != null)
                Console.WriteLine($"Variation ID: {variation.Id}, Variation Key: {variation.Key}");
            else
                Console.WriteLine("Null Variation!");
        }

        public static void RunActivateTests()
        {
            var result = OptimizelyTests.TestActivate(new ActivateParams("test_experiment", TEST_USER_ID));
            PrintVariation(result);

            result = OptimizelyTests.TestActivate(new ActivateParams("test_experiment", TEST_USER_ID, new Entity.UserAttributes
            {
                { "device_type", "iPhone" },
                { "location", "San Francisco" }
            }));
            PrintVariation(result);
        }

        public static void RunAllTests()
        {
            RunActivateTests();
        }
    }
}
