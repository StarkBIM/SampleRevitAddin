// <copyright file="ElementTests.cs" company="StarkBIM Inc">
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
    /// Tests for the element class
    /// </summary>
    [SuppressMessage("ReSharper", "AssignNullToNotNullAttribute", Justification = "Test methods")]
    public class ElementTests
    {
        [NotNull]
        private readonly Element _element = new Element
        {
            Name = "Element1",
            ElementId = 1,
            UniqueId = "1"
        };

        /// <summary>
        /// Ensures that ArgumentNullException is thrown when GetRowForDataTable is passed a null DataTable
        /// </summary>
        [Fact]
        public void CreateRowForDataTable_Throws_ArgumentNullException_On_Null_DataTable()
        {
            Assert.Throws<ArgumentNullException>(() => _element.CreateRowForDataTable(null));
        }

        /// <summary>
        /// Ensure that the columns on the datatable are correctly created
        /// </summary>
        [Fact]
        public void CreateRowForDataTable_Columns_Created_Correctly()
        {
            var dataTable = new DataTable();

            _element.CreateRowForDataTable(dataTable);

            Assert.True(dataTable.Columns.Contains(nameof(Element.Name)));
            Assert.True(dataTable.Columns.Contains(nameof(Element.UniqueId)));
            Assert.True(dataTable.Columns.Contains(nameof(Element.ElementId)));

            Assert.Equal(3, dataTable.Columns.Count);
        }

        /// <summary>
        /// Ensure that the data in the row is correct
        /// </summary>
        [Fact]
        public void CreateRowForDataTable_Row_Is_Created_Correctly()
        {
            var dataTable = new DataTable();

            var row = _element.CreateRowForDataTable(dataTable);

            Assert.NotNull(row);

            Assert.Equal(_element.Name, row[nameof(Element.Name)]);
            Assert.Equal(_element.UniqueId, row[nameof(Element.UniqueId)]);
            Assert.Equal($"{_element.ElementId}", row[nameof(Element.ElementId)]);
        }
    }
}
