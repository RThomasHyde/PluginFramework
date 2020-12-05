using System;
using System.Collections.Generic;
using System.Text;

namespace RThomasHyde.PluginFramework.Catalogs.NuGet.PackageManagement
{
    public class PackageNotFoundException : Exception
    {
        public PackageNotFoundException(string message)
            : base(message)
        {
        }
    }
}
