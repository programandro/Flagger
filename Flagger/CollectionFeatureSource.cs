using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Flagger
{
    public abstract class CollectionFeatureSource : IFeatureSource
    {
        internal IEnumerable<Feature> Features => _features;
        private IEnumerable<Feature> _features;

        internal CollectionFeatureSource(IEnumerable<Feature> features)
        {
            _features = features;
        }

        public bool IsEnabled(string featureName)
        {
            if (_features == null)
                return false;

            var feature = GetFeature(featureName);
            return feature.Enabled;
        }

        private Feature GetFeature(string featureName)
        {
            var feature = _features.FirstOrDefault(f => f.Name == featureName);
            if (feature == null)
                throw new ArgumentException($"No feature exist with name {StringOrNull(featureName)}");

            return feature;
        }

        public bool IsEnabled(string featureName, string strategyName)
        {
            if (_features == null)
                return false;

            var feature = GetFeature(featureName);

            if (feature.Strategies == null)
                return false;

            var strategy = feature.Strategies.FirstOrDefault(s => s.Name == strategyName);
            if (strategy == null)
                throw new ArgumentException($"No startegy exist with name {StringOrNull(strategyName)} in feature {StringOrNull(featureName)}");

            return strategy.Enabled;
        }

        private string StringOrNull(string value)
            => value == null ? "[null]" : "'" + value + "'";
    }
}
