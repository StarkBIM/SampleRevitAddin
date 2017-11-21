// <copyright file="SampleCommandTests.cs" company="StarkBIM Inc">
// Copyright (c) StarkBIM Inc. All rights reserved.
// </copyright>

namespace StarkBIM.SampleRevitApp.Commands.Test.SampleCommand
{
    using System.Collections.Generic;
    using System.Data;

    using Autodesk.Revit.DB;
    using Autodesk.Revit.UI;

    using JetBrains.Annotations;

    using Moq;

    using StarkBIM.SampleRevitApp.Commands.Core;
    using StarkBIM.SampleRevitApp.Commands.SampleCmd;
    using StarkBIM.SampleRevitApp.Commands.SampleCmd.Services;
    using StarkBIM.SampleRevitApp.Model;

    using Xunit;

    using JustMock = Telerik.JustMock.Mock;

    /// <summary>
    ///     Tests for the SampleCommand class
    /// </summary>
    public class SampleCommandTests : IAssemblyFixture<CommandsFixture>
    {
        [NotNull]
        private readonly SampleCommand _emptySampleCommand;

        [NotNull]
        private readonly ExternalCommandData _externalCommandData;

        [NotNull]
        private readonly UIApplication _uiApplication;

        [NotNull]
        private readonly UIDocument _uiDocument;

        [NotNull]
        private readonly Mock<IElementRetriever> _mockedSheetRetriever;

        [NotNull]
        private readonly Document _document;

        [NotNull]
        private readonly Mock<IDataTableCreator> _mockedDataTableCreator;

        [NotNull]
        private readonly Mock<IDataWriter> _mockedDataWriter;

        [NotNull]
        private readonly Mock<IFilePathSelector> _mockedFilePathSelector;

        [NotNull]
        private readonly Mock<IRvtCommandProperties<SampleCommand>> _mockedSampleProperties;

        [NotNull]
        private readonly Mock<IDialogService> _mockedDialogService;

        /// <summary>
        ///     Initializes a new instance of the <see cref="SampleCommandTests" /> class.
        /// </summary>
        public SampleCommandTests()
        {
            _mockedSheetRetriever = new Mock<IElementRetriever>();

            _mockedDataTableCreator = new Mock<IDataTableCreator>();

            _mockedFilePathSelector = new Mock<IFilePathSelector>();

            _mockedDataWriter = new Mock<IDataWriter>();

            _mockedSampleProperties = new Mock<IRvtCommandProperties<SampleCommand>>();
            _mockedSampleProperties.SetupGet(p => p.Name).Returns("Sample");

            _mockedSampleProperties.SetupGet(p => p.DisplayName).Returns("Sample Command");

            _mockedDialogService = new Mock<IDialogService>();
            _mockedDialogService.Setup(dialogService => dialogService.ShowDialog(It.IsAny<string>(), It.IsAny<string>())).Verifiable();

            _emptySampleCommand = GetTestSampleCommand();

            _externalCommandData = JustMock.Create<ExternalCommandData>();

            _uiApplication = JustMock.Create<UIApplication>();

            _uiDocument = JustMock.Create<UIDocument>();

            _document = JustMock.Create<Document>();
        }

        /// <summary>
        ///     Ensures the command's display name is Sample Command
        /// </summary>
        [Fact]
        public void SampleCommand_DisplayName_Is_Sample_Command()
        {
            Assert.Equal("Sample Command", _emptySampleCommand.DisplayName);
        }

        /// <summary>
        /// Ensures that a dialog box with the correct message is shown on success
        /// </summary>
        [Fact]
        public void SampleCommand_Displays_DialogBox_On_Success()
        {
            JustMock.Arrange(() => _externalCommandData.Application).Returns(_uiApplication);

            JustMock.Arrange(() => _uiApplication.ActiveUIDocument).Returns(_uiDocument);

            JustMock.Arrange(() => _uiDocument.Document).Returns(_document);

            // Need to return at least one sheet to get as far as picking the file
            _mockedSheetRetriever.Setup(retriever => retriever.GetSheets(It.IsAny<Document>())).Returns(
                                                                                                        new List<Sheet>
                                                                                                            {
                                                                                                                new Sheet()
                                                                                                            });

            _mockedFilePathSelector.Setup(selector => selector.SelectFilePath()).Returns("filepath.csv");

            _mockedDataWriter.Setup(writer => writer.WriteDataToFile(It.IsAny<DataTable>(), It.IsAny<string>())).Returns(true);

            var sampleCommand = GetTestSampleCommand();

            sampleCommand.Run(_externalCommandData);

            _mockedDialogService.Verify(dialogService => dialogService.ShowDialog("Success", "Successfully wrote data to filepath.csv"), Times.Once);
        }

        /// <summary>
        ///     Ensures the command's internal name is sample
        /// </summary>
        [Fact]
        public void SampleCommand_Name_Is_Sample()
        {
            Assert.Equal("Sample", _emptySampleCommand.Name);
        }

