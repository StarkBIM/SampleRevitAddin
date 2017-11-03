// <copyright file="AssemblyResolver.cs" company="StarkBIM Inc">
// Copyright (c) StarkBIM Inc. All rights reserved.
// </copyright>

namespace StarkBIM.SampleRevitApp.RvtAddin.Core
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Reflection;

    using JetBrains.Annotations;

    using StarkBIM.SampleRevitApp.Helpers;

    /// <summary>
    ///     A helper class for the AppDomain AssemblyResolve event.
    ///     Initialize the class with the desired directories that should be searched for assemblies
    ///     Then, wire up the event handler to the OnAssemblyResolve method
    ///     Example:
    ///     <code>
    /// string location = Assembly.GetExecutingAssembly().Location;
    ///             var assemblyResolver = new AssemblyResolver(Path.GetDirectoryName(location).ThrowIfNull());
    ///             AppDomain.CurrentDomain.AssemblyResolve += _assemblyResolver.OnAssemblyResolve;
    /// </code>
    /// </summary>
    public class AssemblyResolver
    {
        [NotNull]
        private readonly Dictionary<string, Assembly> _assembliesDictionary = new Dictionary<string, Assembly>();

        [NotNull]
        [ItemNotNull]
        private readonly List<string> _directoryNames = new List<string>();

        /// <summary>
        ///     Initializes a new instance of the <see cref="AssemblyResolver" /> class.
        /// </summary>
        /// <param name="directoryName">The first directory that should be searched for assemblies</param>
        /// <param name="directories">Additional directories that should be searched for assemblies</param>
        public AssemblyResolver([NotNull] string directoryName, [NotNull] [ItemNotNull] params string[] directories)
            : this(new[] { directoryName}.Union(directories).ToList())
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="AssemblyResolver" /> class.
        /// </summary>
        /// <param name="directories">Additional directories that should be searched for assemblies</param>
        public AssemblyResolver([NotNull][ItemNotNull] ICollection<string> directories)
        {
            if (directories == null)
            {
                throw new ArgumentNullException(nameof(directories));
            }

            if (directories.Count == 0)
            {
                throw new ArgumentException("At least one directory must be provided", nameof(directories));
            }

            if (directories.Any(d => d.IsNullOrWhiteSpace()))
            {
                throw new ArgumentException("All directories must be non-null", nameof(directories));
            }

            _directoryNames.AddRange(directories);
        }

        /// <summary>
        ///     The event handler for an AppDomain AssemblyResolve event
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="args">The event arguments</param>
        /// <returns>The assembly, if resolved. Otherwise null</returns>
        [CanBeNull]
        public Assembly OnAssemblyResolve([NotNull] object sender, [NotNull] ResolveEventArgs args)
        {
            string assemblyName = GetAssemblyName(args);

            if (_assembliesDictionary.TryGetValue(assemblyName, out Assembly dictionaryAssembly))
            {
                // Can be null
                return dictionaryAssembly;
            }

            foreach (var directoryName in _directoryNames)
            {
                Assembly assembly = GetAssemblyFromDirectory(directoryName, assemblyName);

                if (assembly == null)
                {
                    continue;
                }

                _assembliesDictionary.Add(assemblyName, assembly);
                return assembly;
            }

            _assembliesDictionary.Add(assemblyName, null);
            return null;
        }

        [CanBeNull]
        private static Assembly GetAssemblyFromDirectory([NotNull] string directoryName, [NotNull] string assemblyName)
        {
            string path = Path.Combine(directoryName, assemblyName + ".dll");

            if (!File.Exists(path))
            {
                // Rare, but some assemblies (e.g. old PDFCreator versions) are contained in the exe
                path = Path.Combine(directoryName, assemblyName + ".exe");
                if (!File.Exists(path))
                {
                    return null;
                }
            }

            try
            {
                Assembly assembly = Assembly.LoadFrom(path);

                return string.Equals(
                                     GetAssemblyNameFromFullName(assembly.FullName),
                                     assemblyName,
                                     StringComparison.Ordinal)
                           ? assembly
                           : null;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return null;
            }
        }

        [NotNull]
        private static string GetAssemblyName([NotNull] ResolveEventArgs args)
        {
            string argsName = args.Name;

            string name = argsName.IndexOf(",", StringComparison.Ordinal) > -1
                              ? GetAssemblyNameFromFullName(argsName)
                              : argsName;
            return name;
        }

        [NotNull]
        private static string GetAssemblyNameFromFullName([NotNull] string argsName)
        {
            string name = argsName.Substring(0, argsName.IndexOf(",", StringComparison.Ordinal));

            return name;
        }
    }
}