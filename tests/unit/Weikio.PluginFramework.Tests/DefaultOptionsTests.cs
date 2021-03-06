﻿using System;
using System.Threading.Tasks;
using RThomasHyde.PluginFramework.Abstractions;
using RThomasHyde.PluginFramework.Catalogs;
using RThomasHyde.PluginFramework.Context;
using RThomasHyde.PluginFramework.TypeFinding;
using Xunit;

namespace RThomasHyde.PluginFramework.Tests
{
    public class DefaultOptionsTests
    {
        [Fact]
        public async Task CanConfigureDefaultOptions()
        {
            // Make sure that the referenced version of JSON.NET is loaded into memory
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(1);
            PluginLoadContextOptions.Defaults.UseHostApplicationAssemblies = UseHostApplicationAssembliesEnum.Always;

            Action<TypeFinderCriteriaBuilder> configureFinder = configure =>
            {
                configure.HasName("*JsonResolver");
            };
            
            var assemblyPluginCatalog = new AssemblyPluginCatalog(@"..\..\..\..\..\Assemblies\bin\JsonNew\netstandard2.0\JsonNetNew.dll", configureFinder);
            var folderPluginCatalog = new FolderPluginCatalog(@"..\..\..\..\..\Assemblies\bin\JsonOld\netstandard2.0", configureFinder);
            
            await assemblyPluginCatalog.Initialize();
            await folderPluginCatalog.Initialize();
            
            var newPlugin = assemblyPluginCatalog.Single();
            var oldPlugin = folderPluginCatalog.Single();
            
            dynamic newPluginJsonResolver = Activator.CreateInstance(newPlugin);
            var newPluginVersion = newPluginJsonResolver.GetVersion();
            
            dynamic oldPluginJsonResolver = Activator.CreateInstance(oldPlugin);
            var oldPluginVersion = oldPluginJsonResolver.GetVersion();
            
            Assert.Equal("12.0.0.0", newPluginVersion);
            Assert.Equal("12.0.0.0", oldPluginVersion);
        }
    }
}
