﻿// Copyright (c) .NET Foundation and Contributors. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace Roslynator.CSharp.Refactorings.Tests
{
    internal class RemoveInterpolationRefactoring
    {
        public void Foo()
        {
            int index = 0;
            int length = 0;

            string s = $"index: {index}, length: {length}";
        }
    }
}
