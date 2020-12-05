using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Scripting;
using RThomasHyde.PluginFramework.Abstractions;
using RThomasHyde.PluginFramework.Catalogs.Roslyn;
using RThomasHyde.TypeGenerator;

// ReSharper disable once CheckNamespace
namespace RThomasHyde.PluginFramework.Catalogs
{
    public class RoslynPluginCatalog : IPluginCatalog
    {
        private readonly RoslynPluginCatalogOptions _options;
        private readonly string _code;

        private Assembly _assembly;

        // private Plugin _plugin;
        private AssemblyPluginCatalog _catalog;

        public RoslynPluginCatalog(string code, RoslynPluginCatalogOptions options = null, string description = null, string productVersion = null)
        {
            if (string.IsNullOrWhiteSpace(code))
            {
                throw new ArgumentOutOfRangeException(nameof(code), code, "Code can not be null or empty");
            }

            _code = code;
            _options = options ?? new RoslynPluginCatalogOptions();

            _options.PluginNameOptions.PluginDescriptionGenerator = (nameOptions, type) => description;
            _options.PluginNameOptions.PluginProductVersionGenerator = (nameOptions, type) => productVersion;
            _options.CustomAssemblyLoadContext ??= new CustomAssemblyLoadContext(_options.AdditionalReferences);
        }

        public Plugin Get(string name, Version version)
        {
            return _catalog.Get(name, version);
        }

        public bool IsInitialized { get; private set; }

        private async Task<bool> IsScript()
        {
            try
            {
                var csharScript = CSharpScript.Create(_code, ScriptOptions.Default);

                var compilation = csharScript.GetCompilation();

                var syntaxTree = compilation.SyntaxTrees.Single();

                var descendants = (await syntaxTree.GetRootAsync())
                    .DescendantNodes().ToList();

                var classDeclarations = descendants.OfType<ClassDeclarationSyntax>().FirstOrDefault();

                if (classDeclarations == null)
                {
                    return true;
                }

                return false;
            }
            catch (Exception e)
            {
                throw new InvalidCodeException("Failed to determine if code is script or regular. Code: " + _code, e);
            }
        }

        public async Task Initialize()
        {
            try
            {
                var isScript = await IsScript();

                if (isScript)
                {
                    var scriptInitializer = new ScriptInitializer(_code, _options);
                    _assembly = await scriptInitializer.CreateAssembly(_options.CustomAssemblyLoadContext);
                }
                else
                {
                    var regularInitializer = new RegularInitializer(_code, _options);
                    _assembly = await regularInitializer.CreateAssembly(_options.CustomAssemblyLoadContext);
                }

                var options = new AssemblyPluginCatalogOptions { PluginNameOptions = _options.PluginNameOptions };

                _catalog = new AssemblyPluginCatalog(_assembly, options);
                await _catalog.Initialize();

                IsInitialized = true;
            }
            catch (Exception e)
            {
                throw new InvalidCodeException($"Failed to initialize catalog with code: {Environment.NewLine}{_code}", e);
            }
        }

        public List<Plugin> GetPlugins()
        {
            return _catalog.GetPlugins();
        }

        public void UnloadPlugins()
        {
            _options.CustomAssemblyLoadContext?.Unload();
            GC.Collect();
        }
    }
}
