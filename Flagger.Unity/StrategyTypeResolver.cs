using System;

namespace Flagger.Unity
{
    public class StrategyTypeResolver
    {
        public StrategyTypeResolver(string strategyName, Type type)
            : this(strategyName, type, null)
        { }

        public StrategyTypeResolver(string strategyName, object instance)
            : this(strategyName, null, instance)
        { }

        protected StrategyTypeResolver(string strategyName, Type type, object instance)
        {
            StrategyName = strategyName;
            Type = type;
            Instance = instance;
        }

        public string StrategyName { get; set; }
        public Type Type { get; set; }
        public object Instance { get; set; }
    }

    public class StrategyTypeResolver<T> : StrategyTypeResolver
    {
        public StrategyTypeResolver(string strategyName)
            : base(strategyName, typeof(T), null)
        { }

        public StrategyTypeResolver(string strategyName, object instance)
            : base(strategyName, typeof(T), instance)
        { }
    }
}