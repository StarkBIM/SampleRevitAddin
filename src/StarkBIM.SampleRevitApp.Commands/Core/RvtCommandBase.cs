// <copyright file="RvtCommandBase.cs" company="StarkBIM Inc">
// Copyright (c) StarkBIM Inc. All rights reserved.
// </copyright>

namespace StarkBIM.SampleRevitApp.Commands.Core
{
    using System.Windows.Media;

    using Autodesk.Revit.UI;

    using StarkBIM.SampleRevitApp.Model.Core;

    /// <summary>
    ///     Base class for IRvtCommand implementations
    /// </summary>
    public abstract class RvtCommandBase : IRvtCommand
    {
        /// <inheritdoc />
        public abstract string Name { get; }

        /// <inheritdoc />
        public abstract string DisplayName { get; }

        /// <inheritdoc />
        public virtual string LongDescription { get; } = null;

        /// <inheritdoc />
        public virtual IExternalCommandAvailability CommandAvailability { get; } = null;

        /// <inheritdoc />
        public virtual ImageSource Image { get; } = null;

        /// <inheritdoc />
        public virtual ImageSource LargeImage { get; } = null;

        /// <inheritdoc />
        public virtual string ToolTip { get; } = null;

        /// <inheritdoc />
        public virtual ImageSource ToolTipImage { get; } = null;

        /// <inheritdoc />
        public virtual ContextualHelp ContextualHelp { get; } = null;

        /// <inheritdoc />
        public abstract IRvtCommandResult Run(ExternalCommandData commandData);
    }
}