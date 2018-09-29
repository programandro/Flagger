using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Flagger.Sources
{
    public class InMemoryFeatureSource : PreloadFeatureSource
    {
        private readonly IEnumerable<Feature> _features;

        internal InMemoryFeatureSource(IEnumerable<Feature> features)
        {
            _features = features;
        }

        protected override IEnumerable<Feature> LoadFeatures()
            => _features;
    }
}
