using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Flagger.Sources
{
    public abstract class PreloadFeatureSource : IFeatureSource
    {
        public IEnumerable<Feature> Features => _features;
        private readonly IEnumerable<Feature> _features;

        internal PreloadFeatureSource()
        {
            _features = LoadFeatures();
        }

        protected abstract IEnumerable<Feature> LoadFeatures();

        public bool IsEnabled(string featureName)
            => GetFeature(featureName).Enabled;

        private Feature GetFeature(string featureName)
        {
            var feature = _features?.FirstOrDefault(f => f.Name == featureName);
            if (feature == null)
                throw new ArgumentException($"No feature exist with name {StringOrNull(featureName)}");

            return feature;
        }

        private Strategy GetStrategy(string featureName, string strategyName)
        {
            var feature = GetFeature(featureName);
            var strategy = feature.Strategies?.FirstOrDefault(s => s.Name == strategyName);
            if (strategy == null)
                throw new ArgumentException($"No startegy exist with name {StringOrNull(strategyName)} in feature {StringOrNull(featureName)}");

            return strategy;
        }

        public bool IsEnabled(string featureName, string strategyName)
            => GetStrategy(featureName, strategyName).Enabled;

        private string StringOrNull(string value)
            => value == null ? "[null]" : "'" + value + "'";
    }
}
