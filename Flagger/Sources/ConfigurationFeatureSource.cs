using Flagger.Configuration;
using Flagger.Sources;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Flagger
{
    public class ConfigurationFeatureSource : PreloadFeatureSource
    {
        public static string RootSectionName => "flagger";
        private readonly IConfiguration _configuration;

        public ConfigurationFeatureSource(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override IEnumerable<Feature> LoadFeatures()
        {
            var featuresSection = _configuration.GetSection(RootSectionName);
            FeaturesSection features;
            if (featuresSection == null)
                features = FeaturesSection.Empty;

            features = new FeaturesSection();
            featuresSection.Bind(features);
            return features.Features;
        }
    }
}
