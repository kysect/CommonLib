using Kysect.CommonLib.BaseTypes.Extensions;
using System.Linq.Expressions;
using System.Reflection;

namespace Kysect.CommonLib.Reflection;

public class ExpressionBasedInstanceModifier
{
    public static ExpressionBasedInstanceModifier Instance { get; } = new ExpressionBasedInstanceModifier();

    public void ModifyProperty<TInstance, TProperty>(
        TInstance instance,
        Expression<Func<TInstance, TProperty>> selector,
        TProperty newPropertyValue)
    {
        instance = instance.ThrowIfNull(nameof(instance));
        newPropertyValue = newPropertyValue.ThrowIfNull(nameof(newPropertyValue));

        if (selector.Body is not MemberExpression memberExpression)
        {
            throw new ArgumentException($"Selector body should has type {nameof(MemberExpression)}");
        }

        if (memberExpression.Member is not PropertyInfo propertyInfo)
        {
            throw new ArgumentException($"Selector body should access to type {nameof(PropertyInfo)}");
        }

        if (!ReflectionInstanceInitializingExtensions.TrySetToProperty(instance, propertyInfo, newPropertyValue))
            throw new InvalidOperationException($"Cannot update property {propertyInfo.Name} of type {instance.GetType().Name}");
    }
}