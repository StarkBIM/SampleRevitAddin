// <copyright file="RvtCommandExtensions.cs" company="StarkBIM Inc">
// Copyright (c) StarkBIM Inc. All rights reserved.
// </copyright>

namespace StarkBIM.SampleRevitApp.RvtAddin.Extensions
{
    using System;

    using Autodesk.Revit.UI;

    using JetBrains.Annotations;

    using StarkBIM.SampleRevitApp.Commands.Core;
    using StarkBIM.SampleRevitApp.RvtAddin.Core;

    /// <summary>
    /// Extension methods for RvtCommands
    /// </summary>
    public static class RvtCommandExtensions
    {
        /// <summary>
        /// Creates a pushbutton for an RvtCommand
        /// </summary>
        /// <typeparam name="TCommand">The type of the command</typeparam>
        /// <param name="command">The command</param>
        /// <returns>A PushbuttonData object for the command</returns>
        [NotNull]
        public static PushButtonData CreatePushButtonData<TCommand>([NotNull] this IRvtCommand command)
            where TCommand : class, IRvtCommand
        {
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            var pushButtonData = new PushButtonData(
                                                    command.Name,
                                                    command.DisplayName,
                                                    typeof(GenericCommand<>).Assembly.Location,
                                                    typeof(GenericCommand<TCommand>).FullName)
                .SetAllPushButtonDataProperties(command);

            return pushButtonData;
        }
    }
}