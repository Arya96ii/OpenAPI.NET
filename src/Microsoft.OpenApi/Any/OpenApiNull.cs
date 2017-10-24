﻿//---------------------------------------------------------------------
// <copyright file="OpenApiNull.cs" company="Microsoft">
//      Copyright (C) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.
// </copyright>
//---------------------------------------------------------------------

namespace Microsoft.OpenApi
{
    public class OpenApiNull : IOpenApiAny
    {
        public AnyTypeKind AnyKind { get; } = AnyTypeKind.Null;
    }
}