namespace FractalAPI.Models
{
  public class ControlDto
  {
    public Guid? Id { get; set; }
    public string Indicator { get; set; } = string.Empty;
    public string Data { get; set; } = string.Empty;
  }
}