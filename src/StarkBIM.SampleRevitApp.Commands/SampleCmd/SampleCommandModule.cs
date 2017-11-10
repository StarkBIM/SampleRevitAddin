// <copyright file="SampleCommandModule.cs" company="StarkBIM Inc">
// Copyright (c) StarkBIM Inc. All rights reserved.
// </copyright>

namespace StarkBIM.SampleRevitApp.Commands.SampleCmd
{
    using Autofac;

    using JetBrains.Annotations;

    using StarkBIM.SampleRevitApp.Commands.SampleCmd.Services;
    using StarkBIM.SampleRevitApp.Commands.SampleCmd.Services.Impl;

    /// <summary>
    /// Autofac module for the SampleCommand command, indicating which concrete services need to be loaded
    /// </summary>
    public class SampleCommandModule : Module
    {
        /// <inheritdoc />
        protected override void Load([NotNull] ContainerBuilder builder)
        {
            // All of the services are designed so that they can be re-used without needing to create a new instance each request
            // They do not have any fields containing data, except for references to other services
            // This means only a single instance needs to be created for the lifetime of the application
            // Most specific option for registration
            builder.RegisterType<DataTableCreator>().As<IDataTableCreator>().SingleInstance();

            // However, this is fine too, and is easier
            builder.RegisterType<DataWriter>().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<ElementCollector>().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<ElementRetriever>().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<FilePathSelector>().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<RvtClassMapper>().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<ValidSaveFilePathChecker>().AsImplementedInterfaces().SingleInstance();

            // For the viewmodel, since it is not implementing an interface, we call AsSelf()
            // We also omit the SingleInstance() call, since a new instance should be created every request
            builder.RegisterType<EnterCsvFileNameViewModel>().AsSelf();
        }
    }
}