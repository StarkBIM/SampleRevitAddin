// <copyright file="DialogService.cs" company="StarkBIM Inc">
// Copyright (c) StarkBIM Inc. All rights reserved.
// </copyright>

namespace StarkBIM.SampleRevitApp.Commands.SampleCmd.Services.Impl
{
    using System;

    using Autodesk.Revit.UI;

    /// <inheritdoc />
    public class DialogService : IDialogService
    {
        /// <inheritdoc />
        public void ShowDialog(string title, string message)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(title));
            }

            if (string.IsNullOrWhiteSpace(message))
            {
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(message));
            }

            TaskDialog.Show(title, message);
        }
    }
}
