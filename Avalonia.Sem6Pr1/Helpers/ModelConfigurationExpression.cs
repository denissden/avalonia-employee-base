using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace Avalonia.Sem6Pr1.Helpers;

public class ModelConfigurationExpression<T> : ModelConfigurationExpression
{
    public ModelConfigurationExpression<T> ForMember<TMember>(
        Expression<Func<T, TMember>> member,
        Action<MemberConfigurationExpression<T, TMember>> configuration)
    {
        var expr = new MemberConfigurationExpression<T, TMember>();
        configuration(expr);
        var memberConfig = expr.Configuration;
        var memberInfo = FindProperty(member);
        ConfiguredMembers.Add(memberInfo, memberConfig);

        return this;
    }
}

public class ModelConfigurationExpression
{
    public Dictionary<MemberInfo, MemberConfiguration> ConfiguredMembers { get; set; } = new();

    internal MemberInfo FindProperty(LambdaExpression lambdaExpression)
    {
        Expression expression = lambdaExpression.Body;
        while (true)
        {
            switch (expression)
            {
                case MemberExpression { Member: var member, 
                    Expression: {NodeType: ExpressionType.Parameter or ExpressionType.Convert}}:
                    return member;
                case UnaryExpression {Operand: var operand}:
                    expression = operand;
                    break;
                default: throw new ArgumentException("Invalid member getter expression");
            }
        }
    }
}