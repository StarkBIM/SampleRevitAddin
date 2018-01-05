// <copyright file="RvtTestSetBase.cs" company="StarkBIM Inc">
// Copyright (c) StarkBIM Inc. All rights reserved.
// </copyright>

namespace StarkBIM.SampleRevitApp.Commands.Test.SampleCommand
{
    using System;

    using Autodesk.Revit.UI;

    using JetBrains.Annotations;

    using RvtTestRunner;
    using RvtTestRunner.Util;

    using Xunit;

    /// <summary>
    /// Base class for all test sets that require access to an ExternalCommandData instance
    /// </summary>
    public abstract class RvtTestSetBase : IAssemblyFixture<CommandsFixture>
    {
        // Need to get the CommandData from the Test Runner
        [NotNull]
        private readonly ExternalCommandData _commandData = RunnerCommand.CommandData.ThrowIfNull();

        /// <summary>
        ///     Checks that the current context is valid for the given tests
        /// </summary>
        /// <returns>True if the tests should execute, otherwise false</returns>
        public abstract bool IsTestContextValid();

        /// <summary>
        /// Gets the command data instance initialized by the RunnerCommand
        /// </summary>
        /// <returns>THe command data</returns>
        [NotNull]
        protected ExternalCommandData GetCommandData()
        {
            // Do we want to throw an exception or return null here?
            if (!IsTestContextValid())
            {
                throw new InvalidOperationException("The context is not valid for this test");
            }

            return _commandData;
        }
    }
}