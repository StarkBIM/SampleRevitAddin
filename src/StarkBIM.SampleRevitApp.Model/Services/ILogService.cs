// <copyright file="ILogService.cs" company="StarkBIM Inc">
// Copyright (c) StarkBIM Inc. All rights reserved.
// </copyright>

namespace StarkBIM.SampleRevitApp.Model.Services
{
    using System;

    using JetBrains.Annotations;

    using StarkBIM.SampleRevitApp.Model.Core;

    /// <summary>
    /// A logging service. Helper extension methods are provided in the LoggerExtensions class
    /// </summary>
    public interface ILogService
    {
        /// <summary>
        /// Logs a message with the given log level
        /// </summary>
        /// <param name="logLevel">The log level</param>
        /// <param name="message">The message. Must not be null or whitespace</param>
        void Log(LogLevel logLevel, [NotNull] string message);
    }

    public interface IDialogService
    {
        void NotifyException(Exception exception, object o, LogLevel critical);
    }
}