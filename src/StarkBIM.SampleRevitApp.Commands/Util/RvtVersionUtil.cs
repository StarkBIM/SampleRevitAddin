// <copyright file="RvtVersionUtil.cs" company="StarkBIM Inc">
// Copyright (c) StarkBIM Inc. All rights reserved.
// </copyright>

namespace StarkBIM.SampleRevitApp.Commands.Util
{
    using JetBrains.Annotations;

    /// <summary>
    /// Helper methods for Revit version-specific functions
    /// </summary>
    public static class RvtVersionUtil
    {
        /// <summary>
        /// Gets a string representing the year of the running Revit version (YYYY)
        /// </summary>
        /// <returns>A string representing the year of the running Revit version (YYYY)</returns>
        [NotNull]
        [MustUseReturnValue]
        public static string GetRvtVersionString()
        {
#if RVT2015
            return "2015";
#elif RVT2016
            return "2016";
#elif RVT2017
            return "2017";
#elif RVT2018
            return "2018";
#else
            throw new InvalidOperationException("Could not detect Revit version");
#endif
        }
    }
}
