namespace MatrixAPI.Dto
{
  public class ControlDictionaryDto : Dictionary<string, ControlDto>;
  public record ControlDto(
    Guid? Id,
    string Indicator,
    string Data
  )
  {
    public void Deconstruct(out Guid? id, out string indicator, out string data)
    {
      id = Id;
      indicator = Indicator;
      data = Data;
    }
  }
}
