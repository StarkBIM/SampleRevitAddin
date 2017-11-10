// <copyright file="IFilePathSelector.cs" company="StarkBIM Inc">
// Copyright (c) StarkBIM Inc. All rights reserved.
// </copyright>

namespace StarkBIM.SampleRevitApp.Commands.SampleCmd.Services
{
    using JetBrains.Annotations;

    /// <summary>
    /// Represents a service in which a user will pick a file path at which a file will be saved
    /// </summary>
    public interface IFilePathSelector
    {
        /// <summary>
        /// Selects a file path. If the user cancels the operation, null will be returned
        /// </summary>
        /// <returns>The file path, or null if the operation is cancelled</returns>
        [CanBeNull]
        string SelectFilePath();
    }
}