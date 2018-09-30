using System;

namespace Flagger.Unity
{
    public class StrategyTypeResolver
    {
        public StrategyTypeResolver(string strategyName, Type type)
            : this(strategyName, null, type, null)
        { }

        public StrategyTypeResolver(string strategyName, string name, Type type)
            : this(strategyName, name, type, null)
        { }

        public StrategyTypeResolver(string strategyName, object instance)
            : this(strategyName, null, null, instance)
        { }

        protected StrategyTypeResolver(string strategyName, string name, Type type, object instance)
        {
            StrategyName = strategyName;
            Type = type;
            InjectionName = name;
            Instance = instance;
        }

        public string StrategyName { get; set; }
        public Type Type { get; set; }
        public string InjectionName { get; set; }
        public object Instance { get; set; }
    }

    public class StrategyTypeResolver<T> : StrategyTypeResolver
    {
        public StrategyTypeResolver(string strategyName)
            : base(strategyName, null, typeof(T), null)
        { }

        public StrategyTypeResolver(string strategyName, string name)
            : base(strategyName, name, typeof(T), null)
        { }

        public StrategyTypeResolver(string strategyName, object instance)
            : base(strategyName, null, typeof(T), instance)
        { }
    }
}