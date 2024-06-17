namespace MatrixAPI.Dto
{
  public record UnitDto(
    Guid? Id,
    Guid? UnitId,
    ICollection<UnitDto> Units,
    ControlDictionaryDto Controls
   );
}
