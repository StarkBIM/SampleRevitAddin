// <copyright file="IRvtCommand.cs" company="StarkBIM Inc">
// Copyright (c) StarkBIM Inc. All rights reserved.
// </copyright>

namespace StarkBIM.SampleRevitApp.Commands.Core
{
    using Autodesk.Revit.UI;

    using JetBrains.Annotations;

    /// <summary>
    ///     A command to be executed in Revit
    /// </summary>
    /// <remarks>
    /// The GenericCommand class is responsible for initializing and executing IRvtCommand implementations
    /// </remarks>
    public interface IRvtCommand
    {
        /// <summary>
        ///     Gets the command's internal name. Must be set
        /// </summary>
        /// <value>
        ///     The command's internal name. Must be set
        /// </value>
        [NotNull]
        string Name { get; }

        /// <summary>
        ///     Gets the command's display name, as shown in menus. Must be set
        /// </summary>
        /// <value>
        ///     The command's display name, as shown in menus. Must be set
        /// </value>
        [NotNull]
        string DisplayName { get; }

        /// <summary>
        ///     Runs the command with the given external command data
        /// </summary>
        /// <param name="commandData">The external command data object</param>
        /// <returns>The result of the command</returns>
        [NotNull]
        RvtCommandResult Run([NotNull] ExternalCommandData commandData);
    }
}