// <copyright file="ElementRetriever.cs" company="StarkBIM Inc">
// Copyright (c) StarkBIM Inc. All rights reserved.
// </copyright>

namespace StarkBIM.SampleRevitApp.Commands.SampleCmd.Services.Impl
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Autodesk.Revit.DB;

    using JetBrains.Annotations;

    using StarkBIM.SampleRevitApp.Model;

    using View = StarkBIM.SampleRevitApp.Model.View;

    /// <inheritdoc />
    public class ElementRetriever : IElementRetriever
    {
        [NotNull]
        private readonly IRvtClassMapper _classMapper;

        [NotNull]
        private readonly IElementCollector _collector;

        /// <summary>
        /// Initializes a new instance of the <see cref="ElementRetriever"/> class.
        /// </summary>
        /// <param name="collector">The element collector</param>
        /// <param name="classMapper">The class mapper</param>
        public ElementRetriever([NotNull] IElementCollector collector, [NotNull] IRvtClassMapper classMapper)
        {
            _classMapper = classMapper ?? throw new ArgumentNullException(nameof(classMapper));
            _collector = collector ?? throw new ArgumentNullException(nameof(collector));
        }

        /// <inheritdoc />
        public ICollection<Sheet> GetSheets(Document doc)
        {
            if (doc == null)
            {
                throw new ArgumentNullException(nameof(doc));
            }

            var rvtSheets = _collector.GetSheets(doc);

            var sheets = rvtSheets.Select(rvtSheet => _classMapper.Map<Sheet>(rvtSheet)).ToList();

            return sheets;
        }

        /// <inheritdoc />
        public ICollection<View> GetViews(Document doc)
        {
            if (doc == null)
            {
                throw new ArgumentNullException(nameof(doc));
            }

            var rvtViews = _collector.GetViews(doc);

            var views = rvtViews.Select(rvtView => _classMapper.Map<View>(rvtView)).ToList();

            return views;
        }
    }
}