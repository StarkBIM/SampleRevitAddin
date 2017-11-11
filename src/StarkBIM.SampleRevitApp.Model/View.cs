// <copyright file="View.cs" company="StarkBIM Inc">
// Copyright (c) StarkBIM Inc. All rights reserved.
// </copyright>

namespace StarkBIM.SampleRevitApp.Model
{
    using System.Data;

    using JetBrains.Annotations;

    using StarkBIM.SampleRevitApp.Helpers;

    /// <summary>
    /// Represents a view in Revit
    /// </summary>
    public class View : Element
    {
        /// <summary>
        /// Gets or sets a string representing the view type (this is an enum in Revit)
        /// </summary>
        [NotNull]
        public string ViewType { get; set; }

        /// <inheritdoc />
        public override DataRow CreateRowForDataTable(DataTable dataTable)
        {
            DataRow rowForDataTable = base.CreateRowForDataTable(dataTable);

            rowForDataTable[nameof(ViewType)] = ViewType.ThrowIfNull();

            return rowForDataTable;
        }

        /// <inheritdoc />
        protected override void CreateMissingDataTableRows(DataTable dataTable)
        {
            base.CreateMissingDataTableRows(dataTable);

            if (!dataTable.Columns.Contains(nameof(ViewType)))
            {
                dataTable.Columns.Add(nameof(ViewType));
            }
        }
    }
}