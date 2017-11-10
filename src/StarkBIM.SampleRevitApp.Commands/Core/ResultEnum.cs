// <copyright file="ResultEnum.cs" company="StarkBIM Inc">
// Copyright (c) StarkBIM Inc. All rights reserved.
// </copyright>

namespace StarkBIM.SampleRevitApp.Commands.Core
{
    /// <summary>
    /// Represents a result, either Failed, Succeeded or Cancelled
    /// </summary>
    public enum ResultEnum
    {
        /// <summary>
        ///     The command failed. For an event, the action that triggered this event failed.
        /// </summary>
        Failed = -1,

        /// <summary>
        ///     The command succeeded. For an event, the action that triggered this event succeeded.
        /// </summary>
        Succeeded = 0,

        /// <summary>
        ///     The command was cancelled. For an event, the action that triggered this event was cancelled by an event handler
        ///     from
        ///     the pre-event for the action
        /// </summary>
        Cancelled = 1
    }
}