using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using RThomasHyde.PluginFramework.Abstractions;
using RThomasHyde.PluginFramework.Catalogs;

namespace RThomasHyde.PluginFramework.Configuration.Converters
{
    /// <summary>
    /// Converter implementation for the <see cref="FolderPluginCatalog"/>.
    /// </summary>
    public class FolderCatalogConfigurationConverter : IConfigurationToCatalogConverter
    {
        ///<inheritdoc/>
        public bool CanConvert(string type)
        {
            return string.Equals(type, "Folder", StringComparison.InvariantCultureIgnoreCase);
        }

        ///<inheritdoc/>
        public IPluginCatalog Convert(IConfigurationSection configuration)
        {
            var path = configuration.GetValue<string>("Path")
                ?? throw new ArgumentException("Plugin Framework's FolderCatalog requires a Path.");

            var options = new CatalogFolderOptions();
            configuration.Bind($"Options", options);

            var folderOptions = new FolderPluginCatalogOptions();

            folderOptions.IncludeSubfolders = options.IncludeSubfolders ?? folderOptions.IncludeSubfolders;
            folderOptions.SearchPatterns = options.SearchPatterns ?? folderOptions.SearchPatterns;

            return new FolderPluginCatalog(path, folderOptions);
        }

        private class CatalogFolderOptions
        {
            public bool? IncludeSubfolders { get; set; }

            public List<string>? SearchPatterns { get; set; }
        }
    }
}
