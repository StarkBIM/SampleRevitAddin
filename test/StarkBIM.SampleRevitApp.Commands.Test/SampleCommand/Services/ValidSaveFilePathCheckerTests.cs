// <copyright file="ValidSaveFilePathCheckerTests.cs" company="StarkBIM Inc">
// Copyright (c) StarkBIM Inc. All rights reserved.
// </copyright>

namespace StarkBIM.SampleRevitApp.Commands.Test.SampleCommand.Services
{
    using System;
    using System.IO;

    using StarkBIM.SampleRevitApp.Commands.SampleCmd.Services.Impl;

    using Xunit;

    /// <summary>
    /// Tests for the ValidSaveFilePathChecker class
    /// </summary>
    public class ValidSaveFilePathCheckerTests
    {
        /// <summary>
        /// Ensures that IsFilePathValid returns false if the containing directory does not exist
        /// </summary>
        [Fact]
        public void IsFilePathValid_Returns_False_If_Containing_Directory_Does_Not_Exist()
        {
            const string NonExistentDirectory = @"C:\ThisDirectoryAlmostCertainlyDoesNotExist";
            Assert.False(Directory.Exists(NonExistentDirectory));

            var testPath = Path.Combine(NonExistentDirectory, "Test.csv");

            var validFilePathChecker = new ValidSaveFilePathChecker();

            Assert.False(validFilePathChecker.IsFilePathValid(testPath));
        }

        /// <summary>
        /// Ensures that IsFilePathValid returns false if null or whitespace is passed
        /// </summary>
        [Fact]
        public void IsFilePathValid_Returns_False_If_NullOrWhiteSpace()
        {
            var validFilePathChecker = new ValidSaveFilePathChecker();

            Assert.False(validFilePathChecker.IsFilePathValid(null));
            Assert.False(validFilePathChecker.IsFilePathValid(string.Empty));
            Assert.False(validFilePathChecker.IsFilePathValid(" "));
        }

        /// <summary>
        /// Ensures that IsFilePathValid returns false if the path does not pass the predicate
        /// </summary>
        [Fact]
        public void IsFilePathValid_Returns_False_If_Predicate_Fails()
        {
            // This should always exist
            var appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            Assert.True(Directory.Exists(appDataPath));

            var testPath = Path.Combine(appDataPath, "Test.txt");

            var validFilePathChecker = new ValidSaveFilePathChecker();

            Assert.False(validFilePathChecker.IsFilePathValid(testPath, p => p.EndsWith(".csv", StringComparison.OrdinalIgnoreCase)));
        }

        /// <summary>
        /// Ensures that IsFilePathValid returns true if the predicate passes and the directory containing the file exists
        /// </summary>
        [Fact]
        public void IsFilePathValid_Returns_True_If_Predicate_Passes_And_Directory_Exists()
        {
            // This should always exist
            var appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            Assert.True(Directory.Exists(appDataPath));

            var testPath = Path.Combine(appDataPath, "Test.csv");

            var validFilePathChecker = new ValidSaveFilePathChecker();

            Assert.True(validFilePathChecker.IsFilePathValid(testPath, p => p.EndsWith(".csv", StringComparison.OrdinalIgnoreCase)));
        }

        /// <summary>
        /// Ensures that IsFilePathValid returns true when the directory exists and there is no predicate
        /// </summary>
        [Fact]
        public void IsFilePathValid_Returns_True_If_Containing_Directory_Exists()
        {
            var appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            Assert.True(Directory.Exists(appDataPath));

            var testPath = Path.Combine(appDataPath, "Test.csv");

            var validFilePathChecker = new ValidSaveFilePathChecker();

            Assert.True(validFilePathChecker.IsFilePathValid(testPath));
        }
    }
}