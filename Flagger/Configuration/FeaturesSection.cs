using System;
using System.Collections.Generic;
using System.Text;

namespace Flagger.Configuration
{
    internal class FeaturesSection
    {
        public Feature[] Features { get; set; }

        public FeaturesSection()
        {
            Features = new Feature[0];
        }

        private static FeaturesSection _empty;
        public static FeaturesSection Empty
            => _empty ?? (_empty = new FeaturesSection());
    }
}
