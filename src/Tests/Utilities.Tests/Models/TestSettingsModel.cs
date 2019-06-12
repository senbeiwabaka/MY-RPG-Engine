// <copyright file="TestSettingsModel.cs" company="MY Soft Games LLC">
//      Copyright (c) MY Soft Games LLC. All rights reserved.
// </copyright>

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
