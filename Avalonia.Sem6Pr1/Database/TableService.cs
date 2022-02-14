using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Splat;
using Tmds.DBus;

namespace Avalonia.Sem6Pr1.Database;

public class TableService
{
    private DbContext? _context;
    public TableService() {}

    public async Task LoadAsync<T>(Collection<T> itemsDestination) where T: class
    {
        await Task.Run(() => _context = Locator.Current.GetService<DbContext>());
        await _context.Set<T>().ForEachAsync(o => itemsDestination.Add(o));
    }

    public void Insert(object o) => _context.Add(o);
    public void Attach(object o) => _context.Attach(o);
    public void Delete(object o) => _context.Remove(o);
    public async Task Save() => await _context.SaveChangesAsync();
}