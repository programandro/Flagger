using Flagger.Configuration;
using Flagger.Sources;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Flagger
{
    public class ConfigurationFlagSource : IFeatureSource
    {
        public static string FeaturesSectionName => "features";
        private readonly InMemoryFeatureSource _memorySource;

        public ConfigurationFlagSource(IConfiguration configuration)
        {
            var featuresSection = configuration.GetSection(FeaturesSectionName);
            FeaturesSection features;
            if (featuresSection == null)
                features = FeaturesSection.Empty;

            features = new FeaturesSection();
            featuresSection.Bind(features);
            _memorySource = new InMemoryFeatureSource(features.Features);
        }

        public bool IsEnabled(string featureName)
            => _memorySource.IsEnabled(featureName);

        public bool IsEnabled(string featureName, string strategyName)
            => _memorySource.IsEnabled(featureName, strategyName);
    }
}
