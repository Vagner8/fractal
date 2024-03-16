namespace EntityAPI.Models
{
    public class EntityDto
    {
        public Guid? Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public ICollection<FieldDto> Fields { get; set; } = new List<FieldDto>();
    }
}
