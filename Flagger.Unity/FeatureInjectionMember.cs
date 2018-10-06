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
            string newName;
            if (_emptyType != null)
            {
                var emptyPolicy = BuildPolicy(_featureName, EmptyStrategyName, name, _emptyType, out newName);
                policies.Set(emptyPolicy, new NamedTypeBuildKey(implementationType, newName));
            }

            if (policies != null)
            {
                foreach (var strategy in _strategies)
                {
                    var policy = BuildPolicy(_featureName, strategy.StrategyName, name, strategy.Type, out newName);
                    policies.Set(policy, newName);
                }
            }
        }

        private IBuilderPolicy BuildPolicy(string featureName, string strategyName, string name, Type type, out string newName)
        {
            newName = $"$${featureName}$${strategyName}$${name ?? string.Empty}";
            return new BuildKeyMappingPolicy(type, newName, true);
        }
    }
}
