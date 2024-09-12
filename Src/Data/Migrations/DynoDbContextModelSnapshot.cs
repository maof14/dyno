﻿// <auto-generated />
using System;
using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Data.Migrations
{
    [DbContext(typeof(DynoDbContext))]
    partial class DynoDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.20");

            modelBuilder.Entity("Models.Measurement", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTimeOffset>("DateTimeOffset")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Measurement");
                });

            modelBuilder.Entity("Models.MeasurementResult", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTimeOffset>("DateTimeOffset")
                        .HasColumnType("TEXT");

                    b.Property<double>("Horsepower")
                        .HasColumnType("REAL");

                    b.Property<Guid>("MeasurementId")
                        .HasColumnType("TEXT");

                    b.Property<double>("RPM")
                        .HasColumnType("REAL");

                    b.Property<double>("Torque")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.HasIndex("MeasurementId");

                    b.ToTable("MeasurementResult");
                });

            modelBuilder.Entity("Models.MeasurementResult", b =>
                {
                    b.HasOne("Models.Measurement", "Measurement")
                        .WithMany("MeasurementResults")
                        .HasForeignKey("MeasurementId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Measurement");
                });

            modelBuilder.Entity("Models.Measurement", b =>
                {
                    b.Navigation("MeasurementResults");
                });
#pragma warning restore 612, 618
        }
    }
}