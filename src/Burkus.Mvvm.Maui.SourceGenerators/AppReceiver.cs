using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis;
using System.Collections.Generic;

namespace Burkus.Mvvm.Maui;

/// <summary>
/// A syntax receiver that collects App class declarations
/// </summary>
internal class AppReceiver : ISyntaxReceiver
{
    public List<ClassDeclarationSyntax> AppClasses { get; } = new List<ClassDeclarationSyntax>();

    public void OnVisitSyntaxNode(SyntaxNode syntaxNode)
    {
        // look for a class declaration named App
        if (syntaxNode is ClassDeclarationSyntax classDeclaration
            && classDeclaration.Identifier.ValueText == "App")
        {
            // store all the ones found
            AppClasses.Add(classDeclaration);
        }
    }
}
