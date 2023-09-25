﻿// Copyright (c) .NET Foundation and Contributors. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Text;

namespace Roslynator.CSharp.CodeFixes.Tests
{
    internal static class RemoveSwitchStatement
    {
        private static void Foo()
        {
            string s = null;

            switch (s)
            {
            }
        }
    }
}
