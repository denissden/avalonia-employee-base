using Avalonia.Sem6Pr1.Models;
using Microsoft.EntityFrameworkCore;

namespace Avalonia.Sem6Pr1.Database;

public class AppContext : DbContext
{
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Department> Departments { get; set; }
    public DbSet<Models.Task> Tasks { get; set; }
    public DbSet<Client> Clients { get; set; }
    public DbSet<Models.Application> Applications { get; set; }

    public AppContext(string conn)
    {
        Database.SetConnectionString(conn);
        //Database.EnsureDeleted();
        Database.EnsureCreated();
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseNpgsql();
        optionsBuilder.EnableSensitiveDataLogging();
    }
}