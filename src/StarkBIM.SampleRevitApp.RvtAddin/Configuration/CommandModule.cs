// <copyright file="CommandModule.cs" company="StarkBIM Inc">
// Copyright (c) StarkBIM Inc. All rights reserved.
// </copyright>

namespace StarkBIM.SampleRevitApp.RvtAddin.Configuration
{
    using System.Reflection;

    using Autofac;

    using JetBrains.Annotations;

    using StarkBIM.SampleRevitApp.Commands.Core;
    using StarkBIM.SampleRevitApp.Commands.SampleCmd;

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

            // Register all types that implement IRvtCommand
            // AsSelf call registers the command itself, which is what is needed for GenericCommand
            builder.RegisterAssemblyTypes(commandAssembly).Where(t => typeof(IRvtCommand).IsAssignableFrom(t)).AsSelf();

            // Register all modules in the command assembly, which contain the information needed for each command
            builder.RegisterAssemblyModules(commandAssembly);
        }
    }
}