using RThomasHyde.PluginFramework.Samples.Shared;

namespace RThomasHyde.PluginFramework.Samples.SharedPlugins
{
    public class SumOperator : IOperator
    {
        public int Calculate(int x, int y)
        {
            return x + y;
        }
    }
}