        /// <summary>
        ///     Ensures the command is cancelled if the pick file operation is cancelled
        /// </summary>
        [Fact]
        public void SampleCommand_Run_Cancels_If_PickFile_Cancelled()
        {
            JustMock.Arrange(() => _externalCommandData.Application).Returns(_uiApplication);

            JustMock.Arrange(() => _uiApplication.ActiveUIDocument).Returns(_uiDocument);

            JustMock.Arrange(() => _uiDocument.Document).Returns(_document);

            // Need to return at least one sheet to get as far as picking the file
            _mockedSheetRetriever.Setup(retriever => retriever.GetSheets(It.IsAny<Document>())).Returns(
                                                                                                        new List<Sheet>
                                                                                                            {
                                                                                                                new Sheet()
                                                                                                            });

            var sampleCommand = GetTestSampleCommand();

            var result = sampleCommand.Run(_externalCommandData);

            Assert.Equal(ResultEnum.Cancelled, result.RvtResult);
            Assert.Equal("File path selection was cancelled", result.Message);
        }

        /// <summary>
        ///     Ensures the command fails if no document is open
        /// </summary>
        [Fact]
        public void SampleCommand_Run_Fails_If_No_Document_Open()
        {
            JustMock.Arrange(() => _externalCommandData.Application).Returns(_uiApplication);

            JustMock.Arrange(() => _uiApplication.ActiveUIDocument).Returns((UIDocument)null);

            var result = _emptySampleCommand.Run(_externalCommandData);

            Assert.NotNull(result);

            Assert.Equal(ResultEnum.Failed, result.RvtResult);

            Assert.Equal("No active document is open", result.Message);
        }

        /// <summary>
        ///     Ensures that the command fails if writing the CSV fails
        /// </summary>
        [Fact]
        public void SampleCommand_Run_Fails_If_WriteFile_Fails()
        {
            JustMock.Arrange(() => _externalCommandData.Application).Returns(_uiApplication);

            JustMock.Arrange(() => _uiApplication.ActiveUIDocument).Returns(_uiDocument);

            JustMock.Arrange(() => _uiDocument.Document).Returns(_document);

            // Need to return at least one sheet to get as far as picking the file
            _mockedSheetRetriever.Setup(retriever => retriever.GetSheets(It.IsAny<Document>())).Returns(
                                                                                                        new List<Sheet>
                                                                                                            {
                                                                                                                new Sheet()
                                                                                                            });

            _mockedFilePathSelector.Setup(selector => selector.SelectFilePath()).Returns("filepath.csv");

            _mockedDataWriter.Setup(writer => writer.WriteDataToFile(It.IsAny<DataTable>(), It.IsAny<string>())).Returns(false);

            var sampleCommand = GetTestSampleCommand();

            var result = sampleCommand.Run(_externalCommandData);

            Assert.Equal(ResultEnum.Failed, result.RvtResult);
            Assert.Equal("Writing the file to disk failed", result.Message);
        }

        /// <summary>
        ///     Ensures that the command succeeds if all steps succeed
        /// </summary>
        [Fact]
        public void SampleCommand_Run_Succeeds_If_All_Steps_Complete()
        {
            JustMock.Arrange(() => _externalCommandData.Application).Returns(_uiApplication);

            JustMock.Arrange(() => _uiApplication.ActiveUIDocument).Returns(_uiDocument);

            JustMock.Arrange(() => _uiDocument.Document).Returns(_document);

            // Need to return at least one sheet to get as far as picking the file
            _mockedSheetRetriever.Setup(retriever => retriever.GetSheets(It.IsAny<Document>())).Returns(
                                                                                                        new List<Sheet>
                                                                                                            {
                                                                                                                new Sheet()
                                                                                                            });

            _mockedFilePathSelector.Setup(selector => selector.SelectFilePath()).Returns("filepath.csv");

            _mockedDataWriter.Setup(writer => writer.WriteDataToFile(It.IsAny<DataTable>(), It.IsAny<string>())).Returns(true);

            var sampleCommand = GetTestSampleCommand();

            var result = sampleCommand.Run(_externalCommandData);

            Assert.Equal(ResultEnum.Succeeded, result.RvtResult);
            Assert.Equal("Successfully wrote data to filepath.csv", result.Message);
        }

        /// <summary>
        ///     Ensures that running the command returns a successful result with the correct message when there are no commands in
        ///     the document
        /// </summary>
        [Fact]
        public void SampleCommand_Run_Succeeds_When_No_Sheets_In_Document()
        {
            JustMock.Arrange(() => _externalCommandData.Application).Returns(_uiApplication);

            JustMock.Arrange(() => _uiApplication.ActiveUIDocument).Returns(_uiDocument);

            JustMock.Arrange(() => _uiDocument.Document).Returns(_document);

            _mockedSheetRetriever.Setup(retriever => retriever.GetSheets(It.IsAny<Document>())).Returns(new List<Sheet>());

            var sampleCommand = GetTestSampleCommand();

            var result = sampleCommand.Run(_externalCommandData);

            Assert.Equal(ResultEnum.Succeeded, result.RvtResult);
            Assert.Equal("The document does not contain any sheets", result.Message);
        }

        [NotNull]
        private SampleCommand GetTestSampleCommand()
        {
            return new SampleCommand(
                                     _mockedSampleProperties.Object,
                                     _mockedSheetRetriever.Object,
                                     _mockedDataTableCreator.Object,
                                     _mockedFilePathSelector.Object,
                                     _mockedDataWriter.Object,
                                     _mockedDialogService.Object);
        }
    }
}