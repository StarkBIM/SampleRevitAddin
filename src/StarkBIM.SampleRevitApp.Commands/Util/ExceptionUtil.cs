// <copyright file="ExceptionUtil.cs" company="StarkBIM Inc">
// Copyright (c) StarkBIM Inc. All rights reserved.
// </copyright>

namespace StarkBIM.SampleRevitApp.Commands.Util
{
    using System;
    using System.Reflection;
    using System.Text;

    using JetBrains.Annotations;

    using ApplicationException = Autodesk.Revit.Exceptions.ApplicationException;

    /// <summary>
    /// Utility class with helper methods for exceptions
    /// </summary>
    public static class ExceptionUtil
    {
        /// <summary>
        /// Creates the text to be displayed and/or logged for the given exception
        /// </summary>
        /// <param name="ex">The exception</param>
        /// <param name="currentDocumentName">The active document name, optionally</param>
        /// <returns>The exception text</returns>
        [NotNull]
        public static string CreateExceptionMessage([NotNull] this Exception ex, [CanBeNull] string currentDocumentName = null)
        {
            if (ex == null)
            {
                throw new ArgumentNullException(nameof(ex));
            }

            string additionalData = null;

            var additionalStringBuilder = new StringBuilder();

            if (ex.Data.Count > 0)
            {
                additionalStringBuilder.AppendLine("Additional Data from Exception:");

                foreach (object key in ex.Data.Keys)
                {
                    object value = ex.Data[key];

                    additionalStringBuilder.AppendLine($"Key: {key}, Value: {value}");
                }

                additionalData = additionalStringBuilder.ToString();
            }

            string exceptionSpecificData = CreateExceptionSpecificData(ex);

            var stringBuilder = new StringBuilder();

            stringBuilder.AppendLine($"Exception of type: {ex.GetType()}");
            stringBuilder.AppendLine(ex.Message);

            if (currentDocumentName != null)
            {
                stringBuilder.AppendLine($"Active Document: {currentDocumentName}");
            }

            stringBuilder.AppendLine();

            if (!string.IsNullOrWhiteSpace(exceptionSpecificData))
            {
                stringBuilder.AppendLine("Exception specific data:");
                stringBuilder.AppendLine(exceptionSpecificData);
                stringBuilder.AppendLine();
            }

            stringBuilder.AppendLine($"Source: {ex.Source}");
            stringBuilder.AppendLine();

            MethodBase targetSite = ex.TargetSite;
            if (targetSite != null)
            {
                stringBuilder.AppendLine($"Target Site:{targetSite}");
            }

            if (additionalData != null)
            {
                stringBuilder.AppendLine("Additional Data:");
                stringBuilder.AppendLine(additionalData);
                stringBuilder.AppendLine();
            }

            string body = stringBuilder.ToString();

            return body;
        }

        [NotNull]
        private static string CreateExceptionSpecificData([NotNull] Exception ex)
        {
            var exceptionSpecificData = new StringBuilder();

            // ArgumentNullException inherits from ArgumentException, so is included
            if (ex is ArgumentException argumentException)
            {
                exceptionSpecificData.AppendLine("Parameter name: " + argumentException.ParamName);
            }

            if (ex is Autodesk.Revit.Exceptions.ArgumentException rvtArgumentException)
            {
                exceptionSpecificData.AppendLine("Parameter name: " + rvtArgumentException.ParamName);
            }

            if (!(ex is ApplicationException rvtApplicationException))
            {
                return exceptionSpecificData.ToString();
            }

            exceptionSpecificData.AppendLine("Function Id: " + rvtApplicationException.FunctionId);
            exceptionSpecificData.AppendLine("Function File: " + rvtApplicationException.FunctionId.File);
            exceptionSpecificData.AppendLine("Function Name: " + rvtApplicationException.FunctionId.Function);
            exceptionSpecificData.AppendLine("Function Line: " + rvtApplicationException.FunctionId.Line);

            return exceptionSpecificData.ToString();
        }
    }
}