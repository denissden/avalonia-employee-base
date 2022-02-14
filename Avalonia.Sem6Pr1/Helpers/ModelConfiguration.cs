using System;
using System.Collections.Generic;
using System.Reflection;

namespace Avalonia.Sem6Pr1.Helpers;

public class ModelConfiguration
{
    public Dictionary<MemberInfo, MemberConfiguration> ConfiguredMembers { get; set; } = new();
    public Type ModelType { get; set; }

    public ModelConfiguration(ModelConfigurationExpression config, Type modelType)
    {
        ConfiguredMembers = config.ConfiguredMembers;
        ModelType = modelType;
    }
    
    public ModelConfiguration() {}

    public static ModelConfiguration Create<T>(Action<ModelConfigurationExpression<T>> configurationLambda)
    {
        var modelConfiguration = new ModelConfigurationExpression<T>();
        configurationLambda(modelConfiguration);
        return new ModelConfiguration(modelConfiguration, typeof(T));
    }
    
    public MemberConfiguration? Get(MemberInfo memberInfo)
    {
        ConfiguredMembers.TryGetValue(memberInfo, out var value);
        return value;
    }
}