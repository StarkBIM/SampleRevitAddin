// <copyright file="IDataTableCreator.cs" company="StarkBIM Inc">
// Copyright (c) StarkBIM Inc. All rights reserved.
// </copyright>

namespace StarkBIM.SampleRevitApp.Commands.SampleCmd.Services
{
    using System.Collections.Generic;
    using System.Data;

    using JetBrains.Annotations;

    using StarkBIM.SampleRevitApp.Model;

    /// <summary>
    ///     Represents a service that creates a datatable from a series of elements
    /// </summary>
    public interface IDataTableCreator
    {
        /// <summary>
        ///     Creates a data table for the given elements
        /// </summary>
        /// <remarks>
        ///     AutoMapper will be used to map an element to a DataRow
        /// </remarks>
        /// <param name="elements">The elements</param>
        /// <param name="dataTable">A previously created datatable to add rows to, optionally</param>
        /// <returns>The created datatable</returns>
        [NotNull]
        [MustUseReturnValue]
        DataTable CreateDataTable([NotNull] [ItemNotNull] IEnumerable<Element> elements, [CanBeNull] DataTable dataTable = null);
    }
}