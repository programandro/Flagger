namespace Flagger
{
    internal interface IFeatureSource
    {
        bool IsEnabled(string featureName);
        bool IsEnabled(string featureName, string strategyName);
    }
}