// <copyright file="RvtClassMapperTests.cs" company="StarkBIM Inc">
// Copyright (c) StarkBIM Inc. All rights reserved.
// </copyright>

namespace StarkBIM.SampleRevitApp.Commands.Test.SampleCommand.Services
{
    using System.Diagnostics.CodeAnalysis;

    using Autodesk.Revit.DB;

    using JetBrains.Annotations;

    using StarkBIM.SampleRevitApp.Commands.SampleCmd.Services.Impl;
    using StarkBIM.SampleRevitApp.Model;

    using Xunit;

    using Element = StarkBIM.SampleRevitApp.Model.Element;
    using RvtElement = Autodesk.Revit.DB.Element;
    using RvtView = Autodesk.Revit.DB.View;
    using View = StarkBIM.SampleRevitApp.Model.View;

    /// <summary>
    ///     Tests for the RvtClassMapper class
    ///     The inheritance from IAssemblyFixture sets up resolution for Revit assemblies
    /// </summary>
    /// <remarks>
    /// Remainder of tests exist in the Commands.JustMock.Test project. Separated so that this project is still usable by those that don't have JustMock
    /// </remarks>
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
    // ***
    // Update 2017/11/29 - Pose version 1.10
    // Verdict: Abandon attempts with Pose until it provides mocking capabilities for Revit API classes
    // Next steps: Try TypeMock Initialize to see if it works, then decide between JustMock and that
    // Remarks:
    // Still receive BadImageFormatException with code listed below.
    // However, problem is suspected to be with other libraries and not with the Revit API
    // A 64-bit version of AutoMapper was tested, and did not resolve the issue. Replacing all NuGet packages with 64-bit versions is not feasible
    // A small test replacing the functionality of TaskDialog.Show worked correctly, indicating that the error did not occur when loading Revit API DLLs
    // Pose also does not provide any mocking capability for Revit API objects, and there is no indication that this will be added later
    // Is.A<Type> returns null, but Pose's shimming capabilities require an instantiated object to work
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