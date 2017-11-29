// <copyright file="ElementCollectorTests.cs" company="StarkBIM Inc">
// Copyright (c) StarkBIM Inc. All rights reserved.
// </copyright>

namespace StarkBIM.SampleRevitApp.Commands.Test.SampleCommand.Services
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    using JetBrains.Annotations;

    using StarkBIM.SampleRevitApp.Commands.SampleCmd.Services.Impl;

    using Xunit;

    /// <summary>
    /// Tests for the ElementCollector class
    /// </summary>
    /// <remarks>
    /// It's too difficult to mock a FilteredElementCollector. Making an abstraction of it is possible but too much work for this sample, and may not be worth it
    /// This is an instance where actual file data will be needed to tests that GetSheets and GetViews are returning the correct data
    /// </remarks>
    [SuppressMessage("ReSharper", "AssignNullToNotNullAttribute", Justification = "Test class")]
    public class ElementCollectorTests : IAssemblyFixture<CommandsFixture>
    {
        [NotNull]
        private readonly ElementCollector _elementCollector = new ElementCollector();

        /// <summary>
        /// Ensures that an ArgumentNullException is thrown when a null document is passed to GetSheets
        /// </summary>
        [Fact]
        public void GetSheets_Throws_ArgumentNullException_On_Empty_Document() => Assert.Throws<ArgumentNullException>(() => _elementCollector.GetSheets(null));

        /// <summary>
        /// Ensures that an ArgumentNullException is thrown when a null document is passed to GetViews
        /// </summary>
        [Fact]
        public void GetViews_Throws_ArgumentNullException_On_Empty_Document() => Assert.Throws<ArgumentNullException>(() => _elementCollector.GetViews(null));
    }
}