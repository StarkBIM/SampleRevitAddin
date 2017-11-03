// <copyright file="LoggerExtensions.cs" company="StarkBIM Inc">
// Copyright (c) StarkBIM Inc. All rights reserved.
// </copyright>

namespace StarkBIM.SampleRevitApp.Model.Extensions
{
    using System;

    using JetBrains.Annotations;

    using StarkBIM.SampleRevitApp.Model.Core;
    using StarkBIM.SampleRevitApp.Model.Services;

    public static class LoggerExtensions
    {
        public static void LogCritical([NotNull] this ILogService logger, [NotNull] string message)
        {
            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }

            logger.Log(LogLevel.Critical, message);
        }

        public static void LogDebug([NotNull] this ILogService logger, string message)
        {
            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }

            logger.Log(LogLevel.Debug, message);
        }

        public static void LogError([NotNull] this ILogService logger, string message)
        {
            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }

            logger.Log(LogLevel.Error, message);
        }

        public static void LogInfo([NotNull] this ILogService logger, string message)
        {
            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }

            logger.Log(LogLevel.Information, message);
        }

        public static void LogTrace([NotNull] this ILogService logger, string message)
        {
            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }

            logger.Log(LogLevel.Trace, message);
        }

        public static void LogWarning([NotNull] this ILogService logger, string message)
        {
            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }

            logger.Log(LogLevel.Warning, message);
        }
    }
}