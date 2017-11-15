// <copyright file="IDialogService.cs" company="StarkBIM Inc">
// Copyright (c) StarkBIM Inc. All rights reserved.
// </copyright>

namespace StarkBIM.SampleRevitApp.Commands.SampleCmd.Services
{
    using JetBrains.Annotations;

    /// <summary>
    /// Represents a service that display dialog boxes
    /// </summary>
    public interface IDialogService
    {
        /// <summary>
        /// Shows a dialog box with the given title and message
        /// </summary>
        /// <param name="title">The title. Must not be null, empty or whitespace</param>
        /// <param name="message">The message. Must not be null, empty or whitespace</param>
        void ShowDialog([NotNull] string title, [NotNull] string message);
    }
}
