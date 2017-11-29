// <copyright file="FilePathSelector.cs" company="StarkBIM Inc">
// Copyright (c) StarkBIM Inc. All rights reserved.
// </copyright>

namespace StarkBIM.SampleRevitApp.Commands.SampleCmd.Services.Impl
{
    using System;
    using System.Diagnostics;
    using System.Windows.Interop;

    using JetBrains.Annotations;

    using EnterCsvFileNameWindow = StarkBIM.SampleRevitApp.Commands.SampleCmd.EnterCsvFileNameWindow;

    /// <remarks>This class is not testable</remarks>
    /// <inheritdoc />
    public class FilePathSelector : IFilePathSelector
    {
        [NotNull]
        private readonly IValidSaveFilePathChecker _validSaveFilePathChecker;

        /// <summary>
        /// Initializes a new instance of the <see cref="FilePathSelector"/> class.
        /// </summary>
        /// <param name="validSaveFilePathChecker">The valid save file checker</param>
        public FilePathSelector([NotNull] IValidSaveFilePathChecker validSaveFilePathChecker) =>
            _validSaveFilePathChecker = validSaveFilePathChecker ?? throw new ArgumentNullException(nameof(validSaveFilePathChecker));

        /// <inheritdoc />
        public string SelectFilePath()
        {
            IntPtr mainWindowHandle = Process.GetCurrentProcess().MainWindowHandle;

            var viewModel = new EnterCsvFileNameViewModel(_validSaveFilePathChecker);

            var window = new EnterCsvFileNameWindow
                {
                    DataContext = viewModel,
                };

            // ReSharper disable once UnusedVariable
            var interopHelper = new WindowInteropHelper(window)
                {
                    Owner = mainWindowHandle
                };

            if (window.ShowDialog() != true)
            {
                return null;
            }

            return _validSaveFilePathChecker.IsFilePathValid(viewModel.CsvFilePath, p => p.EndsWith(".csv", StringComparison.OrdinalIgnoreCase)) ? viewModel.CsvFilePath : null;
        }
    }
}