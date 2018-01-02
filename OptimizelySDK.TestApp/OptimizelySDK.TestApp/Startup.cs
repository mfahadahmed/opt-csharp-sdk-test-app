using System;

namespace OptimizelySDK.TestApp
{
    public static class Startup
    {
        public static void RunProgram(string[] args)
        {
            string strOption = args.Length > 0 ? args[0] : "";
            new OptimizelyRunner().RunTestCases(strOption);

            Console.WriteLine("\nPress any key to exit the program.");
            Console.ReadKey();
        }
    }
}
