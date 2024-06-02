﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MatrixAPI.Models
{
  public class Control
  {
    [Key]
    public Guid? Id { get; set; }
    public string Indicator { get; set; } = string.Empty;
    public string Data { get; set; } = string.Empty;

    [ForeignKey("MatrixId")]
    public Guid? MatrixId { get; set; }
    public Matrix Matrix { get; set; } = null!;

    [ForeignKey("GroupId")]
    public Guid? GroupId { get; set; }
    public Group Group { get; set; } = null!;

    [ForeignKey("UnitId")]
    public Guid? UnitId { get; set; }
    public Unit Unit { get; set; } = null!;
  }

  public record ControlDto(Guid? Id, string Indicator, string Data);
}
