using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace RThomasHyde.PluginFramework.Configuration.Providers
{
    /// <summary>
    /// Interface that specified the methods a PluginCatalogConfigurationProvider needs.
    /// </summary>
    public interface IPluginCatalogConfigurationLoader
    {
        /// <summary>
        /// The key of the catalogs section inside the parent configuration section (<see cref="SectionKey"/>).
        /// </summary>
        string CatalogsKey { get; }

        /// <summary>
        /// Returns a list that contains catalog configurations.
        /// </summary>
        /// <param name="configuration">The configuration to use.</param>
        /// <returns>A list that contains catalog configurations.</returns>
        List<CatalogConfiguration> GetCatalogConfigurations(IConfiguration configuration);
    }
}
