// <copyright file="RvtCommandExtensions.cs" company="StarkBIM Inc">
// Copyright (c) StarkBIM Inc. All rights reserved.
// </copyright>

namespace StarkBIM.SampleRevitApp.RvtAddin
{
    using System;
    using System.Windows.Media;

    using Autodesk.Revit.UI;

    using JetBrains.Annotations;

    using StarkBIM.SampleRevitApp.Helpers;
    using StarkBIM.SampleRevitApp.Model.Core;
    using StarkBIM.SampleRevitApp.RvtAddin.Core;

    public static class RvtCommandExtensions
    {
        [NotNull]
        public static PushButtonData CreatePushButtonData<TCommand>(this IRvtCommand command)
            where TCommand : class, IRvtCommand
        {
            var pushButtonData = new PushButtonData(
                                                    command.Name,
                                                    command.DisplayName,
                                                    typeof(GenericCommand<>).Assembly.Location,
                                                    typeof(GenericCommand<TCommand>).FullName)
                .SetAllPushButtonDataProperties(command);

            return pushButtonData;
        }
    }

    public static class RibbonItemDataExtensions
    {
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

            if (command.Image == null)
            {
                return buttonData;
            }

            return buttonData.WithImage(command.Image);
        }

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

        [NotNull]
        public static T WithLargeImage<T, TCommand>(
            [NotNull] this T buttonData,
            [NotNull] TCommand properties)
            where T : ButtonData
            where TCommand : class, IRvtCommand
        {
            if (buttonData == null)
            {
                throw new ArgumentNullException(nameof(buttonData));
            }

            if (properties == null)
            {
                throw new ArgumentNullException(nameof(properties));
            }

            return buttonData.WithLargeImage(properties.LargeImage);
        }

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

        [NotNull]
        public static T WithLongDescription<T, TCommand>([NotNull] this T ribbonItemData, [NotNull] TCommand properties)
            where T : RibbonItemData
            where TCommand : class, IRvtCommand
        {
            if (ribbonItemData == null)
            {
                throw new ArgumentNullException(nameof(ribbonItemData));
            }

            if (properties == null)
            {
                throw new ArgumentNullException(nameof(properties));
            }

            return !properties.LongDescription.IsNullOrWhiteSpace() ? ribbonItemData.WithLongDescription(properties.LongDescription) : ribbonItemData;
        }

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

        [NotNull]
        public static T WithToolTip<T, TCommand>([NotNull] this T ribbonItemData, [NotNull] TCommand properties)
            where T : RibbonItemData
            where TCommand : class, IRvtCommand
        {
            if (ribbonItemData == null)
            {
                throw new ArgumentNullException(nameof(ribbonItemData));
            }

            if (properties == null)
            {
                throw new ArgumentNullException(nameof(properties));
            }

            return !properties.ToolTip.IsNullOrWhiteSpace() ? ribbonItemData.WithToolTip(properties.ToolTip) : ribbonItemData;
        }

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

        [NotNull]
        public static T WithToolTipImage<T, TCommand>(
            [NotNull] this T ribbonItemData,
            [NotNull] TCommand properties)
            where T : RibbonItemData
            where TCommand : class, IRvtCommand
        {
            if (ribbonItemData == null)
            {
                throw new ArgumentNullException(nameof(ribbonItemData));
            }

            if (properties == null)
            {
                throw new ArgumentNullException(nameof(properties));
            }

            return ribbonItemData.WithToolTipImage(properties.ToolTipImage);
        }

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