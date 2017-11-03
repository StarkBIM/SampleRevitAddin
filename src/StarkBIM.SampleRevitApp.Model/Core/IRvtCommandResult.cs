// <copyright file="IRvtCommandResult.cs" company="StarkBIM Inc">
// Copyright (c) StarkBIM Inc. All rights reserved.
// </copyright>

namespace StarkBIM.SampleRevitApp.Model.Core
{
    using System.Collections.Generic;

    using JetBrains.Annotations;

    using RvtElement = Autodesk.Revit.DB.Element;

    /// <summary>
    /// Represents the result of the execution of an IRvtCommand
    /// </summary>
    public interface IRvtCommandResult
    {
        /// <summary>
        /// Gets the result of the command, to be returned to Revit
        /// </summary>
        /// <value>
        /// The result of the command
        /// </value>
        ResultEnum RvtResult { get; }

        /// <summary>
        /// Gets a message to be passed to the IExternalCommand Execute message parameter
        /// </summary>
        /// <value>
        /// A message to be passed to the IExternalCommand Execute message parameter
        /// </value>
        [CanBeNull]
        string Message { get; }

        /// <summary>
        /// Gets a collection of elements that can be passed to the IExternalCommand Execute elements parameter
        /// </summary>
        /// <value>
        /// A collection of elements that can be passed to the IExternalCommand Execute elements parameter
        /// </value>
        [ItemNotNull]
        [NotNull]
        ICollection<RvtElement> Elements { get; }
    }
}