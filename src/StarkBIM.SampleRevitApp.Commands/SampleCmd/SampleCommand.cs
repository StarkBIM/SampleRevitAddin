// <copyright file="SampleCommand.cs" company="StarkBIM Inc">
// Copyright (c) StarkBIM Inc. All rights reserved.
// </copyright>

namespace StarkBIM.SampleRevitApp.Commands.SampleCmd
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;

    using Autodesk.Revit.UI;

    using JetBrains.Annotations;

    using StarkBIM.SampleRevitApp.Commands.Core;
    using StarkBIM.SampleRevitApp.Commands.SampleCmd.Services;
    using StarkBIM.SampleRevitApp.Helpers;

    /// <summary>
    ///     A sample command implementation.  This command does the following:
    ///     -   Read all sheets from a Revit model that is already open and active
    ///     -   Map the name, number and current revision of those sheets into our own class called Sheet
    ///     -   If there are no sheets, the command will end
    ///     -   Otherwise, the user will be prompted for a location to save the file data
    ///     -   If the user cancels, the command will end
    ///     -   Otherwise, the sheet data will be mapped to a DataTable
    ///     -   The DataTable will be written to a CSV file at the path specified earlier
    /// </summary>
    public class SampleCommand : RvtCommandBase<SampleCommand>
    {
        [NotNull]
        [SuppressMessage("ReSharper", "NotAccessedField.Local", Justification = "Temporary")]
        private readonly IDialogService _dialogService;

        [NotNull]
        private readonly IFilePathSelector _filePathSelector;

        [NotNull]
        private readonly IDataWriter _dataWriter;

        [NotNull]
        private readonly IDataTableCreator _dataTableCreator;

        [NotNull]
        private readonly IElementRetriever _sheetRetriever;

        /// <summary>
        ///     Initializes a new instance of the <see cref="SampleCommand" /> class.
        /// </summary>
        /// <param name="sampleCommandProperties">The command properties</param>
        /// <param name="sheetRetriever">The sheet retriever</param>
        /// <param name="dataTableCreator">The data table creator</param>
        /// <param name="filePathSelector">The file path selector</param>
        /// <param name="dataWriter">The data writer</param>
        /// <param name="dialogService">The dialog service</param>
        public SampleCommand(
            [NotNull] IRvtCommandProperties<SampleCommand> sampleCommandProperties,
            [NotNull] IElementRetriever sheetRetriever,
            [NotNull] IDataTableCreator dataTableCreator,
            [NotNull] IFilePathSelector filePathSelector,
            [NotNull] IDataWriter dataWriter,
            [NotNull] IDialogService dialogService)
            : base(sampleCommandProperties)
        {
            _dialogService = dialogService ?? throw new ArgumentNullException(nameof(dialogService));
            _filePathSelector = filePathSelector ?? throw new ArgumentNullException(nameof(filePathSelector));
            _dataWriter = dataWriter ?? throw new ArgumentNullException(nameof(dataWriter));
            _dataTableCreator = dataTableCreator ?? throw new ArgumentNullException(nameof(dataTableCreator));
            _sheetRetriever = sheetRetriever ?? throw new ArgumentNullException(nameof(sheetRetriever));
        }

        /// <inheritdoc />
        public override RvtCommandResult Run(ExternalCommandData commandData)
        {
            var uidoc = commandData.Application.ActiveUIDocument;

            if (uidoc == null)
            {
                return new RvtCommandResult
                    {
                        RvtResult = ResultEnum.Failed,
                        Message = "No active document is open"
                    };
            }

            var doc = uidoc.Document;

            var sheets = _sheetRetriever.GetSheets(doc);

            if (!sheets.Any())
            {
                return new RvtCommandResult
                    {
                        RvtResult = ResultEnum.Succeeded,
                        Message = "The document does not contain any sheets"
                    };
            }

            var dataTable = _dataTableCreator.CreateDataTable(sheets);

            var path = _filePathSelector.SelectFilePath();

            if (path.IsNullOrWhiteSpace())
            {
                return new RvtCommandResult
                    {
                        RvtResult = ResultEnum.Cancelled,
                        Message = "File path selection was cancelled"
                    };
            }

            bool writeResult = _dataWriter.WriteDataToFile(dataTable, path);

            if (!writeResult)
            {
                return new RvtCommandResult
                    {
                        RvtResult = ResultEnum.Failed,
                        Message = "Writing the file to disk failed"
                    };
            }

            _dialogService.ShowDialog("Success", $"Successfully wrote data to {path}");

            return new RvtCommandResult
                {
                    RvtResult = ResultEnum.Succeeded,
                    Message = $"Successfully wrote data to {path}"
                };
        }
    }
}