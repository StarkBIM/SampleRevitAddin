// <copyright file="Log.cs" company="StarkBIM Inc">
// Copyright (c) StarkBIM Inc. All rights reserved.
// </copyright>

namespace StarkBIM.SampleRevitApp.Model.Services
{
    using System;

    using JetBrains.Annotations;

    using StarkBIM.SampleRevitApp.Model.Extensions;

    /// <summary>
    /// Static class that provides easy access logger methods. This prevents the logger from needing to be injected into every class, as it is always required.
    /// This class must be initialized during application startup with the desired logger instance
    /// </summary>
    public static class Log
    {
        private static bool _initialized;

        /// <summary>
        /// The logger instance
        /// </summary>
        [NotNull]
        public static ILogService Instance { get; private set; }

        /// <summary>
        /// Initializes the static class with the desired logger instance
        /// </summary>
        /// <param name="loggerInstance">The logger instance</param>
        public static void Initialize([NotNull] ILogService loggerInstance)
        {
            if (_initialized)
            {
                return;
            }

            Instance = loggerInstance ?? throw new ArgumentNullException(nameof(loggerInstance));

            _initialized = true;
        }

        public static void Critical(string message)
        {
            Instance.LogCritical(message);
        }

        public static void Debug(string message)
        {
            Instance.LogDebug(message);
        }

        public static void Error(string message)
        {
            Instance.LogError(message);
        }

        public static void Info(string message)
        {
            Instance.LogInfo(message);
        }

        public static void Trace(string message)
        {
            Instance.LogTrace(message);
        }

        public static void Warning(string message)
        {
            Instance.LogWarning(message);
        }
    }
}