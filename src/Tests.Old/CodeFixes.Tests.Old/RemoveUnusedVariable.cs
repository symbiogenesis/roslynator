﻿// Copyright (c) .NET Foundation and Contributors. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Text;

namespace Roslynator.CSharp.CodeFixes.Tests
{
    internal static class RemoveUnusedVariable
    {
        private static object Foo()
        {
            object x = null;

            try
            {
            }
            catch (Exception ex)
            {
            }

            object x1 = null, x2 = null;

            return x1;

            void LocalFunction()
            {
            }
        }
    }
}
