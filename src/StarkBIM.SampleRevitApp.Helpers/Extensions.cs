// <copyright file="Extensions.cs" company="StarkBIM Inc">
// Copyright (c) StarkBIM Inc. All rights reserved.
// </copyright>

namespace StarkBIM.SampleRevitApp.Helpers
{
    using System;
    using System.Reflection;

    using JetBrains.Annotations;

    /// <summary>
    /// Helper extension methods
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        ///     Throws an ArgumentNullException when passed a null value.
        ///     This function only works for values that are reference types.
        ///     Use <see cref="ThrowIfNullAnyType{T}" /> if the type of the item is unknown
        /// </summary>
        /// <typeparam name="T">The type of the item. Must be a reference type</typeparam>
        /// <param name="item">The item</param>
        /// <returns>The item if it is not null. Otherwise throws an ArgumentNullException</returns>
        [NotNull]
        [ContractAnnotation("notnull => notnull; null => halt")]
        public static T ThrowIfNull<T>([CanBeNull] this T item)
            where T : class
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            return item;
        }

        /// <summary>
        ///     Throws an ArgumentNullException when passed a null value.
        ///     This function only works for values of any type.
        ///     If it is known that the value is a reference type, use <see cref="ThrowIfNull{T}" />
        ///     Note that the default value of a value type will not cause the exception to be thrown, only null itself.
        /// </summary>
        /// <typeparam name="T">The type of the item</typeparam>
        /// <param name="item">The item</param>
        /// <returns>The item if it is not null. Otherwise throws an ArgumentNullException</returns>
        [NotNull]
        [ContractAnnotation("notnull => notnull; null => halt")]
        public static T ThrowIfNullAnyType<T>([CanBeNull] this T item)
        {
            if (item.IsNullIfReferenceType())
            {
                throw new ArgumentNullException(nameof(item));
            }

            return item;
        }

        /// <summary>
        ///     Checks whether a passed value is null if that value is a reference type. There are situations where the default
        ///     value of a value type should not be treated the same way as null.
        ///     This provides a way of checking that a passed value of an unknown type, that could be a reference or value type, is
        ///     null without needing to box a value type.
        /// </summary>
        /// <typeparam name="T">The type of the value</typeparam>
        /// <param name="value">The value</param>
        /// <returns>True if the value is null, otherwise false</returns>
        [ContractAnnotation("null=>true;notnull=>false;")]
        public static bool IsNullIfReferenceType<T>([CanBeNull] this T value)
        {
            // It should be faster to check that the type is non-nullable through the GetTypeInfo call rather than to box the key to check for null
            // Checking for default is not a valid option here. The default value could be a legitimate key, whereas null cannot
            // The check for generic handles nullables, which are value types.
            TypeInfo typeInfo = typeof(T).GetTypeInfo();
            return (!typeInfo.IsValueType || typeInfo.IsGenericType) && value == null;
        }

        /// <summary>
        /// Extension method to check if an object is null
        /// </summary>
        /// <param name="item">The object</param>
        /// <returns>True if null, otherwise false</returns>
        [ContractAnnotation("null=>true;notnull=>false;")]
        public static bool IsNull([CanBeNull] this object item) => item == null;

        /// <summary>
        ///     Extension method to check is a string is null or whitespace
        /// </summary>
        /// <param name="str">The string</param>
        /// <returns>True if null or whitespace, otherwise false</returns>
        [ContractAnnotation("null => true")]
        public static bool IsNullOrWhiteSpace([CanBeNull] this string str) => string.IsNullOrWhiteSpace(str);
    }
}