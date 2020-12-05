using System;
using System.ComponentModel;
using System.Windows.Forms;
using RThomasHyde.PluginFramework.Samples.Shared;

namespace WinFormsPluginsLibrary
{
    [DisplayName("Hello World")]
    public partial class TestPlugin : Form, IDialog
    {
        public TestPlugin()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Hello Plugin Framework");
        }
    }
}

