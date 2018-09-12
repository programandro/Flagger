using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = new UnityContainer();
            container.RegisterType<ISolution, SolutionB>();
            container.RegisterType<ISolution, SolutionA>();

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
