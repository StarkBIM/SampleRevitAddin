// <copyright file="SheetTests.cs" company="StarkBIM Inc">
// Copyright (c) StarkBIM Inc. All rights reserved.
// </copyright>

namespace StarkBIM.SampleRevitApp.Model.Test
{
    using System;
    using System.Data;
    using System.Diagnostics.CodeAnalysis;

    using JetBrains.Annotations;

    using Xunit;

    /// <summary>
    /// Tests for the sheet class
    /// </summary>
    [SuppressMessage("ReSharper", "AssignNullToNotNullAttribute", Justification = "Test methods")]
    public class SheetTests
    {
        [NotNull]
        private readonly Sheet _sheet;

        /// <summary>
        /// Initializes a new instance of the <see cref="SheetTests"/> class.
        /// </summary>
        public SheetTests()
        {
            _sheet = new Sheet
                {
                    Name = "Sheet1",
                    ElementId = 1,
                    UniqueId = "1",
                    Number = "1",
                    RevisionName = "Revision1"
                };
        }

        /// <summary>
        /// Ensures that ArgumentNullException is thrown when GetRowForDataTable is passed a null DataTable
        /// </summary>
        [Fact]
        public void CreateRowForDataTable_Throws_ArgumentNullException_On_Null_DataTable()
        {
            Assert.Throws<ArgumentNullException>(() => _sheet.CreateRowForDataTable(null));
        }

        /// <summary>
        /// Ensure that the columns on the datatable are correctly created
        /// </summary>
        [Fact]
        public void CreateRowForDataTable_Columns_Created_Correctly()
        {
            var dataTable = new DataTable();

            _sheet.CreateRowForDataTable(dataTable);

            Assert.True(dataTable.Columns.Contains(nameof(Sheet.Name)));
            Assert.True(dataTable.Columns.Contains(nameof(Sheet.UniqueId)));
            Assert.True(dataTable.Columns.Contains(nameof(Sheet.ElementId)));
            Assert.True(dataTable.Columns.Contains(nameof(Sheet.Number)));
            Assert.True(dataTable.Columns.Contains(nameof(Sheet.RevisionName)));

            Assert.Equal(5, dataTable.Columns.Count);
        }

        /// <summary>
        /// Ensure that the data in the row is correct
        /// </summary>
        [Fact]
        public void CreateRowForDataTable_Row_Is_Created_Correctly()
        {
            var dataTable = new DataTable();

            var row = _sheet.CreateRowForDataTable(dataTable);

            Assert.NotNull(row);

            Assert.Equal(_sheet.Name, row[nameof(Sheet.Name)]);
            Assert.Equal(_sheet.UniqueId, row[nameof(Sheet.UniqueId)]);
            Assert.Equal($"{_sheet.ElementId}", row[nameof(Sheet.ElementId)]);
            Assert.Equal(_sheet.Number, row[nameof(Sheet.Number)]);
            Assert.Equal(_sheet.RevisionName, row[nameof(Sheet.RevisionName)]);
        }
    }
}