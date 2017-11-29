// <copyright file="ViewTests.cs" company="StarkBIM Inc">
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
    ///     Tests for the view class
    /// </summary>
    [SuppressMessage("ReSharper", "AssignNullToNotNullAttribute", Justification = "Test methods")]
    public class ViewTests
    {
        [NotNull]
        private readonly View _view = new View
        {
            Name = "View1",
            ElementId = 1,
            UniqueId = "1",
            ViewType = "ViewType1"
        };

        /// <summary>
        ///     Ensure that the columns on the datatable are correctly created
        /// </summary>
        [Fact]
        public void CreateRowForDataTable_Columns_Created_Correctly()
        {
            var dataTable = new DataTable();

            _view.CreateRowForDataTable(dataTable);

            Assert.True(dataTable.Columns.Contains(nameof(View.Name)));
            Assert.True(dataTable.Columns.Contains(nameof(View.UniqueId)));
            Assert.True(dataTable.Columns.Contains(nameof(View.ElementId)));
            Assert.True(dataTable.Columns.Contains(nameof(View.ViewType)));

            Assert.Equal(4, dataTable.Columns.Count);
        }

        /// <summary>
        ///     Ensure that the data in the row is correct
        /// </summary>
        [Fact]
        public void CreateRowForDataTable_Row_Is_Created_Correctly()
        {
            var dataTable = new DataTable();

            var row = _view.CreateRowForDataTable(dataTable);

            Assert.NotNull(row);

            Assert.Equal(_view.Name, row[nameof(View.Name)]);
            Assert.Equal(_view.UniqueId, row[nameof(View.UniqueId)]);
            Assert.Equal($"{_view.ElementId}", row[nameof(View.ElementId)]);
            Assert.Equal(_view.ViewType, row[nameof(View.ViewType)]);
        }

        /// <summary>
        ///     Ensures that ArgumentNullException is thrown when GetRowForDataTable is passed a null DataTable
        /// </summary>
        [Fact]
        public void CreateRowForDataTable_Throws_ArgumentNullException_On_Null_DataTable() =>
            Assert.Throws<ArgumentNullException>(() => _view.CreateRowForDataTable(null));
    }
}