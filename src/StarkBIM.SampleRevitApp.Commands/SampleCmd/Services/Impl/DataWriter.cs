// <copyright file="DataWriter.cs" company="StarkBIM Inc">
// Copyright (c) StarkBIM Inc. All rights reserved.
// </copyright>

namespace StarkBIM.SampleRevitApp.Commands.SampleCmd.Services.Impl
{
    using System;
    using System.Data;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Linq;

    using CsvHelper;

    using JetBrains.Annotations;

    /// <inheritdoc />
    public class DataWriter : IDataWriter
    {
        [NotNull]
        private readonly IValidSaveFilePathChecker _validSaveFilePathChecker;

        /// <summary>
        /// Initializes a new instance of the <see cref="DataWriter"/> class.
        /// </summary>
        /// <param name="validSaveFilePathChecker">Valid save file path checker</param>
        [SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", Justification = "Analyzer bug")]
        public DataWriter([NotNull] IValidSaveFilePathChecker validSaveFilePathChecker) =>
            _validSaveFilePathChecker = validSaveFilePathChecker ?? throw new ArgumentNullException(nameof(validSaveFilePathChecker));

        /// <inheritdoc />
        public bool WriteDataToFile(DataTable dataTable, string filePath)
        {
            if (dataTable == null)
            {
                throw new ArgumentNullException(nameof(dataTable));
            }

            if (filePath == null)
            {
                throw new ArgumentNullException(nameof(filePath));
            }

            if (!_validSaveFilePathChecker.IsFilePathValid(filePath, s => s.EndsWith(".csv", StringComparison.OrdinalIgnoreCase)))
            {
                return false;
            }

            var rows = dataTable.Rows.Cast<DataRow>().ToList();

            var columns = dataTable.Columns.Cast<DataColumn>().ToList();

            if (!columns.Any() || !rows.Any())
            {
                return false;
            }

            try
            {
                using (TextWriter writer = new StreamWriter(filePath))
                {
                    var csvWriter = new CsvWriter(writer);

                    foreach (DataColumn column in columns)
                    {
                        csvWriter.WriteField(column.ColumnName);
                    }

                    csvWriter.NextRecord();

                    foreach (DataRow row in rows)
                    {
                        for (int i = 0; i < columns.Count; i++)
                        {
                            csvWriter.WriteField(row[i]);
                        }

                        csvWriter.NextRecord();
                    }
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                throw;
            }

            return true;
        }
    }
}