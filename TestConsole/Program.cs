using Flagger.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;
using Unity.Injection;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = new UnityContainer();
            container.AddExtension(new FlagExtension());
            //container.RegisterType<ISolution>(new InjectionFactory(c => new SolutionA()));

            container.RegisterType<ISolution>(new FeatureInjectionMember("solution", null, 
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
