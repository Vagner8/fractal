namespace MatrixAPI.Dto
{
  public record MatrixDto(
    Guid Id,
    ICollection<UnitDto> Units,
    ControlDictionaryDto Controls
  );
}
