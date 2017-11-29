// <copyright file="DataTableCreatorTests.cs" company="StarkBIM Inc">
// Copyright (c) StarkBIM Inc. All rights reserved.
// </copyright>

namespace StarkBIM.SampleRevitApp.Commands.Test.SampleCommand.Services
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;

    using JetBrains.Annotations;

    using Moq;

    using StarkBIM.SampleRevitApp.Commands.SampleCmd.Services.Impl;
    using StarkBIM.SampleRevitApp.Model;

    using Xunit;

    /// <summary>
    ///     Tests for the DataTableCreator class
    /// </summary>
    [SuppressMessage("ReSharper", "AssignNullToNotNullAttribute", Justification = "Test methods")]
    public class DataTableCreatorTests
    {
        [NotNull]
        private readonly DataTableCreator _dataTableCreator = new DataTableCreator();

        /// <summary>
        ///     Ensures that the returned datatable contains one row per element
        /// </summary>
        [Fact]
        public void CreateDataTable_DataTable_Contains_One_Row_Per_Element()
        {
            var dataTable = new DataTable();

            dataTable.Columns.Add("Name");
            dataTable.Columns.Add("ElementId");
            dataTable.Columns.Add("UniqueId");
            dataTable.Columns.Add("ViewType");
            dataTable.Columns.Add("Number");
            dataTable.Columns.Add("RevisionName");

            var element1 = Mock.Of<Element>();
            element1.Name = "Element1";
            element1.ElementId = 1;
            element1.UniqueId = "1";
            var mockElement1 = Mock.Get(element1);
            SetupMock(mockElement1, dataTable);

            var element2 = Mock.Of<Element>();
            element2.Name = "Element2";
            element2.ElementId = 2;
            element2.UniqueId = "2";
            var mockElement2 = Mock.Get(element2);
            SetupMock(mockElement2, dataTable);

            var view1 = Mock.Of<View>();
            view1.Name = "View1";
            view1.ElementId = 3;
            view1.UniqueId = "3";
            view1.ViewType = "Plan";
            var mockView1 = Mock.Get(view1);
            SetupMock(mockView1, dataTable);

            var view2 = Mock.Of<View>();
            view2.Name = "View2";
            view2.ElementId = 4;
            view2.UniqueId = "4";
            view2.ViewType = "Section";
            var mockView2 = Mock.Get(view2);
            SetupMock(mockView2, dataTable);

            var sheet1 = Mock.Of<Sheet>();
            sheet1.Name = "Sheet1";
            sheet1.ElementId = 5;
            sheet1.UniqueId = "5";
            sheet1.Number = "1";
            sheet1.RevisionName = null;
            var mockSheet1 = Mock.Get(sheet1);
            SetupMock(mockSheet1, dataTable);

            var sheet2 = Mock.Of<Sheet>();
            sheet2.Name = "Sheet2";
            sheet2.ElementId = 6;
            sheet2.UniqueId = "6";
            sheet2.Number = "2";
            sheet2.RevisionName = "Revision1";
            var mockSheet2 = Mock.Get(sheet2);
            SetupMock(mockSheet2, dataTable);

            var elementList = new List<Element>
                {
                    element1,
                    element2,
                    view1,
                    view2,
                    sheet1,
                    sheet2
                };

            DataTable table = _dataTableCreator.CreateDataTable(elementList, dataTable);

            Assert.Equal(table.Rows.Count, elementList.Count);
        }

        /// <summary>
        ///     Ensures that an empty datatable is returned when no elements are passed
        /// </summary>
        [Fact]
        public void CreateDataTable_Returns_Empty_DataTable_For_Empty_Collection()
        {
            var dataTable = _dataTableCreator.CreateDataTable(Enumerable.Empty<Element>());

            Assert.NotNull(dataTable);

            Assert.Empty(dataTable.Rows);
        }

        /// <summary>
        ///     Ensures that ArgumentNullException is thrown when CreateDataTable is passed null
        /// </summary>
        [Fact]
        public void CreateDataTable_Throws_ArgumentNullException_On_Null_Argument() =>
            Assert.Throws<ArgumentNullException>(() => _dataTableCreator.CreateDataTable(null));

        [NotNull]
        private static DataRow CreateRowForElement([NotNull] DataTable dataTable, [NotNull] Element element)
        {
            var row = dataTable.NewRow();

            row["Name"] = element.Name;
            row["ElementId"] = element.ElementId;
            row["UniqueId"] = element.UniqueId;

            switch (element)
            {
                case View view:
                    row["ViewType"] = view.ViewType;
                    break;
                case Sheet sheet:
                    row["Number"] = sheet.Number;
                    row["RevisionName"] = sheet.RevisionName;
                    break;
            }

            return row;
        }

        private static void SetupMock<T>([NotNull] Mock<T> mock, [NotNull] DataTable dataTable)
            where T : Element
        {
            DataRow row = CreateRowForElement(dataTable, mock.Object);

            mock.Setup(el => el.CreateRowForDataTable(dataTable)).Returns(row);
        }
    }
}