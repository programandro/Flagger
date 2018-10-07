using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Flagger.Sources
{
    public class ConfigurationFeatureSource : PreloadFeatureSource
    {
        private readonly IConfiguration _configuration;

        public ConfigurationFeatureSource(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public ConfigurationFeatureSource()
        {
            //new ConfigurationBuilder().AddXmlFile()
        }

        protected override IEnumerable<Feature> LoadFeatures()
        {
            throw new NotImplementedException();
        }
    }
}
