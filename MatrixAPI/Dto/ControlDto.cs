namespace MatrixAPI.Dto
{
  public class ControlDictionaryDto : Dictionary<string, ControlDto>;
  public record ControlDto(
    Guid? Id,
    Guid? UnitId,
    string Indicator,
    string Data
  )
  {
    public void Deconstruct(out Guid? id, out Guid? unitId, out string indicator, out string data)
    {
      id = Id;
      unitId = UnitId;
      indicator = Indicator;
      data = Data;
    }
  }
}
