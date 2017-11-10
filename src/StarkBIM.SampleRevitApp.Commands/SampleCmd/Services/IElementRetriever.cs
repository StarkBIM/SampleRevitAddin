// <copyright file="IElementRetriever.cs" company="StarkBIM Inc">
// Copyright (c) StarkBIM Inc. All rights reserved.
// </copyright>

namespace StarkBIM.SampleRevitApp.Commands.SampleCmd.Services
{
    using System.Collections.Generic;

    using Autodesk.Revit.DB;

    using JetBrains.Annotations;

    using StarkBIM.SampleRevitApp.Model;

    using View = StarkBIM.SampleRevitApp.Model.View;

    /// <summary>
    /// Represents a service that retrieves elements from a document
    /// </summary>
    public interface IElementRetriever
    {
        /// <summary>
        /// Retrieves all sheets from a document
        /// </summary>
        /// <param name="doc">The document</param>
        /// <returns>The sheets</returns>
        [NotNull]
        [ItemNotNull]
        [MustUseReturnValue]
        ICollection<Sheet> GetSheets([NotNull] Document doc);

        /// <summary>
        /// Retrieves all views from a document
        /// </summary>
        /// <param name="doc">The document</param>
        /// <returns>The views</returns>
        [NotNull]
        [ItemNotNull]
        [MustUseReturnValue]
        ICollection<View> GetViews([NotNull] Document doc);
    }
}