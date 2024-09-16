﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models;

[Table("Measurement")]
public class MeasurementEntity : BaseEntity
{
    public MeasurementEntity()
    {
        
    }
    public MeasurementEntity(Guid id, string name, string description, DateTimeOffset dateTimeOffset, List<MeasurementResultEntity> measurementResults)
    {
        Id = id;
        Name = name;
        Description = description;
        DateTimeOffset = dateTimeOffset;
        MeasurementResults = measurementResults;
    }

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    public List<MeasurementResultEntity> MeasurementResults { get; set; }
    public DateTimeOffset DateTimeOffset { get; set; }
    public string Name { get; set; } = $"{DateTimeOffset.Now.DayOfWeek.ToString()} {DateTimeOffset.Now.ToString()}";
    public string Description { get; set; } = string.Empty;
}