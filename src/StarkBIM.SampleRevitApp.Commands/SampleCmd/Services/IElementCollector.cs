// <copyright file="IElementCollector.cs" company="StarkBIM Inc">
// Copyright (c) StarkBIM Inc. All rights reserved.
// </copyright>

namespace StarkBIM.SampleRevitApp.Commands.SampleCmd.Services
{
    using System.Collections.Generic;

    using Autodesk.Revit.DB;

    using JetBrains.Annotations;

    using RvtView = Autodesk.Revit.DB.View;

    /// <summary>
    /// A class that retrieves elements from a document
    /// </summary>
    public interface IElementCollector
    {
        /// <summary>
        /// Retrieves all sheets from a document
        /// </summary>
        /// <param name="doc">The document</param>
        /// <returns>The sheets</returns>
        [NotNull]
        [ItemNotNull]
        ICollection<ViewSheet> GetSheets([NotNull] Document doc);

        /// <summary>
        /// Retrieves all views from a document
        /// </summary>
        /// <param name="doc">The document</param>
        /// <returns>The views</returns>
        [NotNull]
        [ItemNotNull]
        ICollection<RvtView> GetViews([NotNull] Document doc);
    }
}