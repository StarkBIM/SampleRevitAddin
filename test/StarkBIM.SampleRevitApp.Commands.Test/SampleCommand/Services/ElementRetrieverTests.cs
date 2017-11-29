// <copyright file="ElementRetrieverTests.cs" company="StarkBIM Inc">
// Copyright (c) StarkBIM Inc. All rights reserved.
// </copyright>

namespace StarkBIM.SampleRevitApp.Commands.Test.SampleCommand.Services
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    using JetBrains.Annotations;

    using Moq;

    using StarkBIM.SampleRevitApp.Commands.SampleCmd.Services;
    using StarkBIM.SampleRevitApp.Commands.SampleCmd.Services.Impl;

    using Xunit;

    /// <summary>
    /// Tests for the ElementRetriever class
    /// </summary>
    [SuppressMessage("ReSharper", "AssignNullToNotNullAttribute", Justification = "Test Class")]
    public class ElementRetrieverTests
    {
        [NotNull]
        private readonly Mock<IElementCollector> _mockElementCollector;

        [NotNull]
        private readonly Mock<IRvtClassMapper> _mockClassMapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ElementRetrieverTests"/> class.
        /// </summary>
        public ElementRetrieverTests()
        {
            _mockElementCollector = new Mock<IElementCollector>();

            _mockClassMapper = new Mock<IRvtClassMapper>();
        }

        /// <summary>
        /// Ensures that the constructor throws an ArgumentNullException when passed a null element collector
        /// </summary>
        [Fact]
        public void Constructor_Throws_ArgumentNullException_On_Null_ElementCollector()
        {
            Assert.Throws<ArgumentNullException>(() => new ElementRetriever(null, _mockClassMapper.Object));
        }

        /// <summary>
        /// Ensures that the constructor throws an ArgumentNullException when passed a null class mapper
        /// </summary>
        [Fact]
        public void Constructor_Throws_ArgumentNullException_On_Null_ClassMapper()
        {
            Assert.Throws<ArgumentNullException>(() => new ElementRetriever(_mockElementCollector.Object, null));
        }

        /// <summary>
        /// Ensures that GetSheets throws an ArgumentNullException when passed a null document
        /// </summary>
        [Fact]
        public void GetSheets_Throws_ArgumentNullException_On_Null_Document()
        {
            Assert.Throws<ArgumentNullException>(() => new ElementRetriever(_mockElementCollector.Object, _mockClassMapper.Object).GetSheets(null));
        }

        /// <summary>
        /// Ensures that GetViews throws an ArgumentNullException when passed a null document
        /// </summary>
        [Fact]
        public void GetViews_Throws_ArgumentNullException_On_Null_Document()
        {
            Assert.Throws<ArgumentNullException>(() => new ElementRetriever(_mockElementCollector.Object, _mockClassMapper.Object).GetViews(null));
        }
    }
}