using Xunit;

namespace RThomasHyde.PluginFramework.Tests
{
    [CollectionDefinition(nameof(NotThreadSafeResourceCollection), DisableParallelization = true)]
    public class NotThreadSafeResourceCollection { }
}
