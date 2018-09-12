using System;

namespace Flagger
{
    public static class Flag
    {
        private static IFeatureSource _flagSource;

        internal static void SetSource(IFeatureSource flagSource)
        {
            _flagSource = flagSource;
        }

        public static bool IsEnabled(string feature)
            => _flagSource.IsEnabled(feature);

        public static bool IsEnabled(string feature, string strategy)
            => _flagSource.IsEnabled(feature, strategy);
    }
}
