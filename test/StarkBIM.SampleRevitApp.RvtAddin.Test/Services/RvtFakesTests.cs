// <copyright file="RvtFakesTests.cs" company="StarkBIM Inc">
// Copyright (c) StarkBIM Inc. All rights reserved.
// </copyright>

namespace StarkBIM.SampleRevitApp.RvtAddin.Test.Services
{
    /// <summary>
    ///     Tests that require fakes to run
    /// </summary>
    public sealed class RvtFakesTests
    {
        // Code for MS Fakes
        /*
    /// <summary>
    ///     Checks that a Revit sheet maps correctly to a Sheet
    /// </summary>
    [Fact]
    public void RvtClassMapper_RvtSheet_Maps_To_Sheet()
    {
        Dictionary<Type, Type> typeDictionary = new Dictionary<Type, Type>
            {
                { typeof(ShimElement), typeof(Element) },
                { typeof(ShimView), typeof(View) },
                { typeof(ShimViewSheet), typeof(Sheet) }
            };

        var mapper = new RvtClassMapper(typeDictionary);

        const string SheetName = "SheetName";
        const string SheetNumber = "SheetNumber";
        const string RevisionName = "Revision1";

        var viewSheet = A.Fake<Autodesk.Revit.DB.ViewSheet>();

        A.CallTo(() => viewSheet.Name).Returns(SheetName);
        A.CallTo(() => viewSheet.SheetNumber).Returns(SheetNumber);
        A.CallTo(() => viewSheet.)

        using (ShimsContext.Create())
        {
            var shimRevisionNameParameter = new ShimParameter
                {
                    AsString = () => RevisionName
                };

            var shimViewSheet = new ShimViewSheet()
                {
                    SheetNumberGet = () => RevisionName
                };

            var shimView = new ShimView(shimViewSheet);

            var shimElement = new ShimElement(shimView)
                {
                    NameGet = () => SheetName,
                    ////ParameterGetBuiltInParameter = builtInParameter => (int)builtInParameter == -1007412 ? shimRevisionNameParameter : null
                    ////ParameterGetBuiltInParameter =  shimRevisionNameParameter
                };

            Sheet mappedSheet = mapper.Map<Sheet>(shimElement);

            Assert.NotNull(mappedSheet);
            Assert.IsType<Sheet>(mappedSheet);

            Assert.Equal(SheetName, mappedSheet.Name);
            Assert.Equal(SheetNumber, mappedSheet.Number);
            Assert.Equal(RevisionName, mappedSheet.RevisionName);
        }
    }*/
    }
}