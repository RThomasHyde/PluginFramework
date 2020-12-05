using System;
using System.Reflection;

namespace RThomasHyde.PluginFramework.TypeFinding
{
    public interface ITypeFindingContext
    {
        Assembly FindAssembly(string assemblyName);
        Type FindType(Type type);
    }
}