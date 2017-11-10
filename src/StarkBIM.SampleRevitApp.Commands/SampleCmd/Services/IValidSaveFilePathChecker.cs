// <copyright file="IValidSaveFilePathChecker.cs" company="StarkBIM Inc">
// Copyright (c) StarkBIM Inc. All rights reserved.
// </copyright>

namespace StarkBIM.SampleRevitApp.Commands.SampleCmd.Services
{
    using System;

    using JetBrains.Annotations;

    /// <summary>
    /// Represents a class that checks whether a file path is valid for saving a file
    /// </summary>
    public interface IValidSaveFilePathChecker
    {
        /// <summary>
        /// Checks whether the given path is valid for saving a file, optionally validating it against a predicate
        /// The folder containing the given path must exist. This does not check whether a file is being overwritten
        /// Using this class is not a guarantee the file will save - IO is unpredictable. Wrap all IO calls in a try/catch block
        /// </summary>
        /// <param name="path">The path to check</param>
        /// <param name="validPredicate">The predicate checking whether the path is valid, optionally. Defaults to null</param>
        /// <returns>True if the path is valid, otherwise false</returns>
        bool IsFilePathValid([CanBeNull] string path, [CanBeNull] Predicate<string> validPredicate = null);
    }
}