using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reactive;
using System.Reflection;
using System.Runtime.CompilerServices;
using Avalonia.Controls.Notifications;
using Avalonia.Sem6Pr1.Attributes;
using Avalonia.Sem6Pr1.Database;
using Avalonia.Sem6Pr1.Helpers;
using Avalonia.Sem6Pr1.Interfaces;
using Avalonia.Sem6Pr1.Models;
using JetBrains.Annotations;
using ReactiveUI;
using Splat;
using Notification = System.Reactive.Notification;

namespace Avalonia.Sem6Pr1.ViewModels;

public class TableEditViewModel<T> : TableEditViewModel where T: class, new()
{
    public ObservableCollection<T> Objects { get; } = new ();

    private TableService _tableService;
    private ModelConfiguration? _modelConfiguration;
    
    public TableEditViewModel(IScreen screen) : base(screen)
    {
        // get services
        _modelConfiguration = Locator.Current.GetService<ModelsConfiguration>().Get(typeof(T)) ?? new ModelConfiguration();;
        _tableService = Locator.Current.GetService<TableService>();
        // init commands
        SelectionChanged = ReactiveCommand.Create<int>(UpdateSelection);
        ApplyEdit = ReactiveCommand.Create(TryApplyEditCore);
        Save = ReactiveCommand.CreateFromTask(TrySaveCore);
        Add = ReactiveCommand.Create(AddCore);
        Delete = ReactiveCommand.Create(DeleteCore);
        // load model configuration
        GenerateProperties();
        // load data
        _tableService.LoadAsync<T>(Objects);
    }

    void GenerateProperties()
    {
        var properties = typeof(T).GetProperties();

        foreach (var p in properties)
        {
            // check ignore not editable properties
            var notEditableAttributes = p.GetCustomAttributes(typeof(NotEditableAttribute), false);
            if (notEditableAttributes.Length > 0) continue;

            var nameAttributes = p.GetCustomAttributes(typeof(NameAttribute), false);
            var nameFromAttribute = (nameAttributes.FirstOrDefault() as NameAttribute)?.Name;

            var info = new PropertyData(p);
            var memberConfig = _modelConfiguration.Get(p);
            if (memberConfig != null)
            {
                info.Validator = memberConfig.Validator ?? info.Validator;
                info.ToStringFunc = memberConfig.ToStringConverter ?? info.ToStringFunc;
                info.FromStringFunc = memberConfig.FromStringConverter ?? info.FromStringFunc;
                info.Name = nameFromAttribute ?? memberConfig.Name;
            }

            SelectedObjectProperties.Add(info);
        }
    }

    public override void UpdateSelection(int selectionIndex)
    {
        SelectedIndex = selectionIndex;
        if (selectionIndex < 0) return;
        var selected = Objects[selectionIndex];
        foreach (var pd in SelectedObjectProperties)
        {
            pd.UpdateValue(selected);
        }
    }

    private void TryApplyEditCore()
    {
        if (SelectedIndex < 0 && SelectedIndex < Objects.Count) return;
        var selected = Objects[SelectedIndex];
        if (SelectedObjectProperties.Any(pd => pd.Validate() != true)) return;
        foreach (var pd in SelectedObjectProperties)
        {
            pd.SetObjectValue(selected);
        }
        if (selected is IOnPropertyChanged notifyChanges) notifyChanges.OnPropertyChanged("");
    }

    private async System.Threading.Tasks.Task TrySaveCore()
    {
        bool success = await _tableService.Save();
        if (!success)
        {
            var messageBox = MessageBox.Avalonia.MessageBoxManager.GetMessageBoxStandardWindow(
                "Error",
                "The changes were not saved. Perhaps there are some invalid or empty fields.");
            await messageBox.Show();
        }
    }

    private void AddCore()
    {
        T newObject = new();
        _tableService.Insert(newObject);
        Objects.Add(newObject);
    }
    
    private void DeleteCore()
    {
        if (SelectedIndex < 0) return;
        T toDelete = Objects[SelectedIndex];
        Objects.RemoveAt(SelectedIndex);
        _tableService.Delete(toDelete);
    }
}

public class TableEditViewModel : ViewModelBase, IRoutableViewModel
{
    // ROUTING
    public string? UrlPathSegment { get; } = Guid.NewGuid().ToString().Substring(0, 5);
    public IScreen HostScreen { get; }
    
    // VIEW COMMUNICATION
    public int SelectedIndex { get; set; }
    
    public ObservableCollection<PropertyData> SelectedObjectProperties { get; } = new ();

    // COMMANDS
    public ReactiveCommand<int, Unit> SelectionChanged { get; set; }
    public ReactiveCommand<Unit, Unit> Save { get; set; }
    public ReactiveCommand<Unit, Unit> ApplyEdit { get; set; }
    public ReactiveCommand<Unit, Unit> Delete { get; set; }
    public ReactiveCommand<Unit, Unit> Add { get; set; }

    public TableEditViewModel(IScreen screen)
    {
        HostScreen = screen;
    }

    public virtual void UpdateSelection(int selectionIndex) {}
}

public class PropertyData : INotifyPropertyChanged
{
    private PropertyInfo propertyInfo;

    private object? _value;
    public string? Value
    {
        get => ToStringFunc(_value);
        set
        {
            try
            {
                _value = FromStringFunc(value);
                OnPropertyChanged("");
            }
            catch (Exception)
            {
                _value = null;
            }
        }
    }

    public bool IsValid => Validate() != false;
    public Func<string?, object?> FromStringFunc { get; set; } = o => o is string ? o : null;
    public Func<object?, string?> ToStringFunc { get; set; } = o => o?.ToString();
    public Func<object?, bool?>? Validator { get; set; }


    public PropertyData(PropertyInfo propertyInfo)
    {
        this.propertyInfo = propertyInfo;
    }

    private string? _name;

    public string? Name
    {
        get => _name ?? propertyInfo.Name; 
        set => _name = value;
    }

    public void UpdateValue(object source)
    {
        _value = propertyInfo.GetValue(source);
        OnPropertyChanged("");
    }

    public void SetObjectValue(object source) => propertyInfo.SetValue(source, _value);
    public bool? Validate() => Validator != null ? Validator(_value) : true;
    public event PropertyChangedEventHandler? PropertyChanged;

    [NotifyPropertyChangedInvocator]
    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}