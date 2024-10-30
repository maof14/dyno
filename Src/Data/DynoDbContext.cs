using Models;
using Microsoft.EntityFrameworkCore;

namespace Data;

public class DynoDbContext : DbContext
{
    public DbSet<MeasurementEntity> Measurements { get; set; }

    public DbSet<MeasurementResultEntity> MeasurementResults { get; set; }

    public DbSet<AppUserEntity> AppUsers { get; set; }

    public string DbPath { get; }

    public DynoDbContext()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = Path.Join(path, "dyno.db");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MeasurementEntity>()
            .HasMany(x => x.MeasurementResults)
            .WithOne(x => x.Measurement)
            .HasForeignKey(x => x.MeasurementId);

        modelBuilder.Entity<MeasurementResultEntity>()
            .HasOne(x => x.Measurement);

        modelBuilder.Entity<MeasurementEntity>()
            .HasOne(x => x.AppUser)
            .WithMany(x => x.Measurements)
            .HasForeignKey(x => x.AppUserId);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");

    // https://learn.microsoft.com/en-us/ef/core/get-started/overview/first-app?tabs=netcore-cli
}
