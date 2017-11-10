// <copyright file="ValidSaveFilePathChecker.cs" company="StarkBIM Inc">
// Copyright (c) StarkBIM Inc. All rights reserved.
// </copyright>

namespace StarkBIM.SampleRevitApp.Commands.SampleCmd.Services.Impl
{
    using System;
    using System.IO;

    using StarkBIM.SampleRevitApp.Helpers;

    /// <inheritdoc />
    public class ValidSaveFilePathChecker : IValidSaveFilePathChecker
    {
        /// <inheritdoc />
        public bool IsFilePathValid(string path, Predicate<string> validPredicate = null)
        {
            if (path.IsNullOrWhiteSpace())
            {
                return false;
            }

            if (validPredicate != null && !validPredicate(path))
            {
                return false;
            }

            string directoryName = Path.GetDirectoryName(path);

            return directoryName != null && Directory.Exists(directoryName);
        }
    }
}