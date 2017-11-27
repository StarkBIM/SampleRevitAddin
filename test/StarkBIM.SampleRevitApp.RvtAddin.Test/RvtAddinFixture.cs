// <copyright file="RvtAddinFixture.cs" company="StarkBIM Inc">
// Copyright (c) StarkBIM Inc. All rights reserved.
// </copyright>

namespace StarkBIM.SampleRevitApp.RvtAddin.Test
{
    using System;
    using System.IO;

    using JetBrains.Annotations;

    using StarkBIM.SampleRevitApp.Commands.Core;
    using StarkBIM.SampleRevitApp.Commands.Util;
    using StarkBIM.SampleRevitApp.Helpers;

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
            RvtMapper.Initialize(cfg => cfg.AddProfile<ElementProfile>());
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