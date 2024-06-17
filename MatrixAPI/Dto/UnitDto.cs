namespace MatrixAPI.Dto
{
  public record UnitDto(
    Guid? Id,
    ICollection<UnitDto> Units,
    ControlDictionaryDto Controls
   );
}
