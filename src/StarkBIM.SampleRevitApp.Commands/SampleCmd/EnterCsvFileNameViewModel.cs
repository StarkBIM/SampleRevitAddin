// <copyright file="EnterCsvFileNameViewModel.cs" company="StarkBIM Inc">
// Copyright (c) StarkBIM Inc. All rights reserved.
// </copyright>

namespace StarkBIM.SampleRevitApp.Commands.SampleCmd
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Reactive.Linq;
    using System.Windows;

    using JetBrains.Annotations;

    using Ookii.Dialogs.Wpf;

    using ReactiveUI;

    using StarkBIM.SampleRevitApp.Commands.SampleCmd.Services;

    /// <summary>
    /// Viewmodel for the window where the path for the CSV file will be entered
    /// </summary>
    public class EnterCsvFileNameViewModel : ReactiveObject
    {
        [CanBeNull]
        [SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields", Justification = "Analyzer bug. The field is used")]
        private string _csvFilePath;

        /// <summary>
        /// Initializes a new instance of the <see cref="EnterCsvFileNameViewModel"/> class.
        /// </summary>
        /// <param name="validSaveFilePathChecker">The valid save file path checker</param>
        public EnterCsvFileNameViewModel([NotNull] IValidSaveFilePathChecker validSaveFilePathChecker)
        {
            if (validSaveFilePathChecker == null)
            {
                throw new ArgumentNullException(nameof(validSaveFilePathChecker));
            }

            var canOkExecute = this.WhenAnyValue(
                                                 x => x.CsvFilePath,
                                                 p => validSaveFilePathChecker.IsFilePathValid(p, p1 => p1.EndsWith(".csv", StringComparison.OrdinalIgnoreCase)))
                .ObserveOn(RxApp.MainThreadScheduler);

            OkCommand = ReactiveCommand.Create(
                                               (Window window) =>
                                                   {
                                                       window.DialogResult = true;
                                                       window.Close();
                                                   },
                                               canOkExecute);

            CancelCommand = ReactiveCommand.Create(
                                                   (Window window) =>
                                                       {
                                                           window.DialogResult = false;
                                                           window.Close();
                                                       });

            BrowseCommand = ReactiveCommand.Create(
                                                   (Window window) =>
                                                       {
                                                           var dialog = new VistaSaveFileDialog
                                                               {
                                                                   Filter = "CSV Files (*.csv)|*.csv",
                                                                   AddExtension = true
                                                               };

                                                           var result = dialog.ShowDialog(window);

                                                           if (result != true)
                                                           {
                                                               return;
                                                           }

                                                           CsvFilePath = dialog.FileName;
                                                       });
        }

        /// <summary>
        /// Gets the command to browse for a path to save the file
        /// </summary>
        [NotNull]
        public ReactiveCommand BrowseCommand { get; }

        /// <summary>
        /// Gets the command to cancel and close the window
        /// </summary>
        [NotNull]
        public ReactiveCommand CancelCommand { get; }

        /// <summary>
        /// Gets or sets the file path where the CSV file will be saved
        /// </summary>
        [CanBeNull]
        public string CsvFilePath
        {
            get => _csvFilePath;
            set => this.RaiseAndSetIfChanged(ref _csvFilePath, value);
        }

        /// <summary>
        /// Gets the command to close the window with a successful result
        /// </summary>
        [NotNull]
        public ReactiveCommand OkCommand { get; }
    }
}