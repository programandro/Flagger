using System.Collections.Generic;

namespace Flagger
{
    public class Feature
    {
        public string Name { get; set; }
        public bool Enabled { get; set; }   
        public IEnumerable<Strategy> Strategies { get; set; }
    }
}