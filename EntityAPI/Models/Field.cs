using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EntityAPI.Models
{
    public enum FieldAction
    {
        None,
        Create,
        Update,
        Delete,
    }

    public class Field
    {
        [Key] public Guid Id { get; set; } = Guid.NewGuid();
        public required string Name { get; set; }
        public required string Value { get; set; }

        public Guid Entityid { get; set; }
        [JsonIgnore] public Entity Entity { get; set; } = null!;
    }

    public class FieldDto
    {
        public Guid? Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
        public FieldAction Action { get; set; } = FieldAction.None;
    }
}
