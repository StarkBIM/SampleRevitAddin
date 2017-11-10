// <copyright file="ElementProfile.cs" company="StarkBIM Inc">
// Copyright (c) StarkBIM Inc. All rights reserved.
// </copyright>

namespace StarkBIM.SampleRevitApp.Commands.Core
{
    using Autodesk.Revit.DB;

    using StarkBIM.SampleRevitApp.Model;

    using Element = StarkBIM.SampleRevitApp.Model.Element;
    using Profile = AutoMapper.Profile;
    using RvtElement = Autodesk.Revit.DB.Element;
    using RvtView = Autodesk.Revit.DB.View;
    using View = StarkBIM.SampleRevitApp.Model.View;

    /// <summary>
    ///     Profile containing main AutoMapper mapping data for elements
    /// </summary>
    public class ElementProfile : Profile
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ElementProfile" /> class.
        /// </summary>
        public ElementProfile()
        {
            // Name and UniqueId should be done automatically
            CreateMap<RvtElement, Element>()
                .ForMember(e => e.ElementId, opt => opt.MapFrom(rvtEl => rvtEl.Id.IntegerValue));

            CreateMap<RvtView, View>()
                .IncludeBase<RvtElement, Element>();

            CreateMap<ViewSheet, Sheet>()
                .IncludeBase<RvtElement, Element>()
                .ForMember(s => s.Number, opt => opt.MapFrom(src => src.SheetNumber))
                .ForMember(s => s.RevisionName, opt => opt.ResolveUsing(viewSheet => viewSheet.get_Parameter(BuiltInParameter.SHEET_CURRENT_REVISION)?.AsString()));
        }
    }
}