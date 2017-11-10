// <copyright file="RvtClassMapperStub.cs" company="StarkBIM Inc">
// Copyright (c) StarkBIM Inc. All rights reserved.
// </copyright>

namespace StarkBIM.SampleRevitApp.Commands.SampleCmd.Services.Impl
{
    using System;

    /// <summary>
    ///     Do not use this class
    ///     Stub class to demonstrate Step 2 of the TDD example.
    /// </summary>
    public class RvtClassMapperStub : IRvtClassMapper
    {
        /// <inheritdoc />
        public Type GetMappedType<TRvt>()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public Type GetMappedType(Type type)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public Type GetMappedType<TRvt>(TRvt rvtObject)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public T Map<T>(object nativeRvtObject)
        {
            throw new NotImplementedException();
        }
    }
}