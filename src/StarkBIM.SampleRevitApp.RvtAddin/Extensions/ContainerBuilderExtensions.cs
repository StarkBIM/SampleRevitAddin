// <copyright file="ContainerBuilderExtensions.cs" company="StarkBIM Inc">
// Copyright (c) StarkBIM Inc. All rights reserved.
// </copyright>

namespace StarkBIM.SampleRevitApp.RvtAddin.Extensions
{
    using System;
    using System.Reflection;

    using Autofac;

    using JetBrains.Annotations;

    using StarkBIM.SampleRevitApp.Commands.Core;

    /// <summary>
    /// Extensions methods for Autofac's ContainerBuilder
    /// </summary>
    public static class ContainerBuilderExtensions
    {
        /// <summary>
        ///     Helper method to register the types need for IRvtCommand so that it can be called from either the initial
        ///     CommandModule or during GenericCommand when an updated assembly exists
        /// </summary>
        /// <param name="builder">The container builder</param>
        /// <param name="assembly">The assembly to search</param>
        public static void RegisterCommandTypesForAssembly([NotNull] this ContainerBuilder builder, [NotNull] Assembly assembly)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (assembly == null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }

            // Register all types that implement IRvtCommand
            // AsSelf call registers the command itself, which is what is needed for GenericCommand
            builder.RegisterAssemblyTypes(assembly).Where(t => typeof(IRvtCommand).IsAssignableFrom(t)).AsSelf();

            // Register command properties
            builder.RegisterAssemblyTypes(assembly).Where(t => t.IsAssignableToGenericType(typeof(IRvtCommandProperties<>))).AsSelf().AsImplementedInterfaces();

            // Register all modules in the command assembly, which contain the information needed for each command
            builder.RegisterAssemblyModules(assembly);
        }

        private static bool IsAssignableToGenericType([NotNull] this Type givenType, [NotNull] Type genericType)
        {
            if (givenType == null)
            {
                throw new ArgumentNullException(nameof(givenType));
            }

            if (genericType == null)
            {
                throw new ArgumentNullException(nameof(genericType));
            }

            var interfaceTypes = givenType.GetInterfaces();

            foreach (var it in interfaceTypes)
            {
                if (it.IsGenericType && it.GetGenericTypeDefinition() == genericType)
                {
                    return true;
                }
            }

            if (givenType.IsGenericType && givenType.GetGenericTypeDefinition() == genericType)
            {
                return true;
            }

            Type baseType = givenType.BaseType;
            return baseType != null && IsAssignableToGenericType(baseType, genericType);
        }
    }
}