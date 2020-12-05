using System;
using RThomasHyde.PluginFramework.Samples.Shared;

namespace ConsoleApp
{
    public class FirstPlugin : IPlugin
    {
        public void Run()
        {
            Console.WriteLine("First plugin");
        }
    }
}