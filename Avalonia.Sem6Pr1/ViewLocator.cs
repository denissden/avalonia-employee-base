using System;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Avalonia.Sem6Pr1.ViewModels;

namespace Avalonia.Sem6Pr1
{
    public class ViewLocator : IDataTemplate
    {
        public IControl Build(object data)
        {
            var name = data.GetType().FullName!.Replace("ViewModel", "View");
            var type = Type.GetType(name);

            if (type != null)
            {
                return (Control) Activator.CreateInstance(type)!;
            }
            else
            {
                return new TextBlock {Text = "Not Found: " + name};
            }
        }

        public bool Match(object data)
        {
            return data is ViewModelBase;
        }
    }
}