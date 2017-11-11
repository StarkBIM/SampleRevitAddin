// <copyright file="CommandModule.cs" company="StarkBIM Inc">
// Copyright (c) StarkBIM Inc. All rights reserved.
// </copyright>

namespace StarkBIM.SampleRevitApp.RvtAddin.Configuration
{
    using System.Reflection;

    using Autofac;

    using JetBrains.Annotations;

    using StarkBIM.SampleRevitApp.Commands.SampleCmd;
    using StarkBIM.SampleRevitApp.RvtAddin.Extensions;

    using Module = Autofac.Module;

    /// <summary>
    /// Autofac module containing registration information required for commands
    /// </summary>
    public class CommandModule : Module
    {
        /// <inheritdoc />
        protected override void Load([NotNull] ContainerBuilder builder)
        {
            // Use the SampleCommand type to get the StarkBIM.SampleRevitApp.Commands assembly
            Assembly commandAssembly = typeof(SampleCommand).Assembly;

            builder.RegisterCommandTypesForAssembly(commandAssembly);
        }
    }
}