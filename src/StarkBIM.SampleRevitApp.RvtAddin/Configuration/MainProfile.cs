// <copyright file="MainProfile.cs" company="StarkBIM Inc">
// Copyright (c) StarkBIM Inc. All rights reserved.
// </copyright>

namespace StarkBIM.SampleRevitApp.RvtAddin.Configuration
{
    using Autodesk.Revit.DB;

    using StarkBIM.SampleRevitApp.Model.Core;

    using Element = StarkBIM.SampleRevitApp.Model.Core.Element;
    using Profile = AutoMapper.Profile;
    using RvtElement = Autodesk.Revit.DB.Element;
    using RvtView = Autodesk.Revit.DB.View;
    using View = StarkBIM.SampleRevitApp.Model.Core.View;

    /// <summary>
    ///     Profile containing main AutoMapper mapping data
    /// </summary>
    public class MainProfile : Profile
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="MainProfile" /> class.
        /// </summary>
        public MainProfile()
        {
            CreateMap<RvtElement, Element>();

            CreateMap<RvtView, View>()
                .IncludeBase<RvtElement, ElementSet>();

            CreateMap<ViewSheet, Sheet>()
                .IncludeBase<RvtView, View>()
                .ForMember(s => s.Number, opt => opt.MapFrom(src => src.SheetNumber))
                .ForMember(s => s.RevisionName, opt => opt.ResolveUsing(viewSheet => viewSheet.get_Parameter(BuiltInParameter.SHEET_CURRENT_REVISION)?.AsString()));
        }
    }
}