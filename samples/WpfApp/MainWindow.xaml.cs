using System;
using System.Collections.ObjectModel;
using System.Windows;
using RThomasHyde.PluginFramework.Samples.Shared;
using RThomasHyde.PluginFramework.Abstractions;
using RThomasHyde.PluginFramework.Catalogs;

namespace WpfApp
{
    public partial class MainWindow : Window
    {
        private readonly ObservableCollection<Plugin> _plugins = new ObservableCollection<Plugin>();
        
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            var folderPluginCatalog = new FolderPluginCatalog(@"..\..\..\..\Shared\RThomasHyde.PluginFramework.Samples.SharedPlugins\bin\debug\netcoreapp3.1", type =>
            {
                type.Implements<IOperator>();
            });
            
            var assemblyPluginCatalog = new AssemblyPluginCatalog(typeof(MainWindow).Assembly, type => typeof(IOperator).IsAssignableFrom(type));
            
            var pluginCatalog = new CompositePluginCatalog(folderPluginCatalog, assemblyPluginCatalog); 
            await pluginCatalog.Initialize();

            var allPlugins = pluginCatalog.GetPlugins();

            foreach (var plugin in allPlugins)
            {
                _plugins.Add(plugin);
            }

            PluginsList.ItemsSource = _plugins;
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            if (PluginsList.SelectedItem == null)
            {
                return;
            }

            var selectedPlugin = (Plugin) PluginsList.SelectedItem;

            var instance = (IOperator) Activator.CreateInstance(selectedPlugin);

            var result = instance.Calculate(20, 10);

            PluginOutput.Text = result.ToString();
        }
    }
}
