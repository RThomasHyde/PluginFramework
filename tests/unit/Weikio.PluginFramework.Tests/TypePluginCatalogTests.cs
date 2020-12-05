using System.Threading.Tasks;
using RThomasHyde.PluginFramework.Abstractions;
using RThomasHyde.PluginFramework.Catalogs;
using RThomasHyde.PluginFramework.Tests.Plugins;
using Xunit;

namespace RThomasHyde.PluginFramework.Tests
{
    public class TypePluginCatalogTests
    {
        [Fact]
        public async Task CanInitialize()
        {
            var catalog = new TypePluginCatalog(typeof(TypePlugin));
            await catalog.Initialize();

            var plugins = catalog.GetPlugins();
            Assert.Single(plugins);
        }

        [Fact]
        public async Task NameIsTypeFullName()
        {
            var catalog = new TypePluginCatalog(typeof(TypePlugin));
            await catalog.Initialize();

            var thePlugin = catalog.Single();
        
            Assert.Equal("RThomasHyde.PluginFramework.Tests.Plugins.TypePlugin", thePlugin.Name);
        }
        
        [Fact]
        public async Task CanConfigureNameResolver()
        {
            var catalog = new TypePluginCatalog(typeof(TypePlugin), configure =>
            {
                configure.PluginNameGenerator = (opt, type) => "HelloOptions";
            });
        
            await catalog.Initialize();

            var thePlugin = catalog.Single();
        
            Assert.Equal("HelloOptions", thePlugin.Name);
        }
        
        
        [Fact]
        public async Task CanSetNameByAttribute()
        {
            var catalog = new TypePluginCatalog(typeof(TypePluginWithName));
            await catalog.Initialize();

            var thePlugin = catalog.Single();
        
            Assert.Equal("MyCustomName", thePlugin.Name);
        }
    }
}
