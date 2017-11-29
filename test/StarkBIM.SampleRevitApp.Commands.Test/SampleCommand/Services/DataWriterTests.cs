// <copyright file="DataWriterTests.cs" company="StarkBIM Inc">
// Copyright (c) StarkBIM Inc. All rights reserved.
// </copyright>

namespace StarkBIM.SampleRevitApp.Commands.Test.SampleCommand.Services
{
    using System;
    using System.Data;
    using System.Diagnostics.CodeAnalysis;

    using JetBrains.Annotations;

    using Moq;

    using StarkBIM.SampleRevitApp.Commands.SampleCmd.Services;
    using StarkBIM.SampleRevitApp.Commands.SampleCmd.Services.Impl;

    using Xunit;

    /// <summary>
    ///     Tests for the DataWriter class
    /// </summary>
    /// <remarks>
    /// We don't test the actual writing of a file as we are relying on a 3rd party library that we can't swap out for testing
    /// This class is the abstraction for that library, cannot abstract further
    /// </remarks>
    [SuppressMessage("ReSharper", "AssignNullToNotNullAttribute", Justification = "Test class")]
    public class DataWriterTests
    {
        [NotNull]
        private readonly Mock<IValidSaveFilePathChecker> _mockPathChecker = new Mock<IValidSaveFilePathChecker>();

        /// <summary>
        ///     Ensures that the constructor throws an ArgumentNullException when passed a null argument
        /// </summary>
        [Fact]
        public void Constructor_Throws_ArgumentNullException_On_Null_Argument() => Assert.Throws<ArgumentNullException>(() => new DataWriter(null));

        /// <summary>
        /// Ensures that WriteDataFile returns false when the datatable has no columns
        /// </summary>
        [Fact]
        public void WriteDataFile_Returns_False_When_DataTable_Has_No_Columns()
        {
            _mockPathChecker.Setup(c => c.IsFilePathValid(It.IsAny<string>(), It.IsAny<Predicate<string>>())).Returns(true);

            var dataWriter = new DataWriter(_mockPathChecker.Object);

            var dataTable = new DataTable();

            Assert.False(dataWriter.WriteDataToFile(dataTable, "A Path"));
        }

        /// <summary>
        /// Ensures that WriteDataFile returns false when the datatable has no rows
        /// </summary>
        [Fact]
        public void WriteDataFile_Returns_False_When_DataTable_Has_No_Rows()
        {
            _mockPathChecker.Setup(c => c.IsFilePathValid(It.IsAny<string>(), It.IsAny<Predicate<string>>())).Returns(true);

            var dataWriter = new DataWriter(_mockPathChecker.Object);

            var dataTable = new DataTable();

            dataTable.Columns.Add("A column");

            Assert.False(dataWriter.WriteDataToFile(dataTable, "A Path"));
        }

        /// <summary>
        /// Ensures that WriteDataFile returns false when the file path checker returns false
        /// </summary>
        [Fact]
        public void WriteDataFile_Returns_False_When_FilePath_Checker_Returns_False()
        {
            _mockPathChecker.Setup(c => c.IsFilePathValid(It.IsAny<string>(), It.IsAny<Predicate<string>>())).Returns(false);

            var dataWriter = new DataWriter(_mockPathChecker.Object);

            Assert.False(dataWriter.WriteDataToFile(new DataTable(), "A Path"));
        }

        /// <summary>
        ///     Ensures that WriteDataFile throws an ArgumentNullException when passed a null datatable
        /// </summary>
        [Fact]
        public void WriteDataFile_Throws_ArgumentNullException_On_Null_DataTable()
        {
            var dataWriter = new DataWriter(_mockPathChecker.Object);

            Assert.Throws<ArgumentNullException>(() => dataWriter.WriteDataToFile(null, "String"));
        }

        /// <summary>
        ///     Ensures that WriteDataFile throws an ArgumentNullException when passed a null path
        /// </summary>
        [Fact]
        public void WriteDataFile_Throws_ArgumentNullException_On_Null_Path()
        {
            var dataWriter = new DataWriter(_mockPathChecker.Object);

            Assert.Throws<ArgumentNullException>(() => dataWriter.WriteDataToFile(new DataTable(), null));
        }
    }
}