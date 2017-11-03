// <copyright file="SampleCommand.cs" company="StarkBIM Inc">
// Copyright (c) StarkBIM Inc. All rights reserved.
// </copyright>

namespace StarkBIM.SampleRevitApp.Commands.Sample
{
    using System;

    using Autodesk.Revit.UI;

    using StarkBIM.SampleRevitApp.Commands.Core;
    using StarkBIM.SampleRevitApp.Model.Core;

    /// <summary>
    ///     A sample command implementation.  This command does the following:
    ///     - Reads all views and sheets from the model
    /// </summary>
    public class SampleCommand : RvtCommandBase
    {
        /// <inheritdoc />
        public override string Name { get; } = "Sample";

        /// <inheritdoc />
        public override string DisplayName { get; } = "Sample Command";

        /// <inheritdoc />
        public override IRvtCommandResult Run(ExternalCommandData commandData)
        {
            throw new NotImplementedException();
        }
    }
}