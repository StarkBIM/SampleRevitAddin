// <copyright file="DialogServiceTests.cs" company="StarkBIM Inc">
// Copyright (c) StarkBIM Inc. All rights reserved.
// </copyright>

namespace StarkBIM.SampleRevitApp.Commands.Test.SampleCommand.Services
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using JetBrains.Annotations;
    using StarkBIM.SampleRevitApp.Commands.SampleCmd.Services.Impl;
    using Xunit;

    /// <summary>
    ///     Tests for the dialog service class
    /// </summary>
    [SuppressMessage("ReSharper", "AssignNullToNotNullAttribute", Justification = "Test class")]
    public class DialogServiceTests : IAssemblyFixture<CommandsFixture>
    {
        [NotNull]
        private readonly DialogService _dialogService = new DialogService();

        /// <summary>
        ///     Ensures that the ShowDialog method throws an ArgumentException when the message is null or white space
        /// </summary>
        /// <param name="badMessage">The bad message</param>
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("\r\n")]
        public void ShowDialog_Throws_ArgumentException_When_Message_Is_NullOrWhitespace([NotNull] string badMessage)
        {
            Assert.Throws<ArgumentException>(() => _dialogService.ShowDialog("A title", badMessage));
        }

        /// <summary>
        ///     Ensures that the ShowDialog method throws an ArgumentException when the title is null or white space
        /// </summary>
        /// <param name="badTitle">The bad title</param>
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("\r\n")]
        public void ShowDialog_Throws_ArgumentException_When_Title_Is_NullOrWhitespace([NotNull] string badTitle)
        {
            Assert.Throws<ArgumentException>(() => _dialogService.ShowDialog(badTitle, "A message"));
        }
    }
}