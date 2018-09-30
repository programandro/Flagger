using System;
using Unity;
using Unity.Injection;
using Unity.Policy;
using Unity.Registration;
using System.Linq;

namespace Flagger.Unity
{
    public class FeatureInjectionMember<T> : FeatureInjectionMember
    {
        public FeatureInjectionMember(string featureName, Type nullType, params StrategyTypeResolver[] strategies)
            : base(BuildFactoryFunction(featureName, null, nullType, null, strategies))
        {
        }

        public FeatureInjectionMember(string featureName, string nullName, Type nullType, params StrategyTypeResolver[] strategies)
            : base(BuildFactoryFunction(featureName, nullName, nullType, null, strategies))
        {
        }

        public FeatureInjectionMember(string featureName, object nullInstance, params StrategyTypeResolver[] strategies)
            : base(BuildFactoryFunction(featureName, null, null, nullInstance, strategies))
        {
        }

        public FeatureInjectionMember(string featureName, params StrategyTypeResolver[] strategies)
            : base(BuildFactoryFunction(featureName, null, null, null, strategies))
        {
        }
    }

    public class FeatureInjectionMember : InjectionFactory
    {
        public FeatureInjectionMember(string featureName, Type nullType, params StrategyTypeResolver[] strategies) 
            : base(BuildFactoryFunction(featureName, null, nullType, null, strategies))
        {
        }

        public FeatureInjectionMember(string featureName, string nullName, Type nullType, params StrategyTypeResolver[] strategies)
            : base(BuildFactoryFunction(featureName, nullName, nullType, null, strategies))
        {
        }

        public FeatureInjectionMember(string featureName, object nullInstance, params StrategyTypeResolver[] strategies)
            : base(BuildFactoryFunction(featureName, null, null, nullInstance, strategies))
        {
        }

        public FeatureInjectionMember(string featureName, params StrategyTypeResolver[] strategies)
            : base(BuildFactoryFunction(featureName, null, null, null, strategies))
        {
        }

        private static Func<IUnityContainer, Type, string, object> BuildFactoryFunction(string featureName, object nullInstance, StrategyTypeResolver[] strategies)
        {
            return (container, type, name) =>
            {
                if (!Flag.IsEnabled(featureName))
                    return Resolve(container, name, type, nullInstance);

                var strategy = strategies?.FirstOrDefault(s => Flag.IsEnabled(featureName, s.StrategyName));
                if (strategy == null)
                    return null;

                return Resolve(container, strategy.InjectionName, strategy.Type, strategy.Instance);
            };
        }

        private static object Resolve(IUnityContainer container, string name, Type type, object instance)
        {
            if (!string.IsNullOrEmpty(name) && type != null)
                container.Resolve(type, name);

            if (type != null)
                container.Resolve(type);

            return instance;
        }
    }
}
