// <copyright file="RvtMapper.cs" company="StarkBIM Inc">
// Copyright (c) StarkBIM Inc. All rights reserved.
// </copyright>

namespace StarkBIM.SampleRevitApp.Commands.Core
{
    using System;

    using AutoMapper;

    using JetBrains.Annotations;

    using StarkBIM.SampleRevitApp.Helpers;

    /// <summary>
    ///     A class that provides access to the instance of AutoMapper for this addin. Done to avoid conflict with other addins
    ///     using AutoMapper
    /// </summary>
    public static class RvtMapper
    {
        [CanBeNull]
        private static IMapper _instance;

        /// <summary>
        ///     Gets the mapper instance
        /// </summary>
        [NotNull]
        public static IMapper Instance => _instance.ThrowIfNull();

        /// <summary>
        ///     Initializes the mapper with the given configuration
        /// </summary>
        /// <param name="mapperConfiguration">The mapper configuration</param>
        public static void Initialize([NotNull] MapperConfiguration mapperConfiguration)
        {
            if (mapperConfiguration == null)
            {
                throw new ArgumentNullException(nameof(mapperConfiguration));
            }

            if (_instance != null)
            {
                throw new InvalidOperationException("The mapper has already been configured");
            }

            _instance = new Mapper(mapperConfiguration);
        }

        /// <summary>
        /// Initializes the mapper with the given configuration expression
        /// </summary>
        /// <param name="configurationAction">The configuration expression</param>
        public static void Initialize([NotNull] Action<IMapperConfigurationExpression> configurationAction)
        {
            if (configurationAction == null)
            {
                throw new ArgumentNullException(nameof(configurationAction));
            }

            var conf = new MapperConfiguration(configurationAction);

            Initialize(conf);
        }
    }
}