using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using RThomasHyde.PluginFramework.Abstractions;

namespace RThomasHyde.PluginFramework.Configuration.Providers
{
    /// <summary>
    /// Implementation of <see cref="IPluginCatalogConfigurationLoader"/> that
    /// loads a list of <see cref="CatalogConfiguration"/> objects from the <see cref="IConfiguration"/> object.
    /// </summary>
    public class PluginCatalogConfigurationLoader : IPluginCatalogConfigurationLoader
    {
        private PluginFrameworkOptions _options;

        ///<inheritdoc/>
        public virtual string CatalogsKey => "Catalogs";

        public PluginCatalogConfigurationLoader(IOptions<PluginFrameworkOptions> options)
        {
            _options = options.Value;
        }

        ///<inheritdoc/>
        public List<CatalogConfiguration> GetCatalogConfigurations(IConfiguration configuration)
        {
            var catalogs = new List<CatalogConfiguration>();

            configuration.Bind($"{_options.ConfigurationSection}:{CatalogsKey}", catalogs);

            return catalogs;
        }
    }
}
