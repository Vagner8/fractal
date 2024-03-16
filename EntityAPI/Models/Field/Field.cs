using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EntityAPI.Models
{
    public class Field
    {
        [Key] public Guid Id { get; set; } = Guid.NewGuid();
        public required string Name { get; set; }
        public required string Value { get; set; }

        public int Entityid { get; set; }
        [JsonIgnore] public Entity Entity { get; set; } = null!;
    }
}
