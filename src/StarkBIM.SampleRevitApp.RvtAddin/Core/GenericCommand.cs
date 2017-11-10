// <copyright file="GenericCommand.cs" company="StarkBIM Inc">
// Copyright (c) StarkBIM Inc. All rights reserved.
// </copyright>

namespace StarkBIM.SampleRevitApp.RvtAddin.Core
{
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;

    using Autodesk.Revit.Attributes;
    using Autodesk.Revit.DB;
    using Autodesk.Revit.UI;

    using Autofac;

    using CommonServiceLocator;

    using JetBrains.Annotations;

    using StarkBIM.SampleRevitApp.Commands.Core;

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
    ///     "StarkBIM.RevitAddin1.GenericCommand`1[[BadName]]");
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
            // Do not put this in a using block because then it disposes the scope, which is meant to be the parent lifetime scope
            var lifetimeScope = ServiceLocator.Current.GetInstance<ILifetimeScope>();

            using (ILifetimeScope scope = lifetimeScope.BeginLifetimeScope(b => b.RegisterInstance(commandData).ExternallyOwned()))
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
    }
}
