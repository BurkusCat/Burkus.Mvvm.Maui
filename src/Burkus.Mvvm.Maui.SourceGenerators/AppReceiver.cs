using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis;

namespace Burkus.Mvvm.Maui;

/// <summary>
/// A syntax receiver that collects App class declarations
/// </summary>
internal class AppReceiver : ISyntaxReceiver
{
    public ClassDeclarationSyntax? AppClass { get; private set; }

    public void OnVisitSyntaxNode(SyntaxNode syntaxNode)
    {
        // look for a class declaration named App
        if (syntaxNode is ClassDeclarationSyntax classDeclaration &&
            classDeclaration.Identifier.ValueText == "App")
        {
            // store the first one found
            AppClass ??= classDeclaration;
        }
    }
}
