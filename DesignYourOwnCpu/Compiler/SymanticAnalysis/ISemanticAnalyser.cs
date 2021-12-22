using Compiler.Ast.Nodes;

namespace Compiler.SymanticAnalysis;

/// <summary>
/// Perform semantic analysis of the AST. In this simple compiler there are only two
/// rules. Identifiers can appear in assignments, expressions etc
///<list type="number">
///    <item>
///         <description>Identifiers must be declared before use </description>
///    </item>
///    <item>
///         <description>Identifiers cannot be declared twice</description>
///    </item>
/// </list>
/// 
/// <para>In a future change we could implement string variables in addition to integers. In that case
/// a new semantic check might be that for assignment the type on the left of the := must be
/// the same as the type of the expression on the right. </para>
/// </summary>
internal interface ISemanticAnalyser
{
    /// <summary>
    /// Scans the AST and reports ant semantic errors
    /// </summary>
    void Scan();
}