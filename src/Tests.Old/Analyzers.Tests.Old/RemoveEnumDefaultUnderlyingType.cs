﻿// Copyright (c) .NET Foundation and Contributors. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace Roslynator.CSharp.Analyzers.Tests
{
    internal static class RemoveEnumDefaultUnderlyingType
    {
        private enum Foo : int
        {
        }
    }
}
