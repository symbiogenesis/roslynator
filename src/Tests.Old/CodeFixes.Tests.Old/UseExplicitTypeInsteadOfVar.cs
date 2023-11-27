﻿// Copyright (c) .NET Foundation and Contributors. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace Roslynator.CSharp.CodeFixes.Tests
{
    internal static class UseExplicitTypeInsteadOfVar
    {
        private static void Foo()
        {
            var x = "", y = "";

            const var i = 0;
        }
    }
}
