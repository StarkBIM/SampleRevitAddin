// <copyright file="EnterCsvFileNameViewModelTests.cs" company="StarkBIM Inc">
// Copyright (c) StarkBIM Inc. All rights reserved.
// </copyright>

namespace StarkBIM.SampleRevitApp.Commands.Test.SampleCommand
{
    using System;
    using System.Reactive.Concurrency;
    using System.Windows.Input;

    using JetBrains.Annotations;

    using Moq;

    using ReactiveUI.Testing;

    using StarkBIM.SampleRevitApp.Commands.SampleCmd;
    using StarkBIM.SampleRevitApp.Commands.SampleCmd.Services;

    using Xunit;

    /// <summary>
    /// Tests for the EnterCsvFileNameViewModel class
    /// </summary>
    /// <remarks>
    /// We cannot test whether the Window.Close calls actually work, but we can test whether the commands are able to be executed
    /// </remarks>
    public class EnterCsvFileNameViewModelTests
    {
        [NotNull]
        private readonly Mock<IValidSaveFilePathChecker> _mockedPathChecker;

        /// <summary>
        /// Initializes a new instance of the <see cref="EnterCsvFileNameViewModelTests"/> class.
        /// </summary>
        public EnterCsvFileNameViewModelTests()
        {
            _mockedPathChecker = new Mock<IValidSaveFilePathChecker>();
        }

        /// <summary>
        /// Ensures that the OK command can execute when the file path is valid
        /// </summary>
        [Fact]
        public void OkCommand_Can_Execute_When_File_Path_Is_Valid()
        {
            using (TestUtils.WithScheduler(ImmediateScheduler.Instance))
            {
                _mockedPathChecker.Setup(c => c.IsFilePathValid(It.IsAny<string>(), It.IsAny<Predicate<string>>())).Returns(true);

                var enterCsvFileNameViewModel = new EnterCsvFileNameViewModel(_mockedPathChecker.Object) { CsvFilePath = "AFileName" };

                // ICommand is explicitly implemented and we need ICommand.CanExecute
                // The exposed IObservable never gets triggered until a value changes, so it has no value that we can retrieve
                var okCommand = (ICommand)enterCsvFileNameViewModel.OkCommand;

                var result = okCommand.CanExecute(null);

                Assert.True(result);
            }
        }

        /// <summary>
        /// Ensures that the OK command cannot execute when there is no value entered
        /// </summary>
        [Fact]
        public void OkCommand_Cannot_Execute_When_File_Path_Is_Empty()
        {
            using (TestUtils.WithScheduler(ImmediateScheduler.Instance))
            {
                _mockedPathChecker.Setup(c => c.IsFilePathValid(It.IsAny<string>(), It.IsAny<Predicate<string>>())).Returns(false);

                var enterCsvFileNameViewModel = new EnterCsvFileNameViewModel(_mockedPathChecker.Object);

                // ICommand is explicitly implemented and we need ICommand.CanExecute
                // The exposed IObservable never gets triggered until a value changes, so it has no value that we can retrieve
                var okCommand = (ICommand)enterCsvFileNameViewModel.OkCommand;

                var result = okCommand.CanExecute(null);

                Assert.False(result);
            }
        }

        /// <summary>
        /// Ensures that the OK command cannot execute when the file path is invalid
        /// </summary>
        [Fact]
        public void OkCommand_Cannot_Execute_When_File_Path_Is_Invalid()
        {
            using (TestUtils.WithScheduler(ImmediateScheduler.Instance))
            {
                _mockedPathChecker.Setup(c => c.IsFilePathValid(It.IsAny<string>(), It.IsAny<Predicate<string>>())).Returns(false);

                var enterCsvFileNameViewModel = new EnterCsvFileNameViewModel(_mockedPathChecker.Object) { CsvFilePath = "AFileName" };

                // ICommand is explicitly implemented and we need ICommand.CanExecute
                // The exposed IObservable never gets triggered until a value changes, so it has no value that we can retrieve
                var okCommand = (ICommand)enterCsvFileNameViewModel.OkCommand;

                var result = okCommand.CanExecute(null);

                Assert.False(result);
            }
        }

        /// <summary>
        /// Ensures that the cancel command can execute
        /// </summary>
        [Fact]
        public void CancelCommand_Can_Execute()
        {
            using (TestUtils.WithScheduler(ImmediateScheduler.Instance))
            {
                var enterCsvFileNameViewModel = new EnterCsvFileNameViewModel(_mockedPathChecker.Object);

                // ICommand is explicitly implemented and we need ICommand.CanExecute
                // The exposed IObservable never gets triggered until a value changes, so it has no value that we can retrieve
                var cancelCommand = (ICommand)enterCsvFileNameViewModel.CancelCommand;

                var result = cancelCommand.CanExecute(null);

                Assert.True(result);
            }
        }

        /// <summary>
        /// Ensures that the cancel command can execute
        /// </summary>
        [Fact]
        public void BrowseCommand_Can_Execute()
        {
            using (TestUtils.WithScheduler(ImmediateScheduler.Instance))
            {
                var enterCsvFileNameViewModel = new EnterCsvFileNameViewModel(_mockedPathChecker.Object);

                // ICommand is explicitly implemented and we need ICommand.CanExecute
                // The exposed IObservable never gets triggered until a value changes, so it has no value that we can retrieve
                var browseCommand = (ICommand)enterCsvFileNameViewModel.BrowseCommand;

                var result = browseCommand.CanExecute(null);

                Assert.True(result);
            }
        }
    }
}