// <copyright file="App.cs" company="StarkBIM Inc">
// Copyright (c) StarkBIM Inc. All rights reserved.
// </copyright>

namespace StarkBIM.SampleRevitApp.RvtAddin
{
    using System;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Reflection;

    using Autodesk.Revit.DB.Events;
    using Autodesk.Revit.UI;

    using Autofac;

    using AutoMapper;

    using JetBrains.Annotations;

    using StarkBIM.SampleRevitApp.Commands.Core;
    using StarkBIM.SampleRevitApp.Commands.SampleCmd;
    using StarkBIM.SampleRevitApp.Commands.Util;
    using StarkBIM.SampleRevitApp.Helpers;
    using StarkBIM.SampleRevitApp.RvtAddin.Configuration;
    using StarkBIM.SampleRevitApp.RvtAddin.Core;
    using StarkBIM.SampleRevitApp.RvtAddin.Extensions;

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
        /// <summary>
        ///     The directory containing updated assemblies that can be loaded dynamically. Allows commands to be modified without
        ///     reloading Revit
        /// </summary>
        [NotNull]
        public const string UpdatedAssemblyDirName = "UpdatedAssemblies";

        /// <summary>
        ///     The name to register the lifetime scope with. It is important to register the lifetime scope with a tag, otherwise
        ///     the ServiceLocator may retrieve the scope from another addin using this architecture (e.g. StarkBIM tools)
        /// </summary>
        public const string LifetimeScopeName = "SampleRevitApp";

        [CanBeNull]
        private readonly ILifetimeScope _autofacScope;

        [CanBeNull]
        private readonly AssemblyResolver _assemblyResolver;

        [NotNull]
        private readonly string _assemblyDir;

        ////[CanBeNull]
        ////private readonly IDialogService _dialogService;

        /// <summary>
        ///     Initializes a new instance of the <see cref="App" /> class.
        ///     Initialize as much logic in the constructor as possible
        /// </summary>
        public App()
        {
            Instance = this;

            try
            {
                AppDomain.CurrentDomain.UnhandledException += CurrentDomainOnUnhandledException;

                string location = Assembly.GetExecutingAssembly().Location;
                _assemblyDir = Path.GetDirectoryName(location).ThrowIfNull();

                _assemblyResolver = new AssemblyResolver(_assemblyDir);
                AppDomain.CurrentDomain.AssemblyResolve += _assemblyResolver.OnAssemblyResolve;

                var containerBuilder = new ContainerBuilder();

                containerBuilder.RegisterModule<CommandModule>();

                var container = containerBuilder.Build();

                _autofacScope = container.BeginLifetimeScope("SampleRevitApp");

                // The service locator is required in classes that are not initiated by this program
                // It is used for the GenericCommand class because we do not control its initialization
                ////var autofacServiceLocator = new AutofacServiceLocator(_autofacScope);
                ////ServiceLocator.SetLocatorProvider(() => autofacServiceLocator);

                ////_dialogService = _autofacScope.Resolve<IDialogService>();
                ////_autofacScope.Resolve<ILogService>();

                Mapper.Initialize(cfg => cfg.AddProfile<ElementProfile>());

#if DEBUG
                Mapper.AssertConfigurationIsValid();
#endif
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        /// <summary>
        ///     Gets the LifetimeScope instance for this application. ServiceLocator is throwing strange exceptions
        /// </summary>
        [CanBeNull]
        public static ILifetimeScope LifetimeScope => Instance.ThrowIfNull()._autofacScope;

        [CanBeNull]
        private static App Instance { get; set; }

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
            try
            {
                CleanUpUpdatedAssembliesDir();

                RibbonPanel ribbonPanel = application.CreateRibbonPanel(Tab.AddIns, "Sample");

                ribbonPanel.AddItem(CreatePushButtonData<SampleCommand>());

                application.ControlledApplication.ApplicationInitialized += ControlledApplicationOnApplicationInitialized;

                return Result.Succeeded;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
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
        private static void CurrentDomainOnUnhandledException([NotNull] object sender, [NotNull] UnhandledExceptionEventArgs e)
        {
            ////string programClosing = e.IsTerminating ? "Program is terminating" : "Program is not terminating";

            // Use the instance here, because there is a chance the static class has not yet been initialized
            ////_logService?.LogCritical($"Unhandled exception of type {sender}. {programClosing}.");

            if (!(e.ExceptionObject is Exception exception))
            {
                return;
            }

            TaskDialog.Show("Unhandled Exception", exception.CreateExceptionMessage());
        }

        private void CleanUpUpdatedAssembliesDir()
        {
            // On Startup, we want to clear the UpdatedAssemblies directory
            var updatedAssembliesDir = Path.Combine(_assemblyDir, UpdatedAssemblyDirName);

            if (!Directory.Exists(updatedAssembliesDir))
            {
                return;
            }

            var filePaths = Directory.GetFiles(updatedAssembliesDir);

            foreach (var filePath in filePaths)
            {
                try
                {
                    File.Delete(filePath);
                }
                catch (Exception ex)
                {
                    // It's not the end of the world if this fails
                    Debug.WriteLine(ex.CreateExceptionMessage());
                }
            }

            var subdirectories = Directory.GetDirectories(updatedAssembliesDir);

            foreach (var filePath in subdirectories)
            {
                try
                {
                    Directory.Delete(filePath, true);
                }
                catch (Exception ex)
                {
                    // It's not the end of the world if this fails
                    Debug.WriteLine(ex.CreateExceptionMessage());
                }
            }
        }

        /// <summary>
        ///     Creates a pushbutton for an RvtCommand
        /// </summary>
        /// <typeparam name="TCommand">The type of the command</typeparam>
        /// <returns>A PushbuttonData object for the command</returns>
        [NotNull]
        private PushButtonData CreatePushButtonData<TCommand>()
            where TCommand : class, IRvtCommand
        {
            var commandProperties = _autofacScope.Resolve<IRvtCommandProperties<TCommand>>();

            var pushButtonData = new PushButtonData(
                                                    commandProperties.Name,
                                                    commandProperties.DisplayName,
                                                    typeof(GenericCommand<>).Assembly.Location,
                                                    typeof(GenericCommand<TCommand>).FullName)
                .SetAllPushButtonDataProperties(commandProperties);

            return pushButtonData;
        }
    }
}