using Models;
using Microsoft.EntityFrameworkCore;

namespace Data;

public class DynoDbContext : DbContext
{
    public DbSet<Measurement> Measurements { get; set; }

    public DbSet<MeasurementResult> MeasurementResults { get; set; }

    public string DbPath { get; }

    public DynoDbContext()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = Path.Join(path, "dyno.db");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Measurement>()
            .HasMany(x => x.MeasurementResults)
            .WithOne(x => x.Measurement)
            .HasForeignKey(x => x.MeasurementId);

        modelBuilder.Entity<MeasurementResult>()
            .HasOne(x => x.Measurement);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");

    // https://learn.microsoft.com/en-us/ef/core/get-started/overview/first-app?tabs=netcore-cli
}
