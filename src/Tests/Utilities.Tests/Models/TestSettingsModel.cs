namespace Utilities.Tests.Models
{
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public sealed class TestSettingsModel
    {
        public string AssetsPath { get; set; }

        public string GameName { get; set; }
    }
}
