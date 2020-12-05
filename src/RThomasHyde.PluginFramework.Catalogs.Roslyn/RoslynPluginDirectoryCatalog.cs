using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using RThomasHyde.PluginFramework.Abstractions;

namespace RThomasHyde.PluginFramework.Catalogs.Roslyn
{
    public class RoslynPluginDirectoryCatalog : IPluginCatalog
    {
        private readonly RoslynPluginDirectoryCatalogOptions options;
        private CompositePluginCatalog internalCatalog;
        private readonly Regex nameRegex;
        private readonly Regex versionRegex;
        private readonly Regex referencesRegex;

        public RoslynPluginDirectoryCatalog(string directoryPath, RoslynPluginDirectoryCatalogOptions options = null)
        {
            this.options = options ?? new RoslynPluginDirectoryCatalogOptions();
            nameRegex = new Regex(@"^[/]{2}\s*name[:]\s*(.*)$", RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Multiline);
            versionRegex = new Regex(@"^[/]{2}\s*version[:]\s*([0-9]\.[0-9]).*$", RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Multiline);
            referencesRegex = new Regex(@"^[#]r\s*"".*""\s*$", RegexOptions.Compiled | RegexOptions.Multiline);
        }

        public Task Initialize()
        {
            return internalCatalog?.Initialize();
        }

        public bool IsInitialized => internalCatalog.IsInitialized;

        public List<Plugin> GetPlugins()
        {
            return internalCatalog.GetPlugins();
        }

        public Plugin Get(string name, Version version = null)
        {
            version ??= new Version(1,0);
            return internalCatalog.Get(name, version);
        }

        private void LoadScripts(string directoryPath)
        {
            var scriptFiles = Directory.GetFiles(directoryPath, options.ScriptFileNamePattern,
                options.SearchSubfolders ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly);

            foreach (var scriptFile in scriptFiles)
            {
                try
                {
                    var script = LoadScript(scriptFile);
                    var scriptCatalog = new RoslynPluginCatalog(script.code, description:script.name, productVersion:script.version);
                    internalCatalog.AddCatalog(scriptCatalog);
                }
                catch (Exception e)
                {
                    options.ScriptLoadFailureCallback?.Invoke(scriptFile, e);
                }
            }
        }

        private (string name, string version, string code) LoadScript(string scriptFile)
        {
            var code = File.ReadAllText(scriptFile);
            return options.ScriptPreProcessFunction(code);
        }
    }
}
