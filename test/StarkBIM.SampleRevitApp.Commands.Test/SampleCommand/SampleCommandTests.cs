// <copyright file="SampleCommandTests.cs" company="StarkBIM Inc">
// Copyright (c) StarkBIM Inc. All rights reserved.
// </copyright>

namespace StarkBIM.SampleRevitApp.Commands.Test.SampleCommand
{
    using JetBrains.Annotations;

    using Moq;

    using StarkBIM.SampleRevitApp.Commands.Core;
    using StarkBIM.SampleRevitApp.Commands.SampleCmd;
    using StarkBIM.SampleRevitApp.Commands.SampleCmd.Services;

    using Xunit;

    /// <summary>
    ///     Tests for the SampleCommand class
    /// </summary>
    /// <remarks>
    /// Remainder of tests exist in the Commands.JustMock.Test project. Separated so that this project is still usable by those that don't have JustMock
    /// </remarks>
    public class SampleCommandTests : IAssemblyFixture<CommandsFixture>
    {
        [NotNull]
        private readonly SampleCommand _emptySampleCommand;

        [NotNull]
        private readonly Mock<IElementRetriever> _mockedSheetRetriever;

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
        }

        /// <summary>
        ///     Ensures the command's display name is Sample Command
        /// </summary>
        [Fact]
        public void SampleCommand_DisplayName_Is_Sample_Command() => Assert.Equal("Sample Command", _emptySampleCommand.DisplayName);

        /// <summary>
        ///     Ensures the command's internal name is sample
        /// </summary>
        [Fact]
        public void SampleCommand_Name_Is_Sample() => Assert.Equal("Sample", _emptySampleCommand.Name);

        [NotNull]
        private SampleCommand GetTestSampleCommand() => new SampleCommand(
                                                                          _mockedSampleProperties.Object,
                                                                          _mockedSheetRetriever.Object,
                                                                          _mockedDataTableCreator.Object,
                                                                          _mockedFilePathSelector.Object,
                                                                          _mockedDataWriter.Object,
                                                                          _mockedDialogService.Object);
    }
}