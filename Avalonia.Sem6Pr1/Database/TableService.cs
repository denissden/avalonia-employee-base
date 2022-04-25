using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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

    public void Insert(object o) => _context?.Add(o);
    public void Attach(object o) => _context?.Attach(o);
    public void Delete(object o) => _context?.Remove(o);

    public async Task<bool> Save()
    {
        try
        {
            await _context!.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }

    public async Task<List<int>> GetKeys<T>() where T : class
    {
        if (_context is null) 
            return new List<int>();

        var keyName = _context.Model.FindEntityType(typeof(T))?.FindPrimaryKey()?.Properties
        .Select(x => x.Name).Single();

        return await _context.Set<T>().Select(m => (int)m.GetType().GetProperty(keyName).GetValue(m, null)).ToListAsync();
    }
}