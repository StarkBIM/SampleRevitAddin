// <copyright file="IRvtCommandProperties.cs" company="StarkBIM Inc">
// Copyright (c) StarkBIM Inc. All rights reserved.
// </copyright>

namespace StarkBIM.SampleRevitApp.Commands.Core
{
    using System.Diagnostics.CodeAnalysis;
    using System.Windows.Media;

    using Autodesk.Revit.UI;

    using JetBrains.Annotations;

    /// <summary>
    /// Represents the set of properties for a class implementing IRvtCommand
    /// </summary>
    /// <typeparam name="TCommand">The type of the command. Must implement IRvtCommand and be a class</typeparam>
    [SuppressMessage("ReSharper", "UnusedTypeParameter", Justification = "Required for DI")]
    public interface IRvtCommandProperties<TCommand>
        where TCommand : class, IRvtCommand
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
        ///     Gets the command's long description. Can be null
        /// </summary>
        /// <value>
        ///     The command's long description. Can be null
        /// </value>
        [CanBeNull]
        string LongDescription { get; }

        /// <summary>
        ///     Gets the command's availability. This should be set when the class is initialized. Can be null to use the default
        ///     availability
        /// </summary>
        /// <value>
        ///     The command's availability. This should be set when the class is initialized. Can be null to use the default
        ///     availability
        /// </value>
        [CanBeNull]
        IExternalCommandAvailability CommandAvailability { get; }

        /// <summary>
        ///     Gets the menu small image (16x16). Can be null
        /// </summary>
        /// <value>
        ///     The menu small image (16x16). Can be null
        /// </value>
        [CanBeNull]
        ImageSource Image { get; }

        /// <summary>
        ///     Gets the menu large image (32x32). Can be null
        /// </summary>
        /// <value>
        ///     The menu large image (32x32). Can be null
        /// </value>
        [CanBeNull]
        ImageSource LargeImage { get; }

        /// <summary>
        ///     Gets the tooltip string. Can be null
        /// </summary>
        /// <value>
        ///     The tooltip string. Can be null
        /// </value>
        [CanBeNull]
        string ToolTip { get; }

        /// <summary>
        ///     Gets the tooltip image. Can be null
        /// </summary>
        /// <value>
        ///     The tooltip image. Can be null
        /// </value>
        [CanBeNull]
        ImageSource ToolTipImage { get; }

        /// <summary>
        ///     Gets the contextual help for the command. Can be null
        /// </summary>
        /// <value>
        ///     The contextual help for the command. Can be null
        /// </value>
        [CanBeNull]
        ContextualHelp ContextualHelp { get; }
    }
}