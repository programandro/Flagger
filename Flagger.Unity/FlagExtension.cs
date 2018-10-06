using System;
using System.Collections.Generic;
using System.Text;
using Unity.Builder;
using Unity.Extension;

namespace Flagger.Unity
{
    public class FlagExtension : UnityContainerExtension
    {
        protected override void Initialize()
        {
            Context.Strategies.Add(new FlagBuildStrategy(), UnityBuildStage.Creation);
            Context.Strategies.Add(new FlagBuildStrategy(), UnityBuildStage.Enumerable);
            Context.Strategies.Add(new FlagBuildStrategy(), UnityBuildStage.Initialization);
            Context.Strategies.Add(new FlagBuildStrategy(), UnityBuildStage.Lifetime);
            Context.Strategies.Add(new FlagBuildStrategy(), UnityBuildStage.PostInitialization);
            Context.Strategies.Add(new FlagBuildStrategy(), UnityBuildStage.PreCreation);
            Context.Strategies.Add(new FlagBuildStrategy(), UnityBuildStage.Setup);
            Context.Strategies.Add(new FlagBuildStrategy(), UnityBuildStage.TypeMapping);
        }
    }
}
