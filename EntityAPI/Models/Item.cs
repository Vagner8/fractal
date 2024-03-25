using System.ComponentModel.DataAnnotations;

namespace EntityAPI.Models
{
    public enum Actions
    {
        None,
        Add,
        Update,
        Delete
    }

    public class Item
    {
        [Key] public Guid Id { get; set; }
        public required ICollection<Field> Fields { get; set; }
        public Guid Entityid { get; set; }
    }

    public class ItemDto
    {
        public Guid? Id { get; set; }
        public Actions Actions { get; set; }
        public required List<FieldDto> Fields { get; set; }
    }

    public class ItemBuilder
    {
        public static Item ToItem(ItemDto itemDto)
        {
            return new Item
            {
                Id = itemDto.Id ?? new Guid(),
                Fields = itemDto.Fields.Select(f => FieldBuilder.ToField(f)).ToList(),
            };
        }
    }
}
