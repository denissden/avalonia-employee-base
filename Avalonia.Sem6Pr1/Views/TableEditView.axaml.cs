using System.Collections.ObjectModel;
using System.Diagnostics;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Mixins;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using Avalonia.Sem6Pr1.ViewModels;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ReactiveUI;

namespace Avalonia.Sem6Pr1.Views;

public class TableEditView : ReactiveUserControl<TableEditViewModel>
{
    private DataGrid _dataGrid;
    public TableEditView()
    {
        InitializeComponent();
        _dataGrid = this.FindControl<DataGrid>("DataGrid");
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }

    private void DataGrid_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        ViewModel.UpdateSelection(_dataGrid.SelectedIndex);
    }
}