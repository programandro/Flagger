using System;
using System.Collections.Generic;
using System.Text;
using Unity;
using Unity.Builder;
using Unity.Builder.Strategy;
using Unity.Policy;
using Unity.Registration;

namespace Flagger.Unity
{
    public class FlagBuildStrategy : BuilderStrategy
    {
        public override void PreBuildUp(IBuilderContext context)
        {
            var policy = context.Policies.Get<IBuildKeyMappingPolicy>(context.BuildKey);

            if (policy != null)
            {
                context.BuildKey = policy.Map(context.BuildKey, context);
                return;
            }


        }

        public override void PostBuildUp(IBuilderContext context)
        {
            base.PostBuildUp(context);
        }
    }
}
