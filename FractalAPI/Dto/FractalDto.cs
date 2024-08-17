namespace FractalAPI.Dto
{
  public class FractalDictionaryDto : Dictionary<string, FractalDto>;
  public record FractalDto(
    Guid? Id,
    Guid? ParentId,
    ControlDictionaryDto Controls,
    FractalDictionaryDto? Fractals
  );
}