using System;
using RThomasHyde.PluginFramework.Samples.Shared;

namespace RThomasHyde.PluginFramework.Samples.SharedPlugins
{
    public class SecondSharedPlugin : IOutPlugin
    {
        public string Get()
        {
            return "Second shared plugin";
        }
    }
}
