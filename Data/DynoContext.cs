using Microsoft.EntityFrameworkCore;
using Models;

namespace Repository;

public class DynoContext : DbContext
{
    public DbSet<Measurement> Measurements { get; set; }

    public DbSet<MeasurementResult> MeasurementResults { get; set; }

    public string DbPath { get; }

    public DynoContext()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = Path.Join(path, "dyno.db");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");

    // https://learn.microsoft.com/en-us/ef/core/get-started/overview/first-app?tabs=netcore-cli
}
