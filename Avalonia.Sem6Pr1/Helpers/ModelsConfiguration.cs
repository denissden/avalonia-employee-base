using System;
using System.Collections.Generic;

namespace Avalonia.Sem6Pr1.Helpers;

public class ModelsConfiguration
{
    private Dictionary<Type, ModelConfiguration> _configuredModels = new();
    public ModelsConfiguration(params ModelConfiguration[] modelConfigurations)
    {
        foreach (var m in modelConfigurations)   
        {
            _configuredModels.Add(m.ModelType, m);
        }
    }

    public ModelConfiguration? Get(Type modelType)
    {
        _configuredModels.TryGetValue(modelType, out var value);
        return value;
    }
    
}