// <copyright file="SampleCommandProperties.cs" company="StarkBIM Inc">
// Copyright (c) StarkBIM Inc. All rights reserved.
// </copyright>

namespace StarkBIM.SampleRevitApp.Commands.SampleCmd
{
    using StarkBIM.SampleRevitApp.Commands.Core;

    /// <summary>
    /// Command Properties for the SampleCommand command
    /// </summary>
    public class SampleCommandProperties : RvtCommandPropertiesBase<SampleCommand>
    {
        /// <inheritdoc />
        public override string Name { get; } = "Sample";

        /// <inheritdoc />
        public override string DisplayName { get; } = "Sample Command";
    }
}