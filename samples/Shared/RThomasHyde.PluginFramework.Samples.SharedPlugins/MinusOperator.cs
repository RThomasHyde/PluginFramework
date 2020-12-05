using RThomasHyde.PluginFramework.Samples.Shared;

namespace RThomasHyde.PluginFramework.Samples.SharedPlugins
{
    public class MinusOperator : IOperator
    {
        public int Calculate(int x, int y)
        {
            return x - y;
        }
    }
}