using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Fasetto_Word.Helpers;

public static class ExpressionHelpers
{
    public static T GetPropertyValue<T>(this Expression<Func<T>> lambda)
    {
        return lambda.Compile().Invoke();
    }

    public static void SetPropertyValue<T>(this Expression<Func<T>> lambda, T value)
    {
        if (lambda.Body is MemberExpression memberExpression)
        {
            var propertyInfo = (PropertyInfo)memberExpression.Member!;
            var target = Expression.Lambda(memberExpression.Expression!).Compile().DynamicInvoke();
            propertyInfo.SetValue(target, value);
        }
    }
}