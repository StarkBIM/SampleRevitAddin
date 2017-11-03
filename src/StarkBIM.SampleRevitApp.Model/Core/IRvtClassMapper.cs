// <copyright file="IRvtClassMapper.cs" company="StarkBIM Inc">
// Copyright (c) StarkBIM Inc. All rights reserved.
// </copyright>

namespace StarkBIM.SampleRevitApp.Model.Core
{
    using System;

    using JetBrains.Annotations;

    /// <summary>
    /// Defines a class that maps Revit objects to objects in our data model
    /// </summary>
    /// <example>
    /// Example:
    /// Consider an implementation of IRvtClassMapper that has the following mappings defined
    /// (Revit) -> (Our Classes)
    /// Element -> Element
    /// View -> View
    /// ViewSheet -> Sheet
    ///
    /// A Revit ViewSheet object will be returned as Sheet
    /// A Revit View3D object will be returned as View, because we have not defined a more specific View3D class of our own, but View is defined, so Element is not returned
    /// A Revit Viewport class will be returned as Element, because no Viewport class is defined
    /// A DefinitionFlie class will throw an exception, because no suitable mapping exists for it
    /// </example>
    public interface IRvtClassMapper
    {
        /*
        /// <summary>
        /// Maps a Revit object to an unknown type.
        /// The object will be returned as the base class RvtObject, but will be instantiated as the most defined version of the object possible
        /// Use GetMappedType to see if a mapping is defined for an object
        /// </summary>
        /// <param name="nativeRvtObject">The Revit object to be mapped</param>
        /// <exception cref="ArgumentNullException">The given object is null</exception>
        /// <exception cref="ArgumentException">The given object does not have a valid type mapping defined</exception>
        /// <returns>The mapped object. Will not return null - throws an exception if a valid mapping could not be found</returns>
        [NotNull]
        [MustUseReturnValue]
        object Map([NotNull] object nativeRvtObject);*/

        /// <summary>
        /// Maps a Revit object to a known type.
        /// The object will be returned as an object of type T, but will be instantiated as the most defined version of the object possible
        /// Use GetMappedType to see if a mapping is defined for an object
        /// </summary>
        /// <param name="nativeRvtObject">The Revit object to be mapped</param>
        /// <typeparam name="T">The type that the Revit object will be mapped to</typeparam>
        /// <exception cref="ArgumentNullException">The given object is null</exception>
        /// <exception cref="ArgumentException">The given object does not have a valid type mapping defined</exception>
        /// <exception cref="ArgumentException">The given object does not have a mapping that corresponds to the type parameter</exception>
        /// <returns>The mapped object. Will not return null - throws an exception if a valid mapping could not be found</returns>
        [NotNull]
        [MustUseReturnValue]
        T Map<T>([NotNull] object nativeRvtObject);

        /// <summary>
        /// Gets the defined mapping for a given Revit object. Returns null if no mapping is defined
        /// </summary>
        /// <remarks>
        /// If an object is available, use the GetMapperType(TRvt rvtObject) method to avoid needing to manually define TRvt
        /// </remarks>
        /// <typeparam name="TRvt">The type of the Revit object</typeparam>
        /// <returns>The mapped type if one exists, otherwise null</returns>
        [CanBeNull]
        [MustUseReturnValue]
        Type GetMappedType<TRvt>();

        /// <summary>
        /// Gets the defined mapping for a given Revit object. Returns null if no mapping is defined
        /// </summary>
        /// <remarks>
        /// If an object is available, use the GetMapperType(TRvt rvtObject) method to avoid needing to manually define TRvt
        /// </remarks>
        /// <param name="type">The type of the Revit object</param>
        /// <exception cref="ArgumentNullException">The given type is null</exception>
        /// <returns>The mapped type if one exists, otherwise null</returns>
        [CanBeNull]
        [MustUseReturnValue]
        Type GetMappedType([NotNull] Type type);

        /// <summary>
        /// Gets the defined mapping for a given Revit object. Returns null if no mapping is defined
        /// </summary>
        /// <remarks>
        /// Use this method to take advantage of type inference and to avoid needing to enter the type manually</remarks>
        /// <typeparam name="TRvt">The type of the Revit object</typeparam>
        /// <param name="rvtObject">The Revit object</param>
        /// <exception cref="ArgumentNullException">The given object is null</exception>
        /// <returns>The mapped type if one exists, otherwise null</returns>
        [CanBeNull]

        Type GetMappedType<TRvt>([NotNull] TRvt rvtObject);
    }
}