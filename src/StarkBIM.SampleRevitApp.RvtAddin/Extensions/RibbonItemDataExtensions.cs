// <copyright file="RibbonItemDataExtensions.cs" company="StarkBIM Inc">
// Copyright (c) StarkBIM Inc. All rights reserved.
// </copyright>

namespace StarkBIM.SampleRevitApp.RvtAddin.Extensions
{
    using System;
    using System.Windows.Media;

    using Autodesk.Revit.UI;

    using JetBrains.Annotations;

    using StarkBIM.SampleRevitApp.Commands.Core;
    using StarkBIM.SampleRevitApp.Helpers;

    /// <summary>
    /// Extension methods to add data to ribbon items for Revit's menu
    /// </summary>
    public static class RibbonItemDataExtensions
    {
        /// <summary>
        /// Adds all properties to a ButtonData instance from an IRvtCommand instance
        /// </summary>
        /// <typeparam name="T">The type of the button data</typeparam>
        /// <typeparam name="TCommand">The type of the command</typeparam>
        /// <param name="buttonData">The button data instance</param>
        /// <param name="command">The command instance</param>
        /// <returns>The button data with properties added</returns>
        [NotNull]
        public static T SetAllButtonProperties<T, TCommand>(
            [NotNull] this T buttonData,
            [NotNull] TCommand command)
            where T : ButtonData
            where TCommand : class, IRvtCommand
        {
            if (buttonData == null)
            {
                throw new ArgumentNullException(nameof(buttonData));
            }

            if (command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            return buttonData.SetAllRibbonItemProperties(command).WithImage(command).WithLargeImage(command);
        }

        /// <summary>
        /// Adds all properties to a PushButtonData instance from an IRvtCommand instance
        /// </summary>
        /// <typeparam name="T">The type of the button data</typeparam>
        /// <typeparam name="TCommand">The type of the command</typeparam>
        /// <param name="buttonData">The button data instance</param>
        /// <param name="command">The command instance</param>
        /// <returns>The button data with properties added</returns>
        [NotNull]
        public static T SetAllPushButtonDataProperties<T, TCommand>(
            [NotNull] this T buttonData,
            [NotNull] TCommand command)
            where T : PushButtonData
            where TCommand : class, IRvtCommand
        {
            if (buttonData == null)
            {
                throw new ArgumentNullException(nameof(buttonData));
            }

            if (command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            return buttonData.SetAllButtonProperties(command).WithAvailabilityClass(command);
        }

        /// <summary>
        /// Adds all properties to a RibbonItemData instance from an IRvtCommand instance
        /// </summary>
        /// <typeparam name="T">The type of the button data</typeparam>
        /// <typeparam name="TCommand">The type of the command</typeparam>
        /// <param name="ribbonItemData">The ribbon item data instance</param>
        /// <param name="command">The command instance</param>
        /// <returns>The button data with properties added</returns>
        [NotNull]
        public static T SetAllRibbonItemProperties<T, TCommand>([NotNull] this T ribbonItemData, [NotNull] TCommand command)
            where T : RibbonItemData
            where TCommand : class, IRvtCommand
        {
            if (ribbonItemData == null)
            {
                throw new ArgumentNullException(nameof(ribbonItemData));
            }

            if (command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            return ribbonItemData.WithToolTip(command).WithToolTipImage(command).WithContextualHelp(command)
                .WithLongDescription(command);
        }

        /// <summary>
        /// Adds the availability class information to a button from a command
        /// </summary>
        /// <typeparam name="T">The type of the button data</typeparam>
        /// <typeparam name="TCommand">The type of the command</typeparam>
        /// <param name="buttonData">The button data instance</param>
        /// <param name="command">The command instance</param>
        /// <returns>The button data with the availability class name added</returns>
        [NotNull]
        public static T WithAvailabilityClass<T, TCommand>(
            [NotNull] this T buttonData,
            [NotNull] TCommand command)
            where T : PushButtonData
            where TCommand : class, IRvtCommand
        {
            if (buttonData == null)
            {
                throw new ArgumentNullException(nameof(buttonData));
            }

            if (command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            if (command.CommandAvailability == null)
            {
                return buttonData;
            }

            string buttonDataAvailabilityClassName = command.CommandAvailability.GetType().FullName;

            return buttonData.WithAvailabilityClass(buttonDataAvailabilityClassName);
        }

        /// <summary>
        /// Adds the availability class name to a command
        /// </summary>
        /// <typeparam name="T">The type of the button data</typeparam>
        /// <param name="buttonData">The button data instance</param>
        /// <param name="availabilityClassName">The availability class name</param>
        /// <returns>The button data with the availability class name added</returns>
        [NotNull]
        public static T WithAvailabilityClass<T>(
            [NotNull] this T buttonData,
            [CanBeNull] string availabilityClassName)
            where T : PushButtonData
        {
            if (buttonData == null)
            {
                throw new ArgumentNullException(nameof(buttonData));
            }

            buttonData.AvailabilityClassName = availabilityClassName;

            return buttonData;
        }

        /// <summary>
        /// Adds the contextual help from the given command to the ribbon item data
        /// </summary>
        /// <typeparam name="T">The type of the button data</typeparam>
        /// <typeparam name="TCommand">The type of the command</typeparam>
        /// <param name="ribbonItemData">The ribbon item data instance</param>
        /// <param name="command">The command instance</param>
        /// <returns>The button data with the contextual help added</returns>
        [NotNull]
        public static T WithContextualHelp<T, TCommand>([NotNull] this T ribbonItemData, [NotNull] TCommand command)
            where T : RibbonItemData
            where TCommand : class, IRvtCommand
        {
            if (ribbonItemData == null)
            {
                throw new ArgumentNullException(nameof(ribbonItemData));
            }

            if (command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            return command.ContextualHelp != null ? ribbonItemData.WithContextualHelp(command.ContextualHelp) : ribbonItemData;
        }

        /// <summary>
        /// Adds the given contextual help to the ribbon item data
        /// </summary>
        /// <typeparam name="T">The type of the button data</typeparam>
        /// <param name="ribbonItemData">The ribbon item data instance</param>
        /// <param name="contextualHelp">The contextual help</param>
        /// <returns>The button data with the contextual help added</returns>
        [NotNull]
        public static T WithContextualHelp<T>([NotNull] this T ribbonItemData, [NotNull] ContextualHelp contextualHelp)
            where T : RibbonItemData
        {
            if (ribbonItemData == null)
            {
                throw new ArgumentNullException(nameof(ribbonItemData));
            }

            if (contextualHelp == null)
            {
                throw new ArgumentNullException(nameof(contextualHelp));
            }

            ribbonItemData.SetContextualHelp(contextualHelp);

            return ribbonItemData;
        }

        /// <summary>
        /// Adds the image from the given command to the button data
        /// </summary>
        /// <typeparam name="T">The type of the button data</typeparam>
        /// <typeparam name="TCommand">The type of the command</typeparam>
        /// <param name="buttonData">The button data instance</param>
        /// <param name="command">The command instance</param>
        /// <returns>The button data with the image added</returns>
        [NotNull]
        public static T WithImage<T, TCommand>(
            [NotNull] this T buttonData,
            [NotNull] TCommand command)
            where T : ButtonData
            where TCommand : class, IRvtCommand
        {
            if (buttonData == null)
            {
                throw new ArgumentNullException(nameof(buttonData));
            }

            if (command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            return command.Image == null ? buttonData : buttonData.WithImage(command.Image);
        }

        /// <summary>
        /// Adds the given image to the button data
        /// </summary>
        /// <typeparam name="T">The type of the button data</typeparam>
        /// <param name="buttonData">The button data instance</param>
        /// <param name="imageSource">The image</param>
        /// <returns>The button data with the image added</returns>
        [NotNull]
        public static T WithImage<T>(
            [NotNull] this T buttonData,
            [CanBeNull] ImageSource imageSource)
            where T : ButtonData
        {
            if (buttonData == null)
            {
                throw new ArgumentNullException(nameof(buttonData));
            }

            buttonData.Image = imageSource;

            return buttonData;
        }

        /// <summary>
        /// Adds the large image from the given command to the button data
        /// </summary>
        /// <typeparam name="T">The type of the button data</typeparam>
        /// <typeparam name="TCommand">The type of the command</typeparam>
        /// <param name="buttonData">The button data instance</param>
        /// <param name="command">The command instance</param>
        /// <returns>The button data with the large image added</returns>
        [NotNull]
        public static T WithLargeImage<T, TCommand>(
            [NotNull] this T buttonData,
            [NotNull] TCommand command)
            where T : ButtonData
            where TCommand : class, IRvtCommand
        {
            if (buttonData == null)
            {
                throw new ArgumentNullException(nameof(buttonData));
            }

            if (command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            return buttonData.WithLargeImage(command.LargeImage);
        }

        /// <summary>
        /// Adds the given large image to the button data
        /// </summary>
        /// <typeparam name="T">The type of the button data</typeparam>
        /// <param name="buttonData">The button data instance</param>
        /// <param name="imageSource">The large image</param>
        /// <returns>The button data with the large image added</returns>
        [NotNull]
        public static T WithLargeImage<T>(
            [NotNull] this T buttonData,
            [CanBeNull] ImageSource imageSource)
            where T : ButtonData
        {
            if (buttonData == null)
            {
                throw new ArgumentNullException(nameof(buttonData));
            }

            buttonData.LargeImage = imageSource;

            return buttonData;
        }

        /// <summary>
        /// Adds the long description from the given command to the ribbon item data
        /// </summary>
        /// <typeparam name="T">The type of the button data</typeparam>
        /// <typeparam name="TCommand">The type of the command</typeparam>
        /// <param name="ribbonItemData">The ribbon item data instance</param>
        /// <param name="command">The command instance</param>
        /// <returns>The button data with the long description added</returns>
        [NotNull]
        public static T WithLongDescription<T, TCommand>([NotNull] this T ribbonItemData, [NotNull] TCommand command)
            where T : RibbonItemData
            where TCommand : class, IRvtCommand
        {
            if (ribbonItemData == null)
            {
                throw new ArgumentNullException(nameof(ribbonItemData));
            }

            if (command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            return !command.LongDescription.IsNullOrWhiteSpace() ? ribbonItemData.WithLongDescription(command.LongDescription) : ribbonItemData;
        }

        /// <summary>
        /// Adds the given long description to the ribbon item data
        /// </summary>
        /// <typeparam name="T">The type of the button data</typeparam>
        /// <param name="ribbonItemData">The ribbon item data instance</param>
        /// <param name="longDescription">The long description</param>
        /// <returns>The button data with the long description added</returns>
        [NotNull]
        public static T WithLongDescription<T>([NotNull] this T ribbonItemData, [CanBeNull] string longDescription)
            where T : RibbonItemData
        {
            if (ribbonItemData == null)
            {
                throw new ArgumentNullException(nameof(ribbonItemData));
            }

            ribbonItemData.LongDescription = longDescription;

            return ribbonItemData;
        }

        /// <summary>
        /// Adds the tooltip from the given command to the ribbon item data
        /// </summary>
        /// <typeparam name="T">The type of the button data</typeparam>
        /// <typeparam name="TCommand">The type of the command</typeparam>
        /// <param name="ribbonItemData">The ribbon item data instance</param>
        /// <param name="command">The command instance</param>
        /// <returns>The button data with the tooltip added</returns>
        [NotNull]
        public static T WithToolTip<T, TCommand>([NotNull] this T ribbonItemData, [NotNull] TCommand command)
            where T : RibbonItemData
            where TCommand : class, IRvtCommand
        {
            if (ribbonItemData == null)
            {
                throw new ArgumentNullException(nameof(ribbonItemData));
            }

            if (command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            return !command.ToolTip.IsNullOrWhiteSpace() ? ribbonItemData.WithToolTip(command.ToolTip) : ribbonItemData;
        }

        /// <summary>
        /// Adds the given tooltip to the ribbon item data
        /// </summary>
        /// <typeparam name="T">The type of the button data</typeparam>
        /// <param name="ribbonItemData">The ribbon item data instance</param>
        /// <param name="toolTip">The tooltip</param>
        /// <returns>The button data with the tooltip added</returns>
        [NotNull]
        public static T WithToolTip<T>([NotNull] this T ribbonItemData, [CanBeNull] string toolTip)
            where T : RibbonItemData
        {
            if (ribbonItemData == null)
            {
                throw new ArgumentNullException(nameof(ribbonItemData));
            }

            ribbonItemData.ToolTip = toolTip;

            return ribbonItemData;
        }

        /// <summary>
        /// Adds the tooltip image from the given command to the ribbon item data
        /// </summary>
        /// <typeparam name="T">The type of the button data</typeparam>
        /// <typeparam name="TCommand">The type of the command</typeparam>
        /// <param name="ribbonItemData">The ribbon item data instance</param>
        /// <param name="command">The command instance</param>
        /// <returns>The button data with the tool tip image added</returns>
        [NotNull]
        public static T WithToolTipImage<T, TCommand>(
            [NotNull] this T ribbonItemData,
            [NotNull] TCommand command)
            where T : RibbonItemData
            where TCommand : class, IRvtCommand
        {
            if (ribbonItemData == null)
            {
                throw new ArgumentNullException(nameof(ribbonItemData));
            }

            if (command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            return ribbonItemData.WithToolTipImage(command.ToolTipImage);
        }

        /// <summary>
        /// Adds the given tooltip image to the ribbon item data
        /// </summary>
        /// <typeparam name="T">The type of the button data</typeparam>
        /// <param name="ribbonItemData">The ribbon item data instance</param>
        /// <param name="imageSource">The image</param>
        /// <returns>The button data with the tool tip image added</returns>
        [NotNull]
        public static T WithToolTipImage<T>(
            [NotNull] this T ribbonItemData,
            [CanBeNull] ImageSource imageSource)
            where T : RibbonItemData
        {
            if (ribbonItemData == null)
            {
                throw new ArgumentNullException(nameof(ribbonItemData));
            }

            ribbonItemData.ToolTipImage = imageSource;

            return ribbonItemData;
        }
    }
}