// <copyright file="ElementRetrieverTests.cs" company="StarkBIM Inc">
// Copyright (c) StarkBIM Inc. All rights reserved.
// </copyright>

namespace StarkBIM.SampleRevitApp.Commands.JustMock.Test.SampleCommand.Services
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    using Autodesk.Revit.DB;

    using JetBrains.Annotations;

    using Moq;

    using StarkBIM.SampleRevitApp.Commands.SampleCmd.Services;
    using StarkBIM.SampleRevitApp.Commands.SampleCmd.Services.Impl;
    using StarkBIM.SampleRevitApp.Model;

    using Xunit;

    using JustMock = Telerik.JustMock.Mock;

    using RvtView = Autodesk.Revit.DB.View;
    using View = StarkBIM.SampleRevitApp.Model.View;

    /// <summary>
    /// Tests for the ElementRetriever class
    /// </summary>
    /// <remarks>
    /// Tests that do not require JustMock are in the main Commands.Test project
    /// </remarks>
    [SuppressMessage("ReSharper", "AssignNullToNotNullAttribute", Justification = "Test Class")]
    public class ElementRetrieverTests
    {
        [NotNull]
        private readonly Mock<IElementCollector> _mockElementCollector = new Mock<IElementCollector>();

        [NotNull]
        private readonly Mock<IRvtClassMapper> _mockClassMapper = new Mock<IRvtClassMapper>();

        /// <summary>
        /// Ensures that one sheet is returned for every Revit sheet. Mapping is handled by RvtClassMapper so the testing for correct mapping is done there
        /// </summary>
        [Fact]
        public void GetSheets_All_Retrieved_Sheets_Are_Returned()
        {
            var sheetList = new List<ViewSheet>
                {
                    JustMock.Create<ViewSheet>(),
                    JustMock.Create<ViewSheet>(),
                    JustMock.Create<ViewSheet>()
                };

            _mockElementCollector.Setup(coll => coll.GetSheets(It.IsAny<Document>())).Returns(sheetList);

            _mockClassMapper.Setup(map => map.Map<Sheet>(It.IsAny<ViewSheet>())).Returns(Mock.Of<Sheet>());

            var elementRetriever = new ElementRetriever(_mockElementCollector.Object, _mockClassMapper.Object);

            ICollection<Sheet> sheets = elementRetriever.GetSheets(JustMock.Create<Document>());

            Assert.NotNull(sheets);
            Assert.Equal(sheetList.Count, sheets.Count);
        }

        /// <summary>
        /// Ensures that one sheet is returned for every Revit sheet. Mapping is handled by RvtClassMapper so the testing for correct mapping is done there
        /// </summary>
        [Fact]
        public void GetViews_All_Retrieved_Views_Are_Returned()
        {
            var viewList = new List<RvtView>
                {
                    JustMock.Create<RvtView>(),
                    JustMock.Create<RvtView>(),
                    JustMock.Create<RvtView>()
                };

            _mockElementCollector.Setup(coll => coll.GetViews(It.IsAny<Document>())).Returns(viewList);

            _mockClassMapper.Setup(map => map.Map<View>(It.IsAny<RvtView>())).Returns(Mock.Of<View>());

            var elementRetriever = new ElementRetriever(_mockElementCollector.Object, _mockClassMapper.Object);

            ICollection<View> views = elementRetriever.GetViews(JustMock.Create<Document>());

            Assert.NotNull(views);
            Assert.Equal(viewList.Count, views.Count);
        }
    }
}