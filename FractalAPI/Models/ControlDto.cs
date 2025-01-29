namespace FractalAPI.Models
{
  public class ControlDto
  {
    public Guid? Id { get; set; }
    public Guid? ParentId { get; set; }
    public string Data { get; set; } = string.Empty;
    public string Input { get; set; } = string.Empty;
    public string Indicator { get; set; } = string.Empty;
  }
}