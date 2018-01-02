using OptimizelySDK.Entity;

namespace OptimizelySDK.TestApp.Entities
{
    public class ActivateParams
    {
        public string ExperimentKey { get; }
        public string UserId { get; }
        public UserAttributes UserAttributes { get; }

        public ActivateParams(string experimentKey, string userId, UserAttributes userAttributes = null)
        {
            ExperimentKey = experimentKey;
            UserId = userId;
            UserAttributes = userAttributes;
        }
    }

    public class TrackParams
    {
        public string EventKey { get; }
        public string UserId { get; }
        public UserAttributes UserAttributes { get; }
        public EventTags EventTags { get; }

        public TrackParams(string eventKey, string userId, UserAttributes userAttributes = null, EventTags eventTags = null)
        {
            EventKey = eventKey;
            UserId = userId;
            UserAttributes = userAttributes;
            EventTags = eventTags;
        }
    }

    public class GetVariationParams
    {
        public string ExperimentKey { get; }
        public string UserId { get; }
        public UserAttributes UserAttributes { get; }

        public GetVariationParams(string experimentKey, string userId, UserAttributes userAttributes = null)
        {
            ExperimentKey = experimentKey;
            UserId = userId;
            UserAttributes = userAttributes;
        }
    }

    public class IsFeatureEnabledParams
    {
        public string FeatureKey { get; }
        public string UserId { get; }
        public UserAttributes UserAttributes { get; }

        public IsFeatureEnabledParams(string featureKey, string userId, UserAttributes userAttributes = null)
        {
            FeatureKey = featureKey;
            UserId = userId;
            UserAttributes = userAttributes;
        }
    }

    public class GetEnabledFeaturesParams
    {
        public string FeatureKey { get; }
        public string UserId { get; }
        public UserAttributes UserAttributes { get; }

        public GetEnabledFeaturesParams(string featureKey, string userId, UserAttributes userAttributes = null)
        {
            FeatureKey = featureKey;
            UserId = userId;
            UserAttributes = userAttributes;
        }
    }
}
