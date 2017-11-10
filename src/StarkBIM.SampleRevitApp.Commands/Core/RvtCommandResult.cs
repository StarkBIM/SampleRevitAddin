// <copyright file="RvtCommandResult.cs" company="StarkBIM Inc">
// Copyright (c) StarkBIM Inc. All rights reserved.
// </copyright>

namespace StarkBIM.SampleRevitApp.Commands.Core
{
    using System.Collections.Generic;

    using JetBrains.Annotations;

    using RvtElement = Autodesk.Revit.DB.Element;

    /// <summary>
    ///     Represents the result of the execution of an IRvtCommand
    /// </summary>
    public class RvtCommandResult
    {
        /// <summary>
        ///     Gets or sets the result of the command, to be returned to Revit
        /// </summary>
        /// <value>
        ///     The result of the command
        /// </value>
        public ResultEnum RvtResult { get; set; }

        /// <summary>
        ///     Gets or sets a message to be passed to the IExternalCommand Execute message parameter
        /// </summary>
        /// <value>
        ///     A message to be passed to the IExternalCommand Execute message parameter
        /// </value>
        [CanBeNull]
        public string Message { get; set; }

        /// <summary>
        ///     Gets a collection of elements that can be passed to the IExternalCommand Execute elements parameter
        /// </summary>
        /// <value>
        ///     A collection of elements that can be passed to the IExternalCommand Execute elements parameter
        /// </value>
        [ItemNotNull]
        [NotNull]
        public ICollection<RvtElement> Elements { get; } = new List<RvtElement>();
    }
}