// <copyright file="Element.cs" company="StarkBIM Inc">
// Copyright (c) StarkBIM Inc. All rights reserved.
// </copyright>

namespace StarkBIM.SampleRevitApp.Model
{
    using System;
    using System.Data;

    using JetBrains.Annotations;

    using StarkBIM.SampleRevitApp.Helpers;

    /// <summary>
    /// Class representing a Revit element
    /// </summary>
    public class Element
    {
        /// <summary>
        /// Gets or sets the name of the element
        /// </summary>
        [NotNull]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the unique id of the element
        /// </summary>
        [NotNull]
        public string UniqueId { get; set; }

        /// <summary>
        /// Gets or sets the element id of the element
        /// </summary>
        public int ElementId { get; set; }

        /// <summary>
        /// Creates a datarow and add the data contained in this element. Create the columns on the datatable if they do not exist
        /// </summary>
        /// <remarks>
        /// When overriding, call base.CreateRowForDataTable() before setting properties of derived classes</remarks>
        /// <param name="dataTable">The datatable</param>
        /// <returns>The row containing the data of this element</returns>
        [NotNull]
        public virtual DataRow CreateRowForDataTable([NotNull] DataTable dataTable)
        {
            if (dataTable == null)
            {
                throw new ArgumentNullException(nameof(dataTable));
            }

            CreateMissingDataTableRows(dataTable);

            DataRow dataRow = dataTable.NewRow();

            dataRow[nameof(Name)] = Name.ThrowIfNull();
            dataRow[nameof(UniqueId)] = UniqueId.ThrowIfNull();
            dataRow[nameof(ElementId)] = ElementId;

            return dataRow;
        }

        /// <summary>
        /// Checks whether all the required columns have been added to the datatable, and adds them if they have not
        /// </summary>
        /// <param name="dataTable">The datatable</param>
        protected virtual void CreateMissingDataTableRows([NotNull] DataTable dataTable)
        {
            if (!dataTable.Columns.Contains(nameof(Name)))
            {
                dataTable.Columns.Add(nameof(Name));
            }

            if (!dataTable.Columns.Contains(nameof(UniqueId)))
            {
                dataTable.Columns.Add(nameof(UniqueId));
            }

            if (!dataTable.Columns.Contains(nameof(ElementId)))
            {
                dataTable.Columns.Add(nameof(ElementId));
            }
        }
    }
}