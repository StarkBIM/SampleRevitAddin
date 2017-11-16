// <copyright file="RvtCommandPropertiesBase.cs" company="StarkBIM Inc">
// Copyright (c) StarkBIM Inc. All rights reserved.
// </copyright>

namespace StarkBIM.SampleRevitApp.Commands.Core
{
    using System.Windows.Media;

    using Autodesk.Revit.UI;

    /// <summary>
    /// Base class for implementations of IRvtCommandProperties{TCommand}
    /// </summary>
    /// <typeparam name="TCommand">The type of the command</typeparam>
    public abstract class RvtCommandPropertiesBase<TCommand> : IRvtCommandProperties<TCommand>
        where TCommand : class, IRvtCommand
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
    }
}