// <copyright file="SampleCommandTestsRealData.cs" company="StarkBIM Inc">
// Copyright (c) StarkBIM Inc. All rights reserved.
// </copyright>

namespace StarkBIM.SampleRevitApp.Commands.Test.SampleCommand
{
    using Xunit;

    /// <summary>
    /// Tests for SampleCommand that use actual data from Revit, rather than mocked data. This functionality has not yet been completed
    /// </summary>
    public class SampleCommandTestsRealData : RvtTestSetBase
    {
        /// <inheritdoc />
        public override bool IsTestContextValid() => true;

        /// <summary>
        /// Ensures that GetCommandData is set correctly
        /// </summary>
        [Fact]
        public void GetCommandData_Works() => Assert.NotNull(GetCommandData());
    }
}