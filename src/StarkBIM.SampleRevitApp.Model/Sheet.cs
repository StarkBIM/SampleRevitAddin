// <copyright file="Sheet.cs" company="StarkBIM Inc">
// Copyright (c) StarkBIM Inc. All rights reserved.
// </copyright>

namespace StarkBIM.SampleRevitApp.Model
{
    using System.Data;

    using JetBrains.Annotations;

    /// <summary>
    ///     Represents a Revit sheet
    /// </summary>
    /// <remarks>
    ///     Note that this class does not inherit from View. For our purposes, a sheet and a view are completely different
    ///     things
    ///     This is an example of how OOP classes should not religiously try to replicate their real-world counterparts
    /// </remarks>
    public class Sheet : Element
    {
        /// <summary>
        /// Gets or sets the sheet number
        /// </summary>
        [NotNull]
        public string Number { get; set; }

        /// <summary>
        /// Gets or sets the revision name
        /// </summary>
        [CanBeNull]
        public string RevisionName { get; set; }

        /// <inheritdoc />
        public override DataRow CreateRowForDataTable(DataTable dataTable)
        {
            DataRow rowForDataTable = base.CreateRowForDataTable(dataTable);

            rowForDataTable[nameof(Number)] = Number;
            rowForDataTable[nameof(RevisionName)] = RevisionName;

            return rowForDataTable;
        }

        /// <inheritdoc />
        protected override void CreateMissingDataTableRows(DataTable dataTable)
        {
            base.CreateMissingDataTableRows(dataTable);

            if (!dataTable.Columns.Contains(nameof(Number)))
            {
                dataTable.Columns.Add(nameof(Number));
            }

            if (!dataTable.Columns.Contains(nameof(RevisionName)))
            {
                dataTable.Columns.Add(nameof(RevisionName));
            }
        }
    }
}