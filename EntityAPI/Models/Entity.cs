using System.ComponentModel.DataAnnotations;

namespace EntityAPI.Models
{
    public class Entity
    {
        [Key] public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public ICollection<Field> Fields { get; set; } = new List<Field>();
    }

    public class EntityDto
    {
        public Guid? Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public ICollection<FieldDto> Fields { get; set; } = new List<FieldDto>();
    }
}
