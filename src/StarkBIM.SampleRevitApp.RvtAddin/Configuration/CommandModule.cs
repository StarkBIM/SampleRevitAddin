// <copyright file="CommandModule.cs" company="StarkBIM Inc">
// Copyright (c) StarkBIM Inc. All rights reserved.
// </copyright>

namespace StarkBIM.SampleRevitApp.RvtAddin.Configuration
{
    using Autofac;

    using JetBrains.Annotations;

    using StarkBIM.SampleRevitApp.Commands.Sample;
    using StarkBIM.SampleRevitApp.Model.Core;

    public class CommandModule : Module
    {
        /// <inheritdoc />
        protected override void Load([NotNull] ContainerBuilder builder)
        {
            // Use the SampleCommand type to get the StarkBIM.SampleRevitApp.Commands assembly
            // Register all types that implement IRvtCommand
            // AsSelf call registers the
            builder.RegisterAssemblyTypes(typeof(SampleCommand).Assembly).Where(t => typeof(IRvtCommand).IsAssignableFrom(t)).AsSelf();
        }
    }
}