// <copyright file="MainModule.cs" company="StarkBIM Inc">
// Copyright (c) StarkBIM Inc. All rights reserved.
// </copyright>

namespace StarkBIM.SampleRevitApp.RvtAddin.Configuration
{
    using Autofac;

    using JetBrains.Annotations;

    using StarkBIM.SampleRevitApp.RvtAddin.Services;

    /// <summary>
    /// Autofac module containing all services and other non-command data
    /// </summary>
    public class MainModule : Module
    {
        /// <inheritdoc />
        protected override void Load([NotNull] ContainerBuilder builder)
        {
            builder.RegisterType<DialogService>().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<LogService>().AsImplementedInterfaces().SingleInstance();
        }
    }
}