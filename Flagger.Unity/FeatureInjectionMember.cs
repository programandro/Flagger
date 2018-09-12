using System;
using Unity;
using Unity.Injection;

namespace Flagger.Unity
{
    public class FeatureInjectionMember : InjectionFactory
    {
        private static Func<IUnityContainer, Type, string, object> GetFactoryFunction(string featureName, object emptyInstance)
        {
            return (c, t, s) =>
            {
                if (Flag.IsEnabled(featureName))
                    
            };
        }


        public FeatureInjectionMember(string featureName, object emptyInstance) : base(GetFactoryFunction(featureName, emptyInstance))
        {
        }
    }
}
