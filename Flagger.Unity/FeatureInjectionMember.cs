using System;
using Unity;
using Unity.Injection;
using Unity.Policy;
using Unity.Registration;

namespace Flagger.Unity
{
    public class FeatureInjectionMember : InjectionFactory
    {
        public FeatureInjectionMember(Func<IUnityContainer, object> factoryFunc) : base(factoryFunc)
        {
        }
    }
}
