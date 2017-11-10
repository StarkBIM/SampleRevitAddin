// <copyright file="DataTableCreator.cs" company="StarkBIM Inc">
// Copyright (c) StarkBIM Inc. All rights reserved.
// </copyright>

namespace StarkBIM.SampleRevitApp.Commands.SampleCmd.Services.Impl
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;

    using StarkBIM.SampleRevitApp.Model;

    /// <inheritdoc />
    public class DataTableCreator : IDataTableCreator
    {
        /// <inheritdoc />
        public DataTable CreateDataTable(IEnumerable<Element> elements, DataTable dataTable = null)
        {
            if (elements == null)
            {
                throw new ArgumentNullException(nameof(elements));
            }

            if (dataTable == null)
            {
                dataTable = new DataTable();
            }

            var elementList = elements.ToList();

            if (!elementList.Any())
            {
                return dataTable;
            }

            IEnumerable<DataRow> dataRows = elementList.Select(e => e.CreateRowForDataTable(dataTable));

            foreach (DataRow dataRow in dataRows)
            {
                dataTable.Rows.Add(dataRow);
            }

            return dataTable;
        }
    }
}