using System;

namespace Avalonia.Sem6Pr1.Helpers;

public class MemberConfigurationExpression<T, TMember>
{
    public MemberConfiguration Configuration = new();
    
    public MemberConfigurationExpression<T, TMember> Validate(Func<TMember?, bool> validator)
    {
        Configuration.Validator = o => validator((TMember?)o);
        return this;
    }
    
    public MemberConfigurationExpression<T, TMember> ConvertFromString(Func<string?, TMember?> converter)
    {
        Configuration.FromStringConverter = s => converter(s);
        return this;
    }
    
    public MemberConfigurationExpression<T, TMember> ConvertToString(Func<TMember?, string?> converter)
    {
        Configuration.ToStringConverter = o => converter((TMember?)o);
        return this;
    }
    
    public MemberConfigurationExpression<T, TMember> Name(string name)
    {
        Configuration.Name = name;
        return this;
    }
    
    public MemberConfigurationExpression<T, TMember> ValidateNotNull()
    {
        Configuration.Validator = o => o != null;
        return this;
    }
}