using System.Linq.Expressions;

using DavidGroup.Core.Utilities.Expressions;

namespace DavidGroup.Core.Utilities.Samples.Expressions;

public static class ReplaceParameterVisitorSamples
{
    public static void Run()
    {
        ReplaceParameterSample();
    }

    private static void ReplaceParameterSample()
    {
        Expression<Func<Person, bool>> expression = p => p.Age >= 18;

        ParameterExpression newParameter = Expression.Parameter(typeof(Person), "person");

        ReplaceParameterVisitor visitor = new(expression.Parameters[0], newParameter);

        Expression newBody = visitor.Visit(expression.Body);

        Expression<Func<Person, bool>> updatedExpression =
            Expression.Lambda<Func<Person, bool>>(newBody, newParameter);

        Console.WriteLine(expression);
        Console.WriteLine(updatedExpression);
    }

    private sealed class Person
    {
        public int Age { get; init; }
    }
}
