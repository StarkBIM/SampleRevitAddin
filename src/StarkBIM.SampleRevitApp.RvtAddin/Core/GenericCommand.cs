// <copyright file="GenericCommand.cs" company="StarkBIM Inc">
// Copyright (c) StarkBIM Inc. All rights reserved.
// </copyright>

namespace StarkBIM.SampleRevitApp.RvtAddin.Core
{
    using System;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Linq;
    using System.Reflection;

    using Autodesk.Revit.Attributes;
    using Autodesk.Revit.DB;
    using Autodesk.Revit.UI;

    using Autofac;

    using JetBrains.Annotations;

    using StarkBIM.SampleRevitApp.Commands.Core;
    using StarkBIM.SampleRevitApp.Commands.Util;
    using StarkBIM.SampleRevitApp.Helpers;
    using StarkBIM.SampleRevitApp.RvtAddin.Extensions;

    /// <summary>
    ///     The generic command means that we don't need to tag each class with the Transaction attribute.
    ///     It also means that commands don't need to be kept in the RvtAddin assembly.
    /// </summary>
    /// <typeparam name="TCommand">The type of the command runner</typeparam>
    /// <remarks>
    ///     Tried to see if it was possible to call a class that doesn't exist, but it didn't work
    ///     // Tried to trigger the TypeResolve event using this, but it didn't work
    ///     ////var button3Data = new PushButtonData("Button3", "Button3", asmName, "BadName");
    ///     // This also doesn't trigger the TypeResolve event. There are no other AppDomains (at least accessible from here)
    ///     ////var button3Data = new PushButtonData("Button3", "Button3", asmName,
    ///     "StarkBIM.SampleRevitApp.RvtAddin.GenericCommand`1[[BadName]]");
    ///     ////ribbonPanel.AddItem(button3Data);
    /// </remarks>
    [Transaction(TransactionMode.Manual)]
    public class GenericCommand<TCommand> : IExternalCommand
        where TCommand : class, IRvtCommand
    {
        /// <inheritdoc />
        [SuppressMessage("ReSharper", "RedundantAssignment", Justification = "The message parameter is only for returning data")]
        public Result Execute(
            [NotNull] ExternalCommandData commandData,
            [CanBeNull] ref string message,
            [CanBeNull] [ItemNotNull] ElementSet elements)
        {
            var updatedAssembly = GetUpdatedAssemblyIfNewer();

            ////var allScopes = ServiceLocator.Current.GetAllInstances<ILifetimeScope>().ToList();

            // Do not put this in a using block because then it disposes the scope, which is meant to be the parent lifetime scope
            ////var lifetimeScope = ServiceLocator.Current.GetInstance<ILifetimeScope>(App.LifetimeScopeName);

            var lifetimeScope = App.LifetimeScope.ThrowIfNull();

            using (ILifetimeScope scope = lifetimeScope.BeginLifetimeScope(
                                                                           b =>
                                                                               {
                                                                                   b.RegisterInstance(commandData).ExternallyOwned();

                                                                                   if (updatedAssembly != null)
                                                                                   {
                                                                                       b.RegisterCommandTypesForAssembly(updatedAssembly);
                                                                                   }
                                                                               }))
            {
                // This initializes the command with all the necessary services
                var commandRunner = scope.Resolve<TCommand>();

                RvtCommandResult result = commandRunner.Run(commandData);

                message = result.Message;

                var rvtResult = (Result)result.RvtResult;

                if (!result.Elements.Any())
                {
                    return rvtResult;
                }

                if (elements == null)
                {
                    elements = new ElementSet();
                }

                foreach (var element in result.Elements)
                {
                    elements.Insert(element);
                }

                return rvtResult;
            }
        }

        [CanBeNull]
        private static Assembly GetUpdatedAssemblyIfNewer()
        {
            if (!IsDebugEnabled())
            {
                return null;
            }

            // Check for updated assemblies
            string assemblyLocation = Assembly.GetExecutingAssembly().Location;

            string directoryName = Path.GetDirectoryName(assemblyLocation);

            if (directoryName.IsNullOrWhiteSpace())
            {
                return null;
            }

            var updatedAssembliesDir = Path.Combine(directoryName, App.UpdatedAssemblyDirName);

            if (!Directory.Exists(updatedAssembliesDir))
            {
                return null;
            }

            var commandType = typeof(TCommand);

            var commandAssemblyFileName = Path.GetFileName(commandType.Assembly.Location);

            // Get any file starting with the DLL's filename
            // Needs to be recursive because this directory will only contain folders that each have a copy of the build
            var lastModifiedMatchingFile = Directory.GetFiles(updatedAssembliesDir, commandAssemblyFileName, SearchOption.AllDirectories)
                .Select(path => new FileInfo(path))
                .OrderByDescending(fileInfo => fileInfo.LastWriteTime)
                .FirstOrDefault();

            if (lastModifiedMatchingFile == null)
            {
                return null;
            }

            try
            {
                Assembly loadFile = Assembly.LoadFile(lastModifiedMatchingFile.FullName);
                return loadFile;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.CreateExceptionMessage());
                return null;
            }
        }

#if DEBUG
        private static bool IsDebugEnabled() => true;
#else
        private static bool IsDebugEnabled() => false;
#endif
    }
}