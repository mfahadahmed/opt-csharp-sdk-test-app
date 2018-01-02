using OptimizelySDK.Entity;
using OptimizelySDK.TestApp.Entities;
using System.Collections.Generic;

namespace OptimizelySDK.TestApp.Tests
{
    public class OptimizelyTests
    {
        private Optimizely OptimizelyClient = null;

        public OptimizelyTests(Optimizely optimizelyClient)
        {
            OptimizelyClient = optimizelyClient;
        }

        public Variation TestActivate(ActivateParams parameters)
        {
            return OptimizelyClient.Activate(parameters.ExperimentKey, parameters.UserId, parameters.UserAttributes);
        }

        public void TestTrack(TrackParams parameters)
        {
            OptimizelyClient.Track(parameters.EventKey, parameters.UserId, parameters.UserAttributes, parameters.EventTags);
        }

        public bool TestIsFeatureEnabled(IsFeatureEnabledParams parameters)
        {
            return OptimizelyClient.IsFeatureEnabled(parameters.FeatureKey, parameters.UserId, parameters.UserAttributes);
        }

        public List<string> TestGetEnabledFeatures(GetEnabledFeaturesParams parameters)
        {
            return OptimizelyClient.GetEnabledFeatures(parameters.UserId, parameters.UserAttributes);
        }
    }
}
