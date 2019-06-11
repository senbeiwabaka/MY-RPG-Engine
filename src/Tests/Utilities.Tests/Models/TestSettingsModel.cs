using System.Diagnostics.CodeAnalysis;

namespace Utilities.Tests.Models
{
    [ExcludeFromCodeCoverage]
    public sealed class TestSettingsModel
    {
        public string AssetsPath { get; set; }

        public string GameName { get; set; }
    }
}
