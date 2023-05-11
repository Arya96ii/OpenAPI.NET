﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. 

using System.Collections.Generic;
using Microsoft.OpenApi.Models;
using Microsoft.OpenApi.Readers.Interface;

namespace Microsoft.OpenApi.Readers
{
    /// <summary>
    /// Object containing all diagnostic information related to Open API parsing.
    /// </summary>
    public class OpenApiDiagnostic : IDiagnostic
    {
        /// <summary>
        /// List of all errors.
        /// </summary>
        public IList<OpenApiError> Errors { get; set; } = new List<OpenApiError>();

        /// <summary>
        /// List of all warnings
        /// </summary>
        public IList<OpenApiError> Warnings { get; set; } = new List<OpenApiError>();

        /// <summary>
        /// Open API specification version of the document parsed.
        /// </summary>
        public OpenApiSpecVersion SpecificationVersion { get; set; }

        /// <summary>
        /// Append another set of diagnostic Errors and Warnings to this one, this may be appended from another external
        /// document's parsing and we want to indicate which file it originated from.
        /// </summary>
        /// <param name="diagnosticToAdd"></param>
        /// <param name="fileNameToAdd"></param>
        public void AppendDiagnostic(OpenApiDiagnostic diagnosticToAdd, string fileNameToAdd)
        {
            foreach (var err in diagnosticToAdd.Errors)
            {
                string errMsgWithFileName;
                if (!(string.IsNullOrEmpty(fileNameToAdd) || string.IsNullOrWhiteSpace(fileNameToAdd)))
                {
                    errMsgWithFileName = $"[File: {fileNameToAdd}] {err.Message}";
                }
                else
                {
                    errMsgWithFileName = err.Message;
                }
                Errors.Add(new OpenApiError(err.Pointer, errMsgWithFileName));
            }
            foreach (var warn in diagnosticToAdd.Warnings)
            {
                string warnMsgWithFileName;
                if (!(string.IsNullOrEmpty(fileNameToAdd) || string.IsNullOrWhiteSpace(fileNameToAdd)))
                {
                    warnMsgWithFileName = $"[File: {fileNameToAdd}] {warn.Message}";
                }
                else
                {
                    warnMsgWithFileName = warn.Message;
                }
                Errors.Add(new OpenApiError(warn.Pointer, warnMsgWithFileName));
            }
        }
    }
}

/// <summary>
/// Extension class for IList to add the Method "AddRange" used above
/// </summary>
public static class IDiagnosticExtensions
{
    /// <summary>
    /// Extension method for IList so that another list can be added to the current list.
    /// </summary>
    /// <param name="collection"></param>
    /// <param name="enumerable"></param>
    /// <typeparam name="T"></typeparam>
    public static void AddRange<T>(this ICollection<T> collection, IEnumerable<T> enumerable)
    {
        foreach (var cur in enumerable)
        {
            collection.Add(cur);
        }
    }
}
