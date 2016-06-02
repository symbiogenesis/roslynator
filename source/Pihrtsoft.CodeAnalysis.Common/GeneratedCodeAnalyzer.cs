﻿// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.IO;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Diagnostics;

namespace Pihrtsoft.CodeAnalysis
{
    public class GeneratedCodeAnalyzer : IGeneratedCodeAnalyzer
    {
        public const string AutoGeneratedTag = "<auto-generated>";
        private static readonly int _autoGeneratedCommentMinLength = AutoGeneratedTag.Length + 2;

        private static readonly StringComparison _comparison = StringComparison.OrdinalIgnoreCase;

        public bool IsGeneratedCode(CodeBlockAnalysisContext context)
            => IsGeneratedCode(context.CodeBlock.SyntaxTree);

        public bool IsGeneratedCode(SyntaxNodeAnalysisContext context)
            => IsGeneratedCode(context.Node.SyntaxTree);

        public bool IsGeneratedCode(SyntaxTreeAnalysisContext context)
            => IsGeneratedCode(context.Tree);

        public bool IsGeneratedCode(SymbolAnalysisContext context)
            => IsGeneratedCode(context.Symbol);

        private static bool IsGeneratedCode(SyntaxTree tree)
        {
            return IsGeneratedCodeFile(tree.FilePath) || BeginsWithAutoGeneratedComment(tree);
        }

        private static bool IsGeneratedCode(ISymbol symbol)
        {
            for (int i = 0; i < symbol.DeclaringSyntaxReferences.Length; i++)
            {
                if (IsGeneratedCode(symbol.DeclaringSyntaxReferences[i].SyntaxTree))
                    return true;
            }

            return false;
        }

        public static bool IsGeneratedCodeFile(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
                return false;

            string fileName = Path.GetFileName(filePath);

            if (fileName.StartsWith("TemporaryGeneratedFile_", _comparison))
                return true;

            string extension = Path.GetExtension(fileName);

            if (string.IsNullOrEmpty(extension))
                return false;

            string fileNameWithoutExtension = fileName.Substring(0, fileName.Length - extension.Length);

            return string.Equals(fileNameWithoutExtension, "AssemblyInfo", _comparison)
                || fileNameWithoutExtension.EndsWith(".Designer", _comparison)
                || fileNameWithoutExtension.EndsWith(".Generated", _comparison)
                || fileNameWithoutExtension.EndsWith(".g", _comparison)
                || fileNameWithoutExtension.EndsWith(".g.i", _comparison)
                || fileNameWithoutExtension.EndsWith(".AssemblyAttributes", _comparison);
        }

        private static bool BeginsWithAutoGeneratedComment(SyntaxTree tree)
        {
            SyntaxNode root;
            if (!tree.TryGetRoot(out root))
                return false;

            if (root?.Kind() == SyntaxKind.CompilationUnit)
            {
                SyntaxTriviaList leadingTrivia = root.GetLeadingTrivia();

                if (leadingTrivia.Any())
                {
                    foreach (SyntaxTrivia trivia in leadingTrivia)
                    {
                        if (trivia.Kind() == SyntaxKind.SingleLineCommentTrivia)
                        {
                            string text = trivia.ToString();

                            if (text.Length >= _autoGeneratedCommentMinLength
                                && text[0] == '/'
                                && text[1] == '/')
                            {
                                int index = 2;

                                while (index < text.Length
                                    && char.IsWhiteSpace(text[index]))
                                {
                                    index++;
                                }

                                if (string.Compare(text, index, AutoGeneratedTag, 0, AutoGeneratedTag.Length, _comparison) == 0)
                                    return true;
                            }
                        }
                    }
                }
            }

            return false;
        }
    }
}
