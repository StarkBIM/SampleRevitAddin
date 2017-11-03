// <copyright file="RvtAddinFixture.cs" company="StarkBIM Inc">
// Copyright (c) StarkBIM Inc. All rights reserved.
// </copyright>

namespace StarkBIM.SampleRevitApp.RvtAddin.Test
{
    using System;
    using System.IO;

    using AutoMapper;

    using JetBrains.Annotations;

    using StarkBIM.SampleRevitApp.Model.Util;
    using StarkBIM.SampleRevitApp.RvtAddin.Configuration;
    using StarkBIM.SampleRevitApp.RvtAddin.Core;

    /// <summary>
    ///     Fixture that sets up the assembly resolve event to find Revit assemblies
    /// </summary>
    public sealed class RvtAddinFixture : IDisposable
    {
        [NotNull]
        private readonly AssemblyResolver _assemblyResolver;

        /// <summary>
        ///     Initializes a new instance of the <see cref="RvtAddinFixture" /> class.
        /// </summary>
        public RvtAddinFixture()
        {
            string programFiles = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);

            string autodeskFolder = Path.Combine(programFiles, "Autodesk");

            string rvtVersionString = RvtVersionUtil.GetRvtVersionString();

            var revitFolder = $"Revit {rvtVersionString}";

            var fullRevitFolder = Path.Combine(autodeskFolder, revitFolder);

            _assemblyResolver = new AssemblyResolver(fullRevitFolder);

            AppDomain.CurrentDomain.AssemblyResolve += _assemblyResolver.OnAssemblyResolve;

            // Must come after the AssemblyResolve event handler
            Mapper.Initialize(cfg => cfg.AddProfile<MainProfile>());
        }

        /// <summary>
        ///     Finalizes an instance of the <see cref="RvtAddinFixture" /> class.
        /// </summary>
        ~RvtAddinFixture()
        {
            Dispose(false);
        }

        /// <inheritdoc />
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            AppDomain.CurrentDomain.AssemblyResolve -= _assemblyResolver.OnAssemblyResolve;

            if (disposing)
            {
            }
        }
    }
}