// <copyright file="RvtElement.cs" company="StarkBIM Inc">
// Copyright (c) StarkBIM Inc. All rights reserved.
// </copyright>

namespace StarkBIM.SampleRevitApp.Model.Core
{
    using JetBrains.Annotations;

    /// <summary>
    /// Class representing a Revit element
    /// </summary>
    public class Element
    {
        /// <summary>
        /// Gets or sets the unique id of the element
        /// </summary>
        [NotNull]
        public string UniqueId { get; set; }

        /// <summary>
        /// Gets or sets the element id of the element
        /// </summary>
        public int ElementId { get; set; }
    }

    public class View : Element
    { }

    public class Sheet : Element
    {
        [NotNull]
        public string Name { get; set; }

        [NotNull]
        public string Number { get; set; }

        [CanBeNull]
        public string RevisionName { get; set; }
    }
}