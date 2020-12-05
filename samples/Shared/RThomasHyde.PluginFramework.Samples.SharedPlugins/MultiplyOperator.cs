using System.ComponentModel;
using RThomasHyde.PluginFramework.Samples.Shared;

namespace RThomasHyde.PluginFramework.Samples.SharedPlugins
{
    [DisplayName("The multiplier plugin")]
    public class MultiplyOperator : IOperator
    {
        public int Calculate(int x, int y)
        {
            return x * y;
        }
    }
}
