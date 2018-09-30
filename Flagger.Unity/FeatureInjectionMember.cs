using System;
using Unity;
using Unity.Injection;
using Unity.Policy;
using Unity.Registration;
using System.Linq;
using Unity.Policy.Mapping;

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

    public class FeatureInjectionMember : InjectionMember
    {
        private string _featureName;
        private Type _emptyType;
        private StrategyTypeResolver[] _strategies;

        private static string EmptyStrategyName => "empty";

        public FeatureInjectionMember(string featureName, Type emptyType, params StrategyTypeResolver[] strategies)
        {
            _featureName = featureName;
            _emptyType = emptyType;
            _strategies = strategies;
        }

        public override void AddPolicies(Type serviceType, Type implementationType, string name, IPolicyList policies)
        {
            if (_emptyType != null)
            {
                var emptyPolicy = BuildPolicy(_featureName, EmptyStrategyName, name, _emptyType, policies);
                policies.Set()
            }


            
        }

        private IBuilderPolicy BuildPolicy(string featureName, string strategyName, string name, Type type, IPolicyList policies)
            => new BuildKeyMappingPolicy(type, $"$${featureName}$${strategyName}$${name ?? string.Empty}", true);
    }
}
