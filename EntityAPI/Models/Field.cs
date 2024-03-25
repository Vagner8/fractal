using System.ComponentModel.DataAnnotations;

namespace EntityAPI.Models
{
    public class Field
    {
        [Key] public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Value { get; set; }

        public Guid Itemid { get; set; }
    }

    public class FieldDto
    {
        public Guid? Id { get; set; }
        public required string Name { get; set; }
        public required string Value { get; set; }
    }

    public class FieldBuilder
    {
        public static Field ToField(FieldDto fieldDto)
        {
            return new Field
            {
                Id = fieldDto.Id ?? new Guid(),
                Name = fieldDto.Name,
                Value = fieldDto.Value,
            };
        }
    }
}
