// <copyright file="ElementCollector.cs" company="StarkBIM Inc">
// Copyright (c) StarkBIM Inc. All rights reserved.
// </copyright>

namespace StarkBIM.SampleRevitApp.Commands.SampleCmd.Services.Impl
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Autodesk.Revit.DB;

    using RvtView = Autodesk.Revit.DB.View;

    /// <inheritdoc />
    public class ElementCollector : IElementCollector
    {
        /// <inheritdoc />
        public ICollection<ViewSheet> GetSheets(Document doc)
        {
            if (doc == null)
            {
                throw new ArgumentNullException(nameof(doc));
            }

            var rvtSheets = new FilteredElementCollector(doc).OfClass(typeof(ViewSheet)).Cast<ViewSheet>().ToList();

            return rvtSheets;
        }

        /// <inheritdoc />
        public ICollection<RvtView> GetViews(Document doc)
        {
            if (doc == null)
            {
                throw new ArgumentNullException(nameof(doc));
            }

            var notSheetFilter = new ElementClassFilter(typeof(ViewSheet), true);

            var rvtViews = new FilteredElementCollector(doc).OfClass(typeof(RvtView)).WherePasses(notSheetFilter).Cast<RvtView>().ToList();

            return rvtViews;
        }
    }
}