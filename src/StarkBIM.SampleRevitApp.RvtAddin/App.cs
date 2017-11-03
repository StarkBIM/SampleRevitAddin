// <copyright file="App.cs" company="StarkBIM Inc">
// Copyright (c) StarkBIM Inc. All rights reserved.
// </copyright>

namespace StarkBIM.SampleRevitApp.RvtAddin
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Reflection;

    using Autodesk.Revit.DB.Events;
    using Autodesk.Revit.UI;

    using Autofac;

    using AutoMapper;

    using JetBrains.Annotations;

    using StarkBIM.SampleRevitApp.Helpers;
    using StarkBIM.SampleRevitApp.Model.Core;
    using StarkBIM.SampleRevitApp.Model.Extensions;
    using StarkBIM.SampleRevitApp.Model.Services;
    using StarkBIM.SampleRevitApp.RvtAddin.Configuration;
    using StarkBIM.SampleRevitApp.RvtAddin.Core;

    /// <summary>
    ///     The main application class loaded by Revit
    /// </summary>
    /// <remarks>
    ///     Add commands to the menu in the OnStartup class, otherwise they will not be available to add as shortcuts and will
    ///     not permanently stay in the Quick Launch toolbar
    ///     Other functions can be performed in the ApplicationInitialized event handler
    /// </remarks>
    public class App : IExternalApplication
    {
        [CanBeNull]
        private readonly ILifetimeScope _autofacScope;

        [CanBeNull]
        private readonly AssemblyResolver _assemblyResolver;

        [CanBeNull]
        private readonly IDialogService _dialogService;

        [CanBeNull]
        private readonly ILogService _logService;

        /// <summary>
        /// Initializes a new instance of the <see cref="App"/> class.
        /// Initialize as much logic in the constructor as possible
        /// </summary>
        public App()
        {
            AppDomain.CurrentDomain.UnhandledException += CurrentDomainOnUnhandledException;

            string location = Assembly.GetExecutingAssembly().Location;
            _assemblyResolver = new AssemblyResolver(Path.GetDirectoryName(location).ThrowIfNull());
            AppDomain.CurrentDomain.AssemblyResolve += _assemblyResolver.OnAssemblyResolve;

            var containerBuilder = new ContainerBuilder();

            containerBuilder.RegisterModule<CommandModule>();
            containerBuilder.RegisterModule<MainModule>();

            var container = containerBuilder.Build();

            _autofacScope = container.BeginLifetimeScope();

            _dialogService = _autofacScope.Resolve<IDialogService>();
            _logService = _autofacScope.Resolve<ILogService>();

            Mapper.Initialize(cfg => cfg.AddProfile<MainProfile>());
        }

        /// <inheritdoc />
        public Result OnShutdown([NotNull] UIControlledApplication application)
        {
            _autofacScope?.Dispose();

            if (_assemblyResolver != null)
            {
                AppDomain.CurrentDomain.AssemblyResolve -= _assemblyResolver.OnAssemblyResolve;
            }

            application.ControlledApplication.ApplicationInitialized -= ControlledApplicationOnApplicationInitialized;

            AppDomain.CurrentDomain.UnhandledException -= CurrentDomainOnUnhandledException;

            return Result.Succeeded;
        }

        /// <summary>
        ///     The OnStartup method called by Revit when the addin is loaded.
        ///     The only logic performed here is event registration and menu construction
        ///     All non-Revit-dependent functionality has been initialized in the constructor
        /// </summary>
        /// <param name="application">The UI controlled application object</param>
        /// <returns>The result, always Succeeded unless an exception is thrown</returns>
        public Result OnStartup([NotNull] UIControlledApplication application)
        {
            application.ControlledApplication.ApplicationInitialized += ControlledApplicationOnApplicationInitialized;

            return Result.Succeeded;
        }

        private static void ControlledApplicationOnApplicationInitialized([NotNull] object sender, [NotNull] ApplicationInitializedEventArgs applicationInitializedEventArgs)
        {
            // If the Application or UIApplication objects are needed in any services, they can be added to the IoC container as follows
            // The method should be made non-static if this is needed
            /*
            var application = (Application)sender;
            var uiApplication = new UIApplication(application);

            var containerBuilder = new ContainerBuilder();

            containerBuilder.RegisterInstance(application).SingleInstance();
            containerBuilder.RegisterInstance(uiApplication).SingleInstance();

            containerBuilder.RegisterModule<CommandModule>();
            containerBuilder.RegisterModule<MainModule>();

            var container = containerBuilder.Build();

            _autofacScope = container.BeginLifetimeScope();
            */
        }

        [SuppressMessage(
            "ReSharper",
            "ConstantConditionalAccessQualifier",
            Justification = "Could potentially be called before the services are initialized")]
        private void CurrentDomainOnUnhandledException([NotNull] object sender, [NotNull] UnhandledExceptionEventArgs e)
        {
            var exception = e.ExceptionObject as Exception;

            string programClosing = e.IsTerminating ? "Program is terminating" : "Program is not terminating";

            // Use the instance here, because there is a chance the static class has not yet been initialized
            _logService?.LogCritical($"Unhandled exception of type {sender}. {programClosing}.");

            if (exception == null)
            {
                return;
            }

            _dialogService?.NotifyException(exception, null, LogLevel.Critical);
        }
    }
}