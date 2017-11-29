// <copyright file="RvtClassMapperTests.cs" company="StarkBIM Inc">
// Copyright (c) StarkBIM Inc. All rights reserved.
// </copyright>

namespace StarkBIM.SampleRevitApp.Commands.JustMock.Test.SampleCommand.Services
{
    using System.Diagnostics.CodeAnalysis;

    using Autodesk.Revit.DB;

    using JetBrains.Annotations;

    using StarkBIM.SampleRevitApp.Commands.SampleCmd.Services.Impl;
    using StarkBIM.SampleRevitApp.Model;

    using Telerik.JustMock;

    using Xunit;

    /// <summary>
    ///     Tests for the RvtClassMapper class
    ///     The inheritance from IAssemblyFixture sets up resolution for Revit assemblies
    /// </summary>
    /// <remarks>
    ///     Tests that do not require JustMock are in the main Commands.Test project
    /// </remarks>
    [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", Justification = "Test class")]
    public sealed class RvtClassMapperTests : IAssemblyFixture<CommandsFixture>
    {
        [NotNull]
        private readonly RvtClassMapper _mapper = new RvtClassMapper();

        /// <summary>
        ///     Checks that a Revit sheet maps correctly to a Sheet
        /// </summary>
        [Fact]
        public void RvtClassMapper_RvtSheet_Maps_To_Sheet()
        {
            const string SheetName = "SheetName";
            const string SheetNumber = "SheetNumber";
            const string RevisionName = "Revision1";

            var sheet = Mock.Create<ViewSheet>();

            var param = Mock.Create<Parameter>();

            Mock.Arrange(() => param.AsString()).Returns(RevisionName);

            Mock.Arrange(() => sheet.Name).Returns(SheetName);
            Mock.Arrange(() => sheet.SheetNumber).Returns(SheetNumber);

            // Where multiple parameters need to be retrieved from an element, this would need to be handled in more detail here
            Mock.Arrange(() => sheet.get_Parameter(Arg.IsAny<BuiltInParameter>())).Returns(param);

            Sheet mappedSheet = _mapper.Map<Sheet>(sheet);

            Assert.NotNull(mappedSheet);
            Assert.IsType<Sheet>(mappedSheet);

            Assert.Equal(SheetName, mappedSheet.Name);
            Assert.Equal(SheetNumber, mappedSheet.Number);
            Assert.Equal(RevisionName, mappedSheet.RevisionName);
        }
    }
}