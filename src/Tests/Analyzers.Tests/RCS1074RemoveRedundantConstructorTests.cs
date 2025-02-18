﻿// Copyright (c) .NET Foundation and Contributors. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Roslynator.CSharp.CodeFixes;
using Roslynator.Testing.CSharp;
using Xunit;

namespace Roslynator.CSharp.Analysis.Tests;

public class RCS1074RemoveRedundantConstructorTests : AbstractCSharpDiagnosticVerifier<RemoveRedundantConstructorAnalyzer, ConstructorDeclarationCodeFixProvider>
{
    public override DiagnosticDescriptor Descriptor { get; } = DiagnosticRules.RemoveRedundantConstructor;

    [Fact, Trait(Traits.Analyzer, DiagnosticIdentifiers.RemoveRedundantConstructor)]
    public async Task Test_SingleInstanceConstructor()
    {
        await VerifyDiagnosticAndFixAsync(@"
class C
{
    [|public C()
    {
    }|]
}
", @"
class C
{
}
");
    }

    [Fact, Trait(Traits.Analyzer, DiagnosticIdentifiers.RemoveRedundantConstructor)]
    public async Task TestNoDiagnostic_UsedImplicitlyAttribute()
    {
        await VerifyNoDiagnosticAsync(@"
class C
{
    [JetBrains.Annotations.UsedImplicitly]
    public C()
    {
    }
}

namespace JetBrains.Annotations
{
    public class UsedImplicitlyAttribute : System.Attribute
    {
    }
}
");
    }

    [Fact, Trait(Traits.Analyzer, DiagnosticIdentifiers.RemoveRedundantConstructor)]
    public async Task TestNoDiagnostic_StructWithFieldInitializer()
    {
        await VerifyNoDiagnosticAsync(@"
struct C
{
    private string _f = """";

    public C()
    {
    }
}
");
    }

    [Fact, Trait(Traits.Analyzer, DiagnosticIdentifiers.RemoveRedundantConstructor)]
    public async Task TestNoDiagnostic_StructWithPropertyInitializer()
    {
        await VerifyNoDiagnosticAsync(@"
struct C
{
    public string P { get; init; } = """";

    public C()
    {
    }
}
");
    }

    [Fact, Trait(Traits.Analyzer, DiagnosticIdentifiers.RemoveRedundantConstructor)]
    public async Task TestNoDiagnostic_RecordStructWithFieldInitializer()
    {
        await VerifyNoDiagnosticAsync(@"
record struct C
{
    private string _f = """";

    public C()
    {
    }
}
");
    }

    [Fact, Trait(Traits.Analyzer, DiagnosticIdentifiers.RemoveRedundantConstructor)]
    public async Task TestNoDiagnostic_RecordStructWithPropertyInitializer()
    {
        await VerifyNoDiagnosticAsync(@"
record struct C
{
    public string P { get; init; } = """";

    public C()
    {
    }
}
");
    }
}
