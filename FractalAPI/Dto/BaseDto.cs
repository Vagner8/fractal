namespace FractalAPI.Dto
{
  public class BaseDto
  {
    public Guid Id { get; set; }
    public string Cursor { get; set; } = string.Empty;
    public string? ParentCursor { get; set; } = string.Empty;
  }
}
