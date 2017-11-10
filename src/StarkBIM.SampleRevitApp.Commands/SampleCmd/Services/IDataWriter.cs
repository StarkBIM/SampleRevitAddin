// <copyright file="IDataWriter.cs" company="StarkBIM Inc">
// Copyright (c) StarkBIM Inc. All rights reserved.
// </copyright>

namespace StarkBIM.SampleRevitApp.Commands.SampleCmd.Services
{
    using System.Data;

    using JetBrains.Annotations;

    /// <summary>
    /// Represents a service that will write a datatable to a CSV file
    /// </summary>
    public interface IDataWriter
    {
        /// <summary>
        ///     Writes the datatable to a CSV file at the given path
        /// </summary>
        /// <param name="dataTable">The datatable</param>
        /// <param name="filePath">The file path</param>
        /// <returns>True if the write succeeded, otherwise false</returns>
        bool WriteDataToFile([NotNull] DataTable dataTable, [NotNull] string filePath);
    }
}