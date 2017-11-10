// <copyright file="FilePathSelectorTests.cs" company="StarkBIM Inc">
// Copyright (c) StarkBIM Inc. All rights reserved.
// </copyright>

namespace StarkBIM.SampleRevitApp.Commands.Test.SampleCommand.Services
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    using StarkBIM.SampleRevitApp.Commands.SampleCmd.Services.Impl;

    using Xunit;

    /// <summary>
    /// Tests for the FilePathSelector class
    /// </summary>
    [SuppressMessage("ReSharper", "AssignNullToNotNullAttribute", Justification = "Test Class")]
    public class FilePathSelectorTests
    {
        /// <summary>
        /// Ensures that ArgumentNullException is thrown when the constructor is given a null argument
        /// </summary>
        [Fact]
        public void Constructor_Throws_ArgumentNullException_On_Null_Argument()
        {
            Assert.Throws<ArgumentNullException>(() => new FilePathSelector(null));
        }
    }
}