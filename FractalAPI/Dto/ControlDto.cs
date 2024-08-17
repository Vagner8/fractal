namespace FractalAPI.Dto
{
  public class ControlDictionaryDto : Dictionary<string, ControlDto>;
  public record ControlDto(
    Guid? Id,
    Guid? ParentId,
    string Indicator,
    string Data
  );
}