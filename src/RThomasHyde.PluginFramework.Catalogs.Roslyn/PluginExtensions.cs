using System;
using RThomasHyde.PluginFramework.Abstractions;

namespace RThomasHyde.PluginFramework.Catalogs.Roslyn
{
    public static class PluginExtensions
    {
        public static bool TryCreateInstance<T>(this Plugin plugin, out T instance) where T : class
        {
            try
            {
                instance = Activator.CreateInstance(plugin) as T;
                return instance != null;
            }
            catch
            {
                instance = null;
                return false;
            }
        }
    }
}
