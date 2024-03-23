using System.ComponentModel.DataAnnotations;

namespace EntityAPI.Models
{
    public class Item
    {
        [Key] public Guid Id { get; set; }
        public required ICollection<Field> Fields { get; set; }
        public Guid Entityid { get; set; }
    }

    public class Itemdto
    {
        public Guid? Id { get; set; }
        public required ICollection<FieldDto> Fields { get; set; }
    }
}
