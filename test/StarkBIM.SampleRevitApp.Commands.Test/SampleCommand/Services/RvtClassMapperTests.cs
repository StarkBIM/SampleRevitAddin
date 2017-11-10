// <copyright file="RvtClassMapperTests.cs" company="StarkBIM Inc">
// Copyright (c) StarkBIM Inc. All rights reserved.
// </copyright>

namespace StarkBIM.SampleRevitApp.Commands.Test.SampleCommand.Services
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    using Autodesk.Revit.DB;

    using JetBrains.Annotations;

    using StarkBIM.SampleRevitApp.Commands.SampleCmd.Services.Impl;
    using StarkBIM.SampleRevitApp.Model;

    using Telerik.JustMock;

    using Xunit;

    using Element = StarkBIM.SampleRevitApp.Model.Element;
    using RvtElement = Autodesk.Revit.DB.Element;
    using RvtView = Autodesk.Revit.DB.View;
    using View = StarkBIM.SampleRevitApp.Model.View;

    /// <summary>
    ///     Tests for the RvtClassMapper class
    ///     The inheritance from IAssemblyFixture sets up resolution for Revit assemblies
    /// </summary>
    [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", Justification = "Test class")]
    public sealed class RvtClassMapperTests : IAssemblyFixture<CommandsFixture>
    {
        [NotNull]
        private readonly RvtClassMapper _mapper;

        /// <summary>
        ///     Initializes a new instance of the <see cref="RvtClassMapperTests" /> class.
        /// </summary>
        public RvtClassMapperTests()
        {
            // VS doesn't respect the IAssemblyFixture
            // ReSharper disable once UnusedVariable
            ////var rvtAddinFixture = new RvtAddinFixture();

            _mapper = new RvtClassMapper();
        }

        /// <summary>
        ///     Checks that the RvtElement type maps to Element
        /// </summary>
        [Fact]
        public void RvtClassMapper_RvtElement_GetMappedType_Is_Element()
        {
            var mappedType = _mapper.GetMappedType<RvtElement>();

            Assert.Equal(typeof(Element), mappedType);
        }

        /// <summary>
        ///     Checks that the ViewSheet type maps to Sheet
        /// </summary>
        [Fact]
        public void RvtClassMapper_RvtSheet_GetMappedType_Is_Sheet()
        {
            var mappedType = _mapper.GetMappedType<ViewSheet>();

            Assert.Equal(typeof(Sheet), mappedType);
        }

        /// <summary>
        ///     Checks that a Revit sheet maps correctly to a Sheet
        /// </summary>
        [Fact]
        public void RvtClassMapper_RvtSheet_Maps_To_Sheet()
        {
            Dictionary<Type, Type> typeDictionary = new Dictionary<Type, Type>
                {
                    { typeof(RvtElement), typeof(Element) },
                    { typeof(RvtView), typeof(View) },
                    { typeof(ViewSheet), typeof(Sheet) }
                };

            var mapper = new RvtClassMapper(typeDictionary);

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

            Sheet mappedSheet = mapper.Map<Sheet>(sheet);

            Assert.NotNull(mappedSheet);
            Assert.IsType<Sheet>(mappedSheet);

            Assert.Equal(SheetName, mappedSheet.Name);
            Assert.Equal(SheetNumber, mappedSheet.Number);
            Assert.Equal(RevisionName, mappedSheet.RevisionName);
        }

        /// <summary>
        ///     Checks that the View type maps to View
        /// </summary>
        [Fact]
        public void RvtClassMapper_RvtView_GetMappedType_Is_View()
        {
            var mappedType = _mapper.GetMappedType<RvtView>();

            Assert.Equal(typeof(View), mappedType);
        }

        /// <summary>
        ///     Checks that the Viewport type maps to Element
        /// </summary>
        [Fact]
        public void RvtClassMapper_RvtViewport_GetMappedType_Is_Element()
        {
            var mappedType = _mapper.GetMappedType<Viewport>();

            Assert.Equal(typeof(Element), mappedType);
        }
    }

    // The method below is an attempt to use a new free, open source library called Pose
    // However, a BadImageFormatException is always thrown by the DynamicInvoke method
    // This library is very new, so an attempt to get it to work will be made at a later date
    /*
    /// <summary>
    /// Checks that a Revit sheet maps correctly to a Sheet
    /// </summary>
    [Fact]
    public void RvtClassMapper_RvtSheet_Maps_To_Sheet()
    {
        ////var mockSheet = Is.A<ViewSheet>();

        const string SheetName = "SheetName";
        Shim nameShim = Shim.Replace(() => Is.A<RvtElement>().Name).With((RvtElement @this) => SheetName);

        const string SheetNumber = "SheetNumber";
        Shim numberShim = Shim.Replace(() => Is.A<ViewSheet>().SheetNumber).With((ViewSheet @this) => SheetNumber);

        const string RevisionName = "Revision1";
        Shim revisionShim = Shim.Replace(() => Is.A<RvtElement>().get_Parameter(BuiltInParameter.SHEET_CURRENT_REVISION).AsString()).With((Parameter p) => RevisionName);

        PoseContext.Isolate(
                            () =>
                                {
                                    Sheet mappedSheet = _mapper.Map<Sheet>(Is.A<ViewSheet>());

                                    Assert.NotNull(mappedSheet);
                                    Assert.IsType<Sheet>(mappedSheet);

                                    Assert.Equal(SheetName, mappedSheet.Name);
                                    Assert.Equal(SheetNumber, mappedSheet.Number);
                                    Assert.Equal(RevisionName, mappedSheet.RevisionName);
                                },
                            nameShim,
                            numberShim,
                            revisionShim);
    }*/
}