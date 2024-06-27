namespace MatrixAPI.Dto
{
  public class UnitDictionaryDto : Dictionary<string, UnitDto>;
  public record UnitDto(
    Guid? Id,
    Guid? ParentId,
    ControlDictionaryDto Controls,
    UnitDictionaryDto? Units
  );
}
