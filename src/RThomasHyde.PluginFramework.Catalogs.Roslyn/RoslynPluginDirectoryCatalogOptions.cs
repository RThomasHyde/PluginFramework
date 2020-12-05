using System;
using System.IO;

namespace RThomasHyde.PluginFramework.Catalogs.Roslyn
{
    public class RoslynPluginDirectoryCatalogOptions
    {
        public bool SearchSubfolders { get; set; } = false;
        public string ScriptFileNamePattern { get; set; } = "*.csx";
        public Func<string, (string name, string version, string code)> ScriptPreProcessFunction { get; set; } = s => (Guid.NewGuid().ToString(), "1.0", s);
        public Action<string, Exception> ScriptLoadFailureCallback { get; set; }
    }
}
