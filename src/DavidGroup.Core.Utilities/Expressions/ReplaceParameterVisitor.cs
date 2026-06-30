using System.Linq.Expressions;

namespace DavidGroup.Core.Utilities.Expressions;

/// <summary>
/// An <see cref="ExpressionVisitor"/> that replaces occurrences of a specified <see cref="ParameterExpression"/>
/// with another <see cref="ParameterExpression"/> within an expression tree.
/// </summary>
/// <remarks>
/// This is useful when combining lambda expressions that have different parameter instances
/// but represent the same logical parameter. It allows dynamically rewriting expression trees
/// to unify parameter references.
/// </remarks>
public class ReplaceParameterVisitor(ParameterExpression oldParam, ParameterExpression newParam)
    : ExpressionVisitor
{
    /// <summary>
    /// Replaces parameter.
    /// </summary>
    /// <param name="node">The expression to visit.</param>
    /// <returns>
    /// The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.
    /// </returns>
    protected override Expression VisitParameter(ParameterExpression node)
        => node == oldParam ? newParam : base.VisitParameter(node);
}
