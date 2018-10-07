using Flagger;
using Flagger.Sources;
using Flagger.Unity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            var conf = new ConfigurationBuilder()
                            .AddXmlFile(@"app.config")
                            .Build();

            
            Flag.SetSource(new ConfigurationFeatureSource(conf));

            var container = new UnityContainer();

            container.RegisterType<ISolution>(new FeatureInjectionMember("solution",
                                                                    new StrategyTypeResolver("a", typeof(SolutionA)),
                                                                    new StrategyTypeResolver("b", typeof(SolutionB))));

            var solution = container.Resolve<ISolution>();
            Console.WriteLine(solution.Get());
        }
    }

    public interface ISolution { string Get(); }
    public class SolutionA : ISolution
    {
        public string Get()
            => "A";
    }
    public class SolutionB : ISolution
    {
        public string Get()
            => "B";
    }
}
