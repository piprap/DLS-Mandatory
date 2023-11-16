using FeatureHubSDK;
using System.Runtime.Versioning;

namespace FeatureHub
{
    public static class FeatureFlag
    {
        public static bool MultiplicationFeatureIsEnabled { get; set; }
     
        public static async void PrepareFeatures()
        {
            var multiplicationFeatureConfig = new EdgeFeatureHubConfig("http://featurehub:8085", "4add7269-7e4a-409e-8ad6-d6fff42a39ab/KqMKEOOC2AXAtVZfqPigI1QUgYSyB2OKb4m0cQEO");
            var fh = await multiplicationFeatureConfig.NewContext().Build();

            MultiplicationFeatureIsEnabled = fh["MultipliplicationFeature"].IsEnabled;

        }

    }
    

}