// <copyright file="RvtCommandBase.cs" company="StarkBIM Inc">
// Copyright (c) StarkBIM Inc. All rights reserved.
// </copyright>

namespace StarkBIM.SampleRevitApp.Commands.Core
{
    using System;

    using Autodesk.Revit.UI;

    using JetBrains.Annotations;

    /// <summary>
    ///     Base class for IRvtCommand implementations
    /// </summary>
    /// <typeparam name="TCommand">The type of the command</typeparam>
    public abstract class RvtCommandBase<TCommand> : IRvtCommand
        where TCommand : class, IRvtCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RvtCommandBase{TCommand}"/> class.
        /// </summary>
        /// <param name="commandProperties">The command properties</param>
        protected RvtCommandBase([NotNull] IRvtCommandProperties<TCommand> commandProperties)
        {
            if (commandProperties == null)
            {
                throw new ArgumentNullException(nameof(commandProperties));
            }

            Name = commandProperties.Name;
            DisplayName = commandProperties.DisplayName;
        }

        /// <inheritdoc />
        public string Name { get; }

        /// <inheritdoc />
        public string DisplayName { get; }

        /// <inheritdoc />
        public abstract RvtCommandResult Run(ExternalCommandData commandData);
    }
}