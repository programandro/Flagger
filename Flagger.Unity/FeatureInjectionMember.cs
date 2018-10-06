using System;
using Unity;
using Unity.Injection;
using Unity.Policy;
using Unity.Registration;
using System.Linq;
using Unity.Policy.Mapping;
using Unity.Builder;

namespace Flagger.Unity
{
    public class FeatureInjectionMember : InjectionFactory
    {
        public FeatureInjectionMember(string featureName, Type nullPatternType, params StrategyTypeResolver[] strategies)
            :base(GetFactoryFunction(featureName, nullPatternType, strategies))
        { }

        public FeatureInjectionMember(string featureName, object nullPatternObject, params StrategyTypeResolver[] strategies)
            : base(GetFactoryFunction(featureName, nullPatternObject, strategies))
        { }

        public FeatureInjectionMember(string featureName, params StrategyTypeResolver[] strategies)
            : base(GetFactoryFunction(featureName, strategies))
        { }

        private static Func<IUnityContainer, Type, string, object> GetFactoryFunction(string featureName, Type nullPatternType, StrategyTypeResolver[] strategies)
        {
            return (c, t, n) =>
            {
                if (!Flag.IsEnabled(featureName))
                    return c.Resolve(nullPatternType);

                return ResolveFromStrategies(featureName, strategies, c);
            };
        }

        private static Func<IUnityContainer, Type, string, object> GetFactoryFunction(string featureName, object nullPatternObject, StrategyTypeResolver[] strategies)
        {
            return (c, t, n) =>
            {
                if (!Flag.IsEnabled(featureName))
                    return nullPatternObject;

                return ResolveFromStrategies(featureName, strategies, c);
            };
        }

        private static Func<IUnityContainer, Type, string, object> GetFactoryFunction(string featureName, StrategyTypeResolver[] strategies)
        {
            return (c, t, n) =>
            {
                if (!Flag.IsEnabled(featureName))
                    return null;

                return ResolveFromStrategies(featureName, strategies, c);
            };
        }

        private static object ResolveFromStrategies(string featureName, StrategyTypeResolver[] strategies, IUnityContainer container)
        {
            var strategy = strategies?.FirstOrDefault(s => Flag.IsEnabled(featureName, s.StrategyName));

            if (strategy != null)
            {
                if (strategy.Type != null)
                    return container.Resolve(strategy.Type);

                return strategy.Instance;
            }

            return null;
        }
    }

    public class FeatureInjectionMemeber<T> : FeatureInjectionMember
    {
        public FeatureInjectionMemeber(string featureName, params StrategyTypeResolver<T>[] strategies) : base(featureName, strategies)
        {
        }
    }

    public class FeatureInjectionMemeber<T, TNull> : FeatureInjectionMember where TNull : T
    {
        public FeatureInjectionMemeber(string featureName, params StrategyTypeResolver<T>[] strategies) : base(featureName, typeof(TNull), strategies)
        {
        }

        public FeatureInjectionMemeber(string featureName, TNull nullPatternObject, params StrategyTypeResolver<T>[] strategies) : base(featureName, nullPatternObject, strategies)
        {
        }
    }
}